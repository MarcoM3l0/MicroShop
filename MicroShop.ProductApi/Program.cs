using MicroShop.ProductApi.Context;
using MicroShop.ProductApi.DTOs.Mappings;
using MicroShop.ProductApi.Repositories;
using MicroShop.ProductApi.Services.concrete;
using MicroShop.ProductApi.Services.interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string message = "A string de conexão 'DefaultConnection' não foi encontrada ou está inválida.";

string mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new ArgumentNullException(nameof(mysqlConnection), message);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mysqlConnection, ServerVersion.AutoDetect(mysqlConnection)));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

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
