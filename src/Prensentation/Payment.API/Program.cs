using CorrelationId.AspNetCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add serilog
builder.Host.AddLogService();

// Add services
builder.Services.AddAspNetCoreServices();

// Add httpclient
builder.Services.AddHttpClient("Basket", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7001/");
});

builder.Services.AddHttpClient("Order", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7002/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAspNetCoreApplication();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();