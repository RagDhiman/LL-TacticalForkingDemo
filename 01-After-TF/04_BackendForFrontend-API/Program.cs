using Microsoft.OpenApi.Models;
using Shop_BackendForFrontend_API.BaseAddresses;
using Shop_BackendForFrontend_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend For Frontend (BFF) API" });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAccountsAPIBaseAddress>(a => new AccountsAPIBaseAddress(builder.Configuration.GetValue<string>("AccountsAPIBaseAddress")));
builder.Services.AddScoped<IStockAPIBaseAddress>(a => new StockAPIBaseAddress(builder.Configuration.GetValue<string>("StockAPIBaseAddress")));
builder.Services.AddScoped<IOrdersAPIBaseAddress>(a => new OrdersAPIBaseAddress(builder.Configuration.GetValue<string>("OrdersAPIBaseAddress")));

builder.Services.AddScoped(typeof(IHTTPRepository<>), typeof(HTTPRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
