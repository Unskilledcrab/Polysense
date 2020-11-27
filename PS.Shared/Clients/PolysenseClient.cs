using PS.Shared.Models.Abstractions;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Shared.Clients
{
    public abstract class PolysenseClient
    {
        public PolysenseClient(HttpClient httpClient)
        {
            //httpClient.BaseAddress = new Uri("http://localhost:63763/api/");
            httpClient.BaseAddress = new Uri("http://polysense.us/api/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            client = httpClient;
        }

        protected HttpClient client { get; }

        public async Task<T> DeleteAsync<T>(string endpoint, T deleteObject, CancellationToken token = default) where T : BaseEntity
        {
            token.ThrowIfCancellationRequested();
            var response = await client.DeleteAsync($"{endpoint}/{deleteObject.Id}").ConfigureAwait(false);
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> DeleteAsync<T>(string endpoint, CancellationToken token = default) where T : BaseEntity
        {
            token.ThrowIfCancellationRequested();
            var response = await client.DeleteAsync(endpoint).ConfigureAwait(false);
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> GetAsync<T>(string endpoint, CancellationToken token = default) where T : class
        {
            token.ThrowIfCancellationRequested();
            var response = await client.GetAsync(endpoint, token);
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> PostAsync<T>(string endpoint, T postObject, CancellationToken token = default) where T : class
        {
            token.ThrowIfCancellationRequested();
            var response = await client.PostAsync(endpoint, postObject, new JsonMediaTypeFormatter(), token).ConfigureAwait(false);
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> PutAsync<T>(string endpoint, T postObject, CancellationToken token = default) where T : class
        {
            token.ThrowIfCancellationRequested();
            var response = await client.PutAsync(endpoint, postObject, new JsonMediaTypeFormatter(), token).ConfigureAwait(false);
            return await DeserializeResponse<T>(response);
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<T>(token);
        }
    }
}