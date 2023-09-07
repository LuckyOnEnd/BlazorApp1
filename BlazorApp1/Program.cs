using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2V1hhQlJDfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5UdkBjXHpXcnxWRWFa;MjU0NDg0OEAzMjMyMmUzMDJlMzBvQW9qWEJEVDQ4UWNHQ3lseWU2Smx5S0gxT2NCSUJFYnpBMHBZbWN5azljPQ==");

builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
