using Newtonsoft.Json.Linq;

namespace f00die_finder_be.Common
{
    public static class FileService
    {
        private const string ImgbbAPI = "0b3a1a01592a719072a36436ba3f503a";
        public static string GetFileExtension(string fileName) => Path.GetExtension(fileName);

        public static async Task<byte[]> GetBytesAsync(this IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
        public static async Task<string> UploadImageToImgbb(IFormFile file, Guid id)
        {
            var client = new HttpClient();
            var url = $"https://api.imgbb.com/1/upload?key={ImgbbAPI}";
            var content = new MultipartFormDataContent();
            var b64 = Convert.ToBase64String(await GetBytesAsync(file));

            content.Add(new StringContent(b64), "image");

            var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            var response = await client.SendAsync(request);
            return JObject.Parse(await response.Content.ReadAsStringAsync())["data"]["url"].ToString();
        }
    }
}
