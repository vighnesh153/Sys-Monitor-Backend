using System.Net.Http;
using System.Threading.Tasks;

namespace SystemMonitor.ServiceLayer
{
    public class RequestMaker
    {
        public static async Task<bool> MakePutRequest(string json, string putUrl)
        {
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(json);
                var response = client.PutAsync(putUrl, stringContent).Result;

                await response.Content.ReadAsStringAsync();
            }
            return true;
        }
    }
}
