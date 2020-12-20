using PS.Shared.HttpClients.Abstractions;
using PS.Shared.Models;
using System;
using System.Net.Http;

namespace PS.Shared.HttpClients
{
    public class TextCategoryClient : BaseClient<TextCategory>
    {
        public TextCategoryClient(HttpClient httpClient) : base(httpClient)
        {
            client.BaseAddress = new Uri($"{client.BaseAddress}TextCategories/");
        }
    }
}