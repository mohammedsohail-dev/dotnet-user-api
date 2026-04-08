using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserApi.Models;
using UserApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind MongoDB settings
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

// Add UserService as singleton
builder.Services.AddSingleton<UserService>();   // <-- THIS LINE IS REQUIRED

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // <-- requires Swashbuckle package

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      // <-- show swagger.json
    app.UseSwaggerUI();    // <-- show swagger UI
}

app.UseAuthorization();
app.MapControllers();

app.Run();
