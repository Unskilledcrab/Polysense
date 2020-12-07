using PS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Shared.HttpClients
{
    public class ScraperTextClient : PolysenseClient
    {
        public ScraperTextClient(HttpClient httpClient) : base(httpClient)
        {
            client.BaseAddress = new Uri($"{client.BaseAddress}ScraperTexts");
        }

        public async Task<IEnumerable<ScraperText>> GetScraperTexts(CancellationToken token = default)
        {
            return await GetAsync<IEnumerable<ScraperText>>(string.Empty, token);
        }

        public async Task<ScraperText> GetScraperText(int id, CancellationToken token = default)
        {
            return await GetAsync<ScraperText>($"/{id}", token);
        }

        public async Task<ScraperText> SetScraperText(ScraperText ScraperText, CancellationToken token = default)
        {
            return await PostAsync(ScraperText, string.Empty, token);
        }

        public async Task<ScraperText> UpdateScraperText(ScraperText ScraperText, CancellationToken token = default)
        {
            return await PutAsync(ScraperText, string.Empty, token);
        }

        public async Task<ScraperText> DeleteScraperText(int id, CancellationToken token = default)
        {
            return await DeleteAsync<ScraperText>($"/{id}", token);
        }
    }
}