using MAAM.Components;
using MAAM.Services;
using Microsoft.AspNetCore.ResponseCompression;
using MudBlazor.Services;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<AssetService>();
builder.Services.AddHostedService<SystemService>();
builder.Services.AddMinionsData();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});

builder.Services.AddControllers();
var app = builder.Build();
app.UsePathBase("/minions");
app.MapBlazorHub("/minions");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();
app.UseResponseCompression();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
