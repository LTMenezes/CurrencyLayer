using System.IO;
using System.Net.Http;
using System.Text;

namespace CurrencyLayerBackend.Infrastructure.HttpUtils
{
    public static class RequestUtils
    {
        public static string GetBody(this HttpResponseMessage response)
        {
            string responseContents;
            using (Stream receiveStream = response.Content.ReadAsStreamAsync().Result)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    responseContents = readStream.ReadToEnd();
                }
            }
            return responseContents;
        }

        public static HttpResponseMessage CreateHttpResponse(string content)
        {
            var result = new HttpResponseMessage();
            result.Content = new StringContent(content, Encoding.UTF8, "application/json");
            result.Headers.Add("Access-Control-Allow-Origin", "*");
            result.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");

            return result;
        }
    }
}
