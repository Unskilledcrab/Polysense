using PS.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PS.UI.Shared.Clients
{
    public class PoliticianClient : PolysenseClient
    {
        public PoliticianClient()
        {
            BaseAddress = new Uri($"{BaseAddress}politicians");
        }

        public int id { get; set; }

        public async Task<IEnumerable<Politician>> GetPoliticians(CancellationToken token)
        {
            return await GetAsync<IEnumerable<Politician>>(String.Empty, token);
        }
    }
}