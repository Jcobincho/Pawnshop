// Librabies
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Pawnshop.Web.Components;
using Pawnshop.Web.Services.ApiService;
using Pawnshop.Web.Services.AuthenticationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddAuthenticationCore();

// Backend URL configuration
var backendUrl = builder.Configuration["BackendUrl"] ?? "https://localhost:7287";
if (!backendUrl.EndsWith("/")) backendUrl += "/";

builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri(backendUrl);
});

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(backendUrl)
    });


builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<Pawnshop.Web.Services.LanguageService>();
builder.Services.AddTransient<MudBlazor.MudLocalizer, Pawnshop.Web.Services.MudBlazorLocalizer>();
builder.Services.AddScoped<AuthStateProviderService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProviderService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
