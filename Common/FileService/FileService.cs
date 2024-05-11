using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace f00die_finder_be.Common.FileService
{
    public class FileService : IFileService
    {
        protected readonly IConfiguration _configuration;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
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
                            httpClient.BaseAddress = new Uri(_configuration["BaseURL"]);

                            var response = await httpClient.PostAsync("images/", content);
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
