using EzcaneBilgiSistemi.Models;
using EzcaneBilgiSistemi.Services;
using EzcaneBilgiSistemi.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Repositories.Implementations;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region DbContext
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DataConnection"));
    options.EnableSensitiveDataLogging(true);
}).AddScoped<DataContext>();
#endregion

#region Scoped
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IEczaneBilgileriRepository, EczaneBilgileriRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISehirRepository, SehirRepository>();
builder.Services.AddScoped<IBolgelerRepository, BolgelerRepository>();
builder.Services.AddScoped<IResmiTatillerRepository, ResmiTatillerRepository>();
builder.Services.AddScoped<IMazeretBilgileriRepository, MazeretBilgileriRepository>();
builder.Services.AddScoped<INobetlerRepository, NobetlerRepository>();
builder.Services.AddScoped<INobetDagilimRepository, NobetDagilimRepository>();
#endregion

builder.Services.AddDistributedMemoryCache(); // Bellek tabanl� oturum y�netimi i�in
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi 30 dakika
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<UygulamaAyarlar�>(builder.Configuration.GetSection("UygulamaAyarlar�"));
builder.Services.AddScoped<AyarlarServisi>();
builder.Services.AddScoped<NobetRaporServisi>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/LoginRegister/Index"; // Kullan�c� giri� yapmadan �nce y�nlendirilecek sayfa
            options.LogoutPath = "/LoginRegister/Index"; // Kullan�c� ��k�� yapt�ktan sonra y�nlendirilecek sayfa
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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LoginRegister}/{action=Index}/{id?}");

app.Run();
