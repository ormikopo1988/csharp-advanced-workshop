using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace GenericConstraints
{
    public class HttpClientService : IHttpClientService
    {
        public async Task<TResponseModel> GetRequestAsync<TResponseModel>(string requestUrl) 
            where TResponseModel : class
        {
            using (HttpResponseMessage responseMessage = await GetRequestAsync(requestUrl))
            {
                string body = await responseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TResponseModel>(body);
            }
        }

        private async Task<HttpResponseMessage> GetRequestAsync(string requestUrl)
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            {
                return await SendRequestAsync(request);
            }
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage responseMessage = await httpClient.SendAsync(request);

                return responseMessage;
            }
        }
    }
}