# Stocks API

A full-stack web application built with ASP.Net Core, Entity Framework Core, SQL Server, and JWT authentication.  
Users can browse, comment, and add stocks to their personal portfolios.

---

## Table of Contents

1. [Getting Started](#getting-started)
2. [Running the Application](#running-the-application)
   - [Docker Compose (Recommended)](#docker-compose-recommended)
   - [Docker Only](#docker-only)
   - [Local Development](#local-development)
3. [Configuration](#configuration)
4. [Viewing the App](#viewing-the-app)
5. [Useful Commands](#useful-commands)
6. [Future Improvements](#future-improvements)

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Ryanfox123/c--api
cd api
```

### 2. **Installations**

Local development requires:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)

Docker development requires:

- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### 3.1 Running app and DB (Docker) - Recommended

This will start both the API and SQL Server containers:

```

docker compose up --build

```

- API will be available at: http://localhost:5050/swagger

- SQL Server is accessible internally by the service name sqlserver.

Using Docker Compose ensures the database is included automatically, making setup easier.

### 3.2 **Running the app (Docker)**

Build the Docker image:

```

docker build -t portfolio-api .

```

Run the container:

```

docker run -p 5050:5050 portfolio-api

```

Note: This method requires that a database is already accessible. Docker Compose is preferred for local development as it includes SQL Server automatically.

---

### 3.3 **Running the app (Locally)**

Restore dependencies

```

dotnet restore

```

Set up databases

```

dotnet ef database update

```

Run the app

```

dotnet watch run

```

### **4. Viewing the app**

Here you can view the endpoints of the app:
https://localhost:5050/swagger

## Configuration

To configure your database connection you will want to edit your appsettings.json to include your connection string. for example:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=TestDB;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
  },
```

Also you will need to include this connection string in your Docker-compose.yaml:

```yaml
- sqlserver
    environment:
      ConnectionStrings__DefaultConnection: "Server=sqlserver;Database=MyDb;User Id=sa;Password=YourStrong!Passw0rd;Encrypt=False;TrustServerCertificate=True;"
      ASPNETCORE_ENVIRONMENT: Development
```

---

## Useful commands

Restore dependencies

```
dotnet restore
```

Add new EF Core migration

```
dotnet ef migrations add <MigrationName>
```

Apply migrations / update database

```
dotnet ef database update
```

Run API locally with hot reload

```
dotnet watch run
```

Build Docker image

```
docker build -t portfolio-api .
```

Run Docker container

```
docker run -p 5050:5050 portfolio-api
```
