using HeadlessSharp.Components;
using DotNetEnv;


// get storefront API key from environment file
Env.Load();
string apiKey = Environment.GetEnvironmentVariable("STOREFRONT_API_TOKEN");
string domain = Environment.GetEnvironmentVariable("STOREFRONT_DOMAIN");

var builder = WebApplication.CreateBuilder(args);

// add http client
// https://learn.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-8.0
builder.Services.AddHttpClient("ShopifyClient", client =>
{
    client.BaseAddress = new Uri(domain);
    client.DefaultRequestHeaders.Add("X-Shopify-Access-Token", apiKey);
});


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();