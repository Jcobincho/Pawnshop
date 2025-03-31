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
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new(Environment.GetEnvironmentVariable("FrontendUrl") ?? "https+http://localhost:7287");
});

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7287")
    });


builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<AuthStateProviderService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProviderService>();


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
