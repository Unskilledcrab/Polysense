﻿using PS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Shared.HttpClients
{
    public class PoliticianClient : PolysenseClient
    {
        public PoliticianClient(HttpClient httpClient) : base(httpClient)
        {
            client.BaseAddress = new Uri($"{client.BaseAddress}politicians");
        }

        public async Task<IEnumerable<Politician>> GetPoliticians(CancellationToken token = default)
        {
            return await GetAsync<IEnumerable<Politician>>(token: token);
        }

        public async Task<Politician> GetPolitician(int id, CancellationToken token = default)
        {
            return await GetAsync<Politician>($"/{id}", token);
        }

        public async Task<Politician> SetPolitician(Politician politician, CancellationToken token = default)
        {
            return await PostAsync(politician, token: token);
        }

        public async Task<Politician> UpdatePolitician(Politician politician, CancellationToken token = default)
        {
            return await PutAsync(politician, token: token);
        }

        public async Task<Politician> DeletePolitician(int id, CancellationToken token = default)
        {
            return await DeleteAsync<Politician>($"/{id}", token);
        }

        public async Task<Politician> DeletePolitician(Politician politician, CancellationToken token = default)
        {
            return await DeleteAsync(politician, token: token);
        }
    }
}