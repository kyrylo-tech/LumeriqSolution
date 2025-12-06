# 🌐 LumeriqSolution

API доступне за адресою:

https://api.lumeriq.com
https://api.lumeriq.com/swagger/index.html

---

## 📁 .env файл

**Path:**  
`LumeriqSolution / WebAPI / .env`

Проєкт використовує `.env` для конфігурації Redis та PostgreSQL.  
Підтримуються два типи URL для кожного сервісу:

- стандартні URI (Railway / Supabase / Render)
- класичні connection strings

---

## 🔥 Приклад `.env`

```env
# Redis connection

## 1. URI-формат (Railway / Supabase / Render)
REDIS_URL="redis://default:password@host:port"

## 2. Класичний формат (ConnectionMultiplexer)
REDIS_URL="host:port,password=password,ssl=True,abortConnect=False"

# PostgreSQL connection

## 1. URI-формат (Railway / Supabase / Render)
# DATABASE_URL="postgresql://postgres:password@host:port/db_name"

## 2. Класичний формат Npgsql
DATABASE_URL="Host=;Port=;Database=;Username=;Password="
```