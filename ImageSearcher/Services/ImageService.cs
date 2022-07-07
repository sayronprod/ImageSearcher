using System.Net.Http.Headers;

namespace ImageSearcher.Services
{
    internal class ImageService
    {
        private readonly string BaseUrl = "https://www.google.com/";

        public async Task<string> GetImageUrl(byte[] imageData)
        {
            string result = String.Empty;
            using (var httpClientHandle = new HttpClientHandler() { AllowAutoRedirect = false })
            {
                using (var client = new HttpClient(httpClientHandle))
                {
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");

                    using (var content = new MultipartFormDataContent())
                    {
                        var fileContent = new ByteArrayContent(imageData, 0, imageData.Length);
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "\"encoded_image\"",
                            FileName = "\"image.jpg\"",
                        };
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                        content.Add(fileContent);

                        using (var message = await client.PostAsync($"{BaseUrl}searchbyimage/upload", content))
                        {
                            result = message.Headers.Location?.ToString();
                        }
                    }
                }
            }


            return result;

        }

        public async Task<string> GetImageUrl(string filePath)
        {
            byte[] imageData = File.ReadAllBytes(filePath);
            return await GetImageUrl(imageData);
        }
    }
}
