using PS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Shared.HttpClients
{
    public class TextCategoryFinalizedClient : PolysenseClient
    {
        public TextCategoryFinalizedClient(HttpClient httpClient) : base(httpClient)
        {
            client.BaseAddress = new Uri($"{client.BaseAddress}TextCategoryFinalized");
        }

        public async Task<IEnumerable<TextCategoryFinalized>> GetTextCategoryFinalizeds(CancellationToken token = default)
        {
            return await GetAsync<IEnumerable<TextCategoryFinalized>>(string.Empty, token);
        }

        public async Task<TextCategoryFinalized> GetTextCategoryFinalized(int id, CancellationToken token = default)
        {
            return await GetAsync<TextCategoryFinalized>($"/{id}", token);
        }

        public async Task<TextCategoryFinalized> SetTextCategoryFinalized(TextCategoryFinalized TextCategoryFinalized, CancellationToken token = default)
        {
            return await PostAsync(TextCategoryFinalized, string.Empty, token);
        }

        public async Task<TextCategoryFinalized> UpdateTextCategoryFinalized(TextCategoryFinalized TextCategoryFinalized, CancellationToken token = default)
        {
            return await PutAsync(TextCategoryFinalized, string.Empty, token);
        }

        public async Task<TextCategoryFinalized> DeleteTextCategoryFinalized(int id, CancellationToken token = default)
        {
            return await DeleteAsync<TextCategoryFinalized>($"/{id}", token);
        }
    }
}