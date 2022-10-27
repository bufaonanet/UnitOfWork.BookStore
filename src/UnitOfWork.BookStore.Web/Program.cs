using UnitOfWork.BookStore.Data.EFCore.Context;
using UnitOfWork.BookStore.Data.EFCore.Repositories;
using UnitOfWork.BookStore.Domain.Interfaces;
using UnitOfWork.BookStore.Domain.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork.BookStore.Data.EFCore.UoW.UnitOfWork>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
