using PS.Shared.Models;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
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

        public async Task<T> DeleteAsync<T>(string endpoint, T deleteObject, CancellationToken token = default) where T : BaseEntity
        {
            token.ThrowIfCancellationRequested();
            var response = await this.DeleteAsync($"{endpoint}/{deleteObject.Id}").ConfigureAwait(false);
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> GetAsync<T>(string endpoint, CancellationToken token = default) where T : class
        {
            token.ThrowIfCancellationRequested();
            var response = await this.GetAsync(endpoint, token);
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> PostAsync<T>(string endpoint, T postObject, CancellationToken token = default) where T : class
        {
            token.ThrowIfCancellationRequested();
            var response = await this.PostAsync(endpoint, postObject, new JsonMediaTypeFormatter(), token).ConfigureAwait(false);
            return await DeserializeResponse<T>(response);
        }

        public async Task<T> PutAsync<T>(string endpoint, T postObject, CancellationToken token = default) where T : class
        {
            token.ThrowIfCancellationRequested();
            var response = await this.PutAsync(endpoint, postObject, new JsonMediaTypeFormatter(), token).ConfigureAwait(false);
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