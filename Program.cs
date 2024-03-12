using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using TemporalBox.Interfaces;
using TemporalBox.Models;
using Microsoft.AspNetCore.Identity;
using TemporalBox.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IProducts,Products>();
builder.Services.AddScoped<ICart,CartService>();
builder.Services.AddDirectoryBrowser();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(10);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:44366") 
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .WithExposedHeaders("Content-Disposition") 
                   .SetIsOriginAllowed((host) => true); 
        }); 
});

//adding policy authorization

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("admin"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
    RequestPath = "/images"
});
app.UseSession();
app.UseRouting();
app.UseCors("MyAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
