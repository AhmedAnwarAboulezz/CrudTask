# CrudTask

A full-stack CRUD application built with **ASP.NET Core 8**, **Angular 19**, and **Google OAuth SSO**.

## Architecture

- **Backend**: Clean Architecture + DDD + CQRS (MediatR), PostgreSQL, EF Core 8
- **Frontend**: Angular 19 (standalone), Angular Material, SCSS
- **Auth**: Google OAuth 2.0 (PKCE code flow) — ID token validated as JWT Bearer on the API

```
CrudTask/
├── src/
│   ├── CrudTask.Domain/          # Entities, value objects, interfaces, exceptions
│   ├── CrudTask.Application/     # CQRS commands/queries, validators, pipeline behaviors
│   ├── CrudTask.Infrastructure/  # EF Core, PostgreSQL, repositories, migrations
│   └── CrudTask.API/             # ASP.NET Core controllers, middleware, JWT auth
└── CrudTask.Client/              # Angular 19 SPA
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [Node.js 18+](https://nodejs.org/)
- [PostgreSQL](https://www.postgresql.org/)
- A Google OAuth **Desktop app** client (see setup below)

## Setup

### 1. Google OAuth

1. Go to [Google Cloud Console](https://console.cloud.google.com/) → APIs & Services → Credentials
2. Create Credentials → **OAuth Client ID** → **Desktop app**
3. Copy the **Client ID** and **Client Secret**

### 2. Backend configuration

Copy the example file and fill in your values:

```bash
cp src/CrudTask.API/appsettings.Development.json.example src/CrudTask.API/appsettings.Development.json
```

Edit `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=CrudTaskDb;Username=postgres;Password=YOUR_DB_PASSWORD"
  },
  "Auth": {
    "Audience": "YOUR_GOOGLE_CLIENT_ID"
  }
}
```

### 3. Frontend configuration

Copy the example file and fill in your values:

```bash
cp CrudTask.Client/src/environments/environment.local.ts.example CrudTask.Client/src/environments/environment.local.ts
```

Edit `environment.local.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7001',
  auth: {
    issuer: 'https://accounts.google.com',
    clientId: 'YOUR_GOOGLE_CLIENT_ID',
    clientSecret: 'YOUR_GOOGLE_CLIENT_SECRET',
  },
};
```

### 4. Apply database migrations

```bash
dotnet ef database update --project src/CrudTask.Infrastructure --startup-project src/CrudTask.API
```

## Running

### Option A — Local (without Docker)

#### API

```bash
dotnet run --project src/CrudTask.API --launch-profile "Development (https)"
```

Swagger UI: `https://localhost:7001/swagger`

#### Angular dev server

```bash
cd CrudTask.Client
node node_modules/@angular/cli/bin/ng.js serve --open
```

App: `http://localhost:4200`

---

### Option B — Docker Compose

#### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

No local .NET SDK, Node.js, or PostgreSQL required.

#### Configuration

All secrets and settings are passed as **environment variables** in `docker-compose.yml`, overriding `appsettings.json`. `appsettings.Development.json` is excluded from the image via `.dockerignore`.

Before first run, complete the following two steps:

**Step 1 — Frontend: update `CrudTask.Client/src/environments/environment.docker.ts`** with your Google OAuth credentials:

```typescript
export const environment = {
  production: true,
  apiUrl: 'http://localhost:5211',
  auth: {
    issuer: 'https://accounts.google.com',
    clientId: 'YOUR_GOOGLE_CLIENT_ID',       // <-- replace with your Client ID
    clientSecret: 'YOUR_GOOGLE_CLIENT_SECRET' // <-- replace with your Client Secret
  },
};
```

**Step 2 — Backend: update the following values in `docker-compose.yml`** under the `api` and `migrate` services:

| Variable | Description |
|---|---|
| `Auth__Audience` | Your Google OAuth Client ID |
| `ConnectionStrings__DefaultConnection` | DB credentials (defaults use `postgres`/`postgres`) |
| `Cors__AllowedOrigins` | Frontend origin (default `http://localhost:4200`) |

#### First time setup

```bash
# 1. Apply database migrations
docker compose run --rm migrate

# 2. Build images and start all services
docker compose up --build
```

#### Ports

| Service | URL |
|---|---|
| Angular SPA | `http://localhost:4200` |
| API | `http://localhost:5211` |
| Swagger UI | `http://localhost:5211/swagger` |
| PostgreSQL | `localhost:5432` |

#### Common commands

```bash
# Start everything (rebuild changed images)
docker compose up --build

# Start in background
docker compose up --build -d

# Apply new EF migrations after adding one
docker compose run --rm migrate

# Stop all containers (DB data is preserved)
docker compose down

# Stop and wipe all data (full reset)
docker compose down -v

# Force full rebuild from scratch
docker compose build --no-cache
docker compose up
```

## Features

- **Authentication**: Sign in with Google — redirects to `/products` after login
- **Products CRUD**: Create, read, update, delete products with name, description, price (with currency), and category
- **Validation**: Server-side (FluentValidation) + client-side (Angular reactive forms)
- **Authorization**: All API endpoints require a valid Google ID token as Bearer token
