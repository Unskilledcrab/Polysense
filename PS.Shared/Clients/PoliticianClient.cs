using PS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Shared.Clients
{
    public class PoliticianClient : PolysenseClient
    {
        public PoliticianClient()
        {
            BaseAddress = new Uri($"{BaseAddress}politicians");
        }

        public async Task<IEnumerable<Politician>> GetPoliticians(CancellationToken token = default)
        {
            return await GetAsync<IEnumerable<Politician>>(string.Empty, token);
        }

        public async Task<Politician> GetPolitician(int id, CancellationToken token = default)
        {
            return await GetAsync<Politician>($"/{id}", token);
        }

        public async Task<Politician> SetPolitician(Politician politician, CancellationToken token = default)
        {
            return await PostAsync(string.Empty, politician, token);
        }

        public async Task<Politician> UpdatePolitician(Politician politician, CancellationToken token = default)
        {
            return await PutAsync(string.Empty, politician, token);
        }

        public async Task<Politician> DeletePolitician(int id, CancellationToken token = default)
        {
            return await DeleteAsync<Politician>($"/{id}", token);
        }
    }
}