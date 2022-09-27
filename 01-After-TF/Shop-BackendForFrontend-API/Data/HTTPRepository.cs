using Newtonsoft.Json;
using Shop_BackendForFrontend_API.BaseAddresses;
using System.Net.Http.Headers;

namespace Shop_BackendForFrontend_API.Data
{
    public class HTTPRepository<T> : IHTTPRepository<T> where T: IEntity
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        private string _APIPath { get; set; }

        public HTTPRepository(IConfiguration configuration)
        {
        }
        public void SetBaseAddress(IBaseAddress baseAddress, string APIPath)
        {
            _APIPath = APIPath;
            _httpClient.BaseAddress = baseAddress.BaseAddress;
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //HttyRequestMessage
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_APIPath}");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Receive HttyResponseMessage
            var responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode(); 
            var content = await responseMessage.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<IEnumerable<T>>(content);

            return entities;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            //HttyRequestMessage
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{_APIPath}/{entity.Id}");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Receive HttyResponseMessage
            var responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode(); 
            var content = await responseMessage.Content.ReadAsStringAsync();

            return (content.Length==0);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            //HttyRequestMessage
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_APIPath}/{id}");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Receive HttyResponseMessage
            var responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode(); 
            var content = await responseMessage.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<T>(content);

            return entities;
        }

        public async Task<bool> AddAsync(T entity)
        {
            //HttyRequestMessage
            var entityToCreate = JsonConvert.SerializeObject(entity);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_APIPath}");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = new StringContent(entityToCreate);
            requestMessage.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Receive HttyResponseMessage
            var responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode(); 
            var content = await responseMessage.Content.ReadAsStringAsync();
            var newEntity = JsonConvert.DeserializeObject<T>(content);

            return (newEntity.Id>0);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            //HttyRequestMessage
            var entityToUpdate = JsonConvert.SerializeObject(entity);

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"{_APIPath}/{entity.Id}");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = new StringContent(entityToUpdate);
            requestMessage.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            //Receive HttyResponseMessage
            var responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode(); 
            var content = await responseMessage.Content.ReadAsStringAsync();
            var newEntity = JsonConvert.DeserializeObject<T>(content);

            return (newEntity.Id > 0);
        }

    }
}
