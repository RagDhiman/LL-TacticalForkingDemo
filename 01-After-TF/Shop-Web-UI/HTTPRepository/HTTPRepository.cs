using Newtonsoft.Json;
using Shop_Web_UI.DTOs;
using System.Net.Http.Headers;

namespace Shop_Web_UI.HTTPRepository
{
    public class HTTPRepository<T> : IHTTPRepository<T> where T: IEntity
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public string APIPath { get; set; }

        public HTTPRepository(IConfiguration configuration)
        {
            _httpClient.BaseAddress = new Uri($"{configuration.GetValue<string>("BackendApi")}");
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
            _httpClient.DefaultRequestHeaders.Accept.Clear(); 
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //HttyRequestMessage
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{APIPath}");
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
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{APIPath}/{entity.Id}");
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
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{APIPath}/{id}");
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
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{APIPath}");
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

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"{APIPath}/{entity.Id}");
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
