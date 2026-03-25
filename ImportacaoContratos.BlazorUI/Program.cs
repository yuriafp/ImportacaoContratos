using ImportacaoContratos.BlazorUI;
using ImportacaoContratos.BlazorUI.Handlers;
using ImportacaoContratos.BlazorUI.Services; 
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7007/")
});

//services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RelatoriosService>();
builder.Services.AddScoped<JwtAuthorizationHandler>();


builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7007/");
})
.AddHttpMessageHandler<JwtAuthorizationHandler>();

builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var client = clientFactory.CreateClient("API");
    return new RelatoriosService(client);
});

await builder.Build().RunAsync();