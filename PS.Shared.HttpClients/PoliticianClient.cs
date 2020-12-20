using PS.Shared.HttpClients.Abstractions;
using PS.Shared.Models;
using System;
using System.Net.Http;

namespace PS.Shared.HttpClients
{
    public class PoliticianClient : BaseClient<Politician>
    {
        public PoliticianClient(HttpClient httpClient) : base(httpClient)
        {
            client.BaseAddress = new Uri($"{client.BaseAddress}politicians/");
        }
    }
}