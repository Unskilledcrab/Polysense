using Newtonsoft.Json;
using PS.Shared.Models;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace PS.UI.Shared
{
    public class PolysenseClient : HttpClient
    {
        public PolysenseClient()
        {
            BaseAddress = new Uri("http://localhost:63763/api/");
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<T> GetAsync<T>(string endpoint) where T : class
        {
            var response = await this.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> PostAsync<T>(string endpoint, T postObject) where T : class
        {
            var response = await this.PostAsync(endpoint, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();            
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> PutAsync<T>(string endpoint, T postObject) where T : class
        {
            var response = await this.PutAsync(endpoint, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> DeleteAsync<T>(string endpoint, T deleteObject) where T : BaseEntity
        {
            var response = await this.DeleteAsync($"{endpoint}/{deleteObject.Id}");
            response.EnsureSuccessStatusCode();
            return await DeserializeResponse<T>(response);
        }
    }
}
