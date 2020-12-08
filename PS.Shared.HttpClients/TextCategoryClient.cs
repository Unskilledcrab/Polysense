using PS.Shared.Http;
using PS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Shared.HttpClients
{
    public class TextCategoryClient : PolysenseClient
    {
        public TextCategoryClient(HttpClient httpClient) : base(httpClient)
        {
            client.BaseAddress = new Uri($"{client.BaseAddress}TextCategorys");
        }

        public async Task<PagedResponse<IEnumerable<TextCategory>>> GetTextCategorys(int pageNumber = 1, int pageSize = PaginationFilter.DefaultPageSize, CancellationToken token = default)
        {
            var endpoint = $"?pageNumber={pageNumber}&pageSize={pageSize}";
            return (await GetAsync<PagedResponse<IEnumerable<TextCategory>>>(endpoint, token)).Build(client);
        }

        public async Task<TextCategory> GetTextCategory(int id, CancellationToken token = default)
        {
            return await GetAsync<TextCategory>($"/{id}", token);
        }

        public async Task<TextCategory> SetTextCategory(TextCategory TextCategory, CancellationToken token = default)
        {
            return await PostAsync(TextCategory, string.Empty, token);
        }

        public async Task<TextCategory> UpdateTextCategory(TextCategory TextCategory, CancellationToken token = default)
        {
            return await PutAsync(TextCategory, string.Empty, token);
        }

        public async Task<TextCategory> DeleteTextCategory(int id, CancellationToken token = default)
        {
            return await DeleteAsync<TextCategory>($"/{id}", token);
        }

        public async Task<TextCategory> DeleteTextCategory(TextCategory TextCategory, CancellationToken token = default)
        {
            return await DeleteAsync(TextCategory, token: token);
        }
    }
}