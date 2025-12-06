using Logic;
using DotNetEnv;
using Logic.Services;
using Microsoft.EntityFrameworkCore;

Env.Load(".env");

var redisUrl = Env.GetString("REDIS_URL");
var databaseUrl = Env.GetString("DATABASE_URL");

Console.WriteLine($"Redis URL: {redisUrl}");
Console.WriteLine($"Database URL: {databaseUrl}");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRedis(redisUrl);
builder.Services.AddAppData(databaseUrl);
builder.Services.AddAppServices();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Services.CreateScope()
    .ServiceProvider
    .GetRequiredService<ApplicationDbContext>()
    .Database.Migrate();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();