using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using accountmetrics.Context;
using Microsoft.Extensions.DependencyInjection;
using accountmetrics.Repositories;
using accountmetrics.Repositories.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 🔹 Register the database context (replaces `ConfigureServices`)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

 builder.Services.AddIdentity<IdentityUser, IdentityRole>()
     .AddEntityFrameworkStores<AppDbContext>()
     .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    // Default password settings
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 1;}

);

// Adicionando o serviço dos repositorios
builder.Services.AddTransient<ICompraRepository, CompraRepository>();

// Permitindo o uso de sessão http no sistema
builder.Services.AddMemoryCache();
builder.Services.AddSession();

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

// Habilita o uso da sessão http
app.UseSession(); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
