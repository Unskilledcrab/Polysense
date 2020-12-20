using PS.Shared.HttpClients.Abstractions;
using PS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Shared.HttpClients
{
    public class ScraperTextClient : BaseClient<ScraperText>
    {
        public ScraperTextClient(HttpClient httpClient) : base(httpClient)
        {
            client.BaseAddress = new Uri($"{client.BaseAddress}ScraperTexts/");
        }

        public async Task<IEnumerable<ScraperText>> GetScraperTextByCategoryId(int categoryId, CancellationToken token = default)
        {
            return await GetAsync<IEnumerable<ScraperText>>($"category/{categoryId}", token);
        }

        public async Task<IEnumerable<ScraperText>> GetScraperTextByCategoryNull(CancellationToken token = default)
        {
            return await GetAsync<IEnumerable<ScraperText>>("category/null", token);
        }
    }
}