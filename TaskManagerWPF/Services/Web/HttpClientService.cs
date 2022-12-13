using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaskManagerWPF.Services.Web
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _domainUrl;

        public HttpClientService(string domainUrl)
        {
            _domainUrl = domainUrl;
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string route)
        {
            var url = _domainUrl + route;
            var response = await _httpClient.GetAsync(url);

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync<TDataType>(TDataType? objectData, string route)
        {
            var url = _domainUrl + route;
            var json = JsonConvert.SerializeObject(objectData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, data);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync<TDataType>(string route)
        {
            var url = _domainUrl + route;
            var response = await _httpClient.DeleteAsync(url);

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync<TDataType>(TDataType objectData, string route)
        {
            var url = _domainUrl + route;
            var json = JsonConvert.SerializeObject(objectData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, data);

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string route)
        {
            var url = _domainUrl + route;
            var response = await _httpClient.PostAsync(url, null);

            return response;
        }

        public static async Task<TResponseDataType> DeserializeResponse<TResponseDataType>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponseDataType>(responseString)!;
        }
    }
}
