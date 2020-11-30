using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PS.UI.Shared;
using System.Threading.Tasks;

namespace PS.UI.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            DIConfigure.Shared(builder.Services);

            await builder.Build().RunAsync();
        }
    }
}