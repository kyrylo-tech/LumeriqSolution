using Backend.Infrastructure.Cache;
using Backend.Infrastructure.Persistence;
using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// ── env -> configuration
Env.Load();
builder.Configuration.AddEnvironmentVariables();

// optional: скласти конекшн-строку з ENV
var envConn = Environment.GetEnvironmentVariable("CONNSTR__Default");
if (!string.IsNullOrWhiteSpace(envConn))
    builder.Configuration["ConnectionStrings:Default"] = envConn;

// ── services
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);   // DbContext
builder.Services.AddRedis(builder.Configuration);         // IConnectionMultiplexer
builder.Services.AddSingleton<IRedisCache, RedisCacheService>(); // твоя обгортка

// якщо хочеш ще й IDistributedCache, РЕЄСТРУЙ ДО Build():
// builder.Services.AddStackExchangeRedisCache(o =>
// {
//     o.Configuration = builder.Configuration.GetSection("Redis")["Configuration"];
//     o.InstanceName  = builder.Configuration.GetSection("Redis")["Instance"] ?? "lumeriq:";
// });

var app = builder.Build();

// ── pipeline
app.UseSwagger();
app.UseSwaggerUI();
app.MapOpenApi();

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Json(new
{
    Ok = true,
    Name = "Backend",
    Version = "1.0.0",
    Description = "This is the backend service.",
    Docs = "/swagger/index.html"
}));

app.MapGet("/health/redis", ([FromServices] IConnectionMultiplexer mux) =>
{
    var db = mux.GetDatabase();
    var latency = db.Ping();
    return Results.Ok(new { ok = true, latencyMs = latency.TotalMilliseconds });
});


// використовуємо твою обгортку
app.MapGet("/cache-test", async ([FromServices] IRedisCache cache) =>
{
    await cache.SetAsync("ping", "pong", TimeSpan.FromMinutes(5));
    var v = await cache.GetAsync<string>("ping");
    return Results.Ok(new { ok = v });
});

app.Run();