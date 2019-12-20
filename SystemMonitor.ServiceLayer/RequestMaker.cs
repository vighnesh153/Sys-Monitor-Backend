using System.Net.Http;

namespace SystemMonitor.ServiceLayer
{
    public class RequestMaker
    {
        public static void MakePutRequest(string json, string putUrl)
        {
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(json);
                client.PutAsync(putUrl, stringContent);
            }
        }
    }
}
