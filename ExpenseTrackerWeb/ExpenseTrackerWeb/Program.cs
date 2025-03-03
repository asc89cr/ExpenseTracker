using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Fast.Components.FluentUI;

namespace ExpenseTrackerWeb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Register Fluent UI services
            builder.Services.AddFluentUIComponents();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<TokenAuthenticationStateProvider>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
            provider.GetRequiredService<TokenAuthenticationStateProvider>());
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7122/") });
            builder.Services.AddBlazoredLocalStorage();

            

            await builder.Build().RunAsync();
        }
    }
}
