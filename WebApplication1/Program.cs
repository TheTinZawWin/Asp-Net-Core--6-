using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Interfaces;
using WebApplication1.Data.Repositories;
using Microsoft.Extensions;
using WebApplication1.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Server configuration

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DrinkAndGo;Trusted_Connection=True;MultipleActiveResultSets=true"));
//Authentication, Identity config
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IDrinkRepository, DrinkRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => ShoppingCart.GetCart(sp));
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddMvc();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseSession();
//app.UseIdentity();


//app.UseMvc(routes =>
//{
//    routes.MapRoute(
//       name: "drinkdetails",
//       template: "Drink/Details/{drinkId?}",
//       defaults: new { Controller = "Drink", action = "Details" });

//    routes.MapRoute(
//        name: "categoryfilter",
//        template: "Drink/{action}/{category?}",
//        defaults: new { Controller = "Drink", action = "List" });

//    routes.MapRoute(
//        name: "default",
//        template: "{controller=Home}/{action=Index}/{Id?}");
//});


//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
//DbInitializer.Seed(app);