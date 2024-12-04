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
string ApiKey = Environment.GetEnvironmentVariable("STOREFRONT_API_TOKEN");
string Domain = Environment.GetEnvironmentVariable("STOREFRONT_DOMAIN");
string ApiVersion = Environment.GetEnvironmentVariable("STOREFRONT_API_VERSION");

if (ApiKey != null && Domain != null && ApiVersion != null)
{
    var _singleton = SfapiSubject.GetInstance(ApiKey, Domain, ApiVersion);
    app.Run();
}
else
{
    throw new Exception("API key or domain is missing.");
}