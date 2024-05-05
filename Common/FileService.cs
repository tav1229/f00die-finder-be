using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace f00die_finder_be.Common
{
    public static class FileService
    {
        public static async Task<string> UploadImageAsync(IFormFile file)
        {
            try
            {
                using (var content = new MultipartFormDataContent())
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var streamContent = new StreamContent(stream);
                        streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(streamContent, "file", file.FileName);

                        using (var httpClient = new HttpClient())
                        {
                            httpClient.BaseAddress = new Uri("http://104.43.108.3:8000/");

                            var response = await httpClient.PostAsync("images", content);
                            response.EnsureSuccessStatusCode();

                            var responseContent = await response.Content.ReadAsStringAsync();
                            dynamic responseData = JObject.Parse(responseContent);
                            return responseData.image_url;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                throw;
            }
        }
    }
}
