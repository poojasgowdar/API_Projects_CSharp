using Interfaces.Interface;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repositories.Repository;
using Services.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IService, ProductService>();
builder.Services.AddTransient<IRepository<Product>, ProductRepository<Product>>();

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".SessionTrackingApp"; 
    options.IdleTimeout = TimeSpan.FromMinutes(1);
    options.Cookie.IsEssential = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseSession();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
