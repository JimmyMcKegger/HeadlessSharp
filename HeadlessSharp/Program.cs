using HeadlessSharp.Components;
using DotNetEnv;
using HeadlessSharp;

// get storefront API key from environment file
Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();
builder.Logging.AddDebug();

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

// Start singleton with environment variables
string apiKey = Environment.GetEnvironmentVariable("STOREFRONT_API_TOKEN");
// Console.WriteLine($"API Key: {apiKey}");
string domain = Environment.GetEnvironmentVariable("STOREFRONT_DOMAIN");
// Console.WriteLine($"Domain: {domain}");
if (apiKey != null && domain != null)
{
    var _singleton = SfapiSubject.GetInstance(apiKey, domain);
    app.Run();
}
else
{
    throw new Exception("API key or domain is missing.");
}