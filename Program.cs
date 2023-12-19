using Microsoft.Extensions.Configuration;
using Temperature_Web.Interfaces;
using Temperature_Web.Services;

// шаблонный код из проекта ASP.NET Core Web App

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 
builder.Services.AddRazorPages();
// регистрация сервиса в контейнере зависимостей
builder.Services.AddScoped<ITemperatureService, TemperatureService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
