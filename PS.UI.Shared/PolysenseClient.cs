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
#if DEBUG
            BaseAddress = new Uri("http://localhost:63763/api/");
#else
            BaseAddress = new Uri("http://localhost:63763/api/");
#endif
        }

        public async Task<T> DeleteAsync<T>(string endpoint, T deleteObject) where T : BaseEntity
        {
            var response = await this.DeleteAsync($"{endpoint}/{deleteObject.Id}");
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> GetAsync<T>(string endpoint) where T : class
        {
            var response = await this.GetAsync(endpoint);
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> PostAsync<T>(string endpoint, T postObject) where T : class
        {
            var response = await this.PostAsync(endpoint, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> PutAsync<T>(string endpoint, T postObject) where T : class
        {
            var response = await this.PutAsync(endpoint, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);
            return await DeserializeResponse<T>(response);
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>();
        }
    }
}