﻿using Minio;
using Minio.DataModel.Args;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace f00die_finder_be.Common.FileService
{
    public class FileService : IFileService
    {
        protected readonly IConfiguration _configuration;
        private readonly IMinioClient _minioClient;
        private readonly string _baseUrl;
        private string BucketName => _configuration["Minio:BucketName"];

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl =
                 $"{(_configuration["Minio:UseSSL"] == "True" ? "https://" : "http://")}{_configuration["Minio:EndPoint"]}/{BucketName}/";

            _minioClient = new MinioClient()
                .WithEndpoint(_configuration["Minio:EndPoint"])
                .WithCredentials(_configuration["Minio:AccessKey"], _configuration["Minio:SecretKey"])
                .WithSSL(_configuration["Minio:UseSSL"].ToUpper() == "TRUE")
                .Build();
        }

        public async Task<string> UploadFileGetUrlAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(BucketName)
                .WithObject(fileName)
                .WithObjectSize(-1)
                .WithStreamData(file.OpenReadStream())
                .WithContentType(file.ContentType);
            await _minioClient.PutObjectAsync(putObjectArgs);

            return $"{_baseUrl}{fileName}";
        }

        public async Task DeleteFileAsync(List<string> fileNames)
        {
            var removeObjectArgs = new RemoveObjectsArgs()
                .WithBucket(BucketName)
                .WithObjects(fileNames);
            await _minioClient.RemoveObjectsAsync(removeObjectArgs);
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