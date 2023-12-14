using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Minions.Data;
using Minions.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddMinionsData();

var app = builder.Build();
app.UsePathBase("/minions");
app.MapBlazorHub("/minions");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
