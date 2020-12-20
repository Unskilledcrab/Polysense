using PS.Shared.Http;
using PS.Shared.Models.Abstractions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Shared.HttpClients.Abstractions
{
    public class BaseClient<T> : PolysenseClient where T : BaseEntity
    {
        public BaseClient(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<PagedResponse<IEnumerable<T>>> GetEntities(int pageNumber = 1, int pageSize = PaginationFilter.DefaultPageSize, CancellationToken token = default)
        {
            var endpoint = $"?pageNumber={pageNumber}&pageSize={pageSize}";
            return (await GetAsync<PagedResponse<IEnumerable<T>>>(endpoint, token)).Build(client);
        }

        public async Task<T> GetEntity(int id, CancellationToken token = default)
        {
            return await GetAsync<T>($"{id}", token);
        }

        public async Task<T> SetEntity(T entity, CancellationToken token = default)
        {
            return await PostAsync(entity, token: token);
        }

        public async Task<T> UpdateEntity(T entity, CancellationToken token = default)
        {
            return await PutAsync(entity, token: token);
        }

        public async Task<T> DeleteEntity(int id, CancellationToken token = default)
        {
            return await DeleteAsync<T>($"{id}", token);
        }

        public async Task<T> DeleteEntity(T entity, CancellationToken token = default)
        {
            return await DeleteAsync(entity, token: token);
        }
    }
}