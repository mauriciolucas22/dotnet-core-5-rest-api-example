# Intro

Hello, thank you for visit my repository example. This project is a example DotNet Core 5 Rest API.

## SETUP

```bash
# Resolve dependencies
dotnet restore
```

## Edit file appsettings.json

> 👉 Setup your credentials database in "DefaultConnection"

```json
...
"ConnectionStrings": {
    "DefaultConnect": "server=localhost;port=3306database=MY_DATABASE_NAME;uid=MY_USER_DB;pwd=MY_PASSWORD;"
  }
  ...
```

## RUN

```bash
# Migrate
dotnet ef database update -v

# Start
dotnet run
```

## TRY

> 👉 open url on your browser: https://localhost:5001/swagger/index.html

> ℹ NOTE: proceed with unsecure becouse https

## Do you create a new table? 🔨

```bash
# Install tool command line
dotnet tool install --global dotnet-ef
```

> Create a new file in Models folder

```cs
namespace dotnet_mvc.Models
{
    public class MyMTableName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
```

> Migrations

```bash
# Create migrations
dotnet ef migrations add MyMigrationName -v

# Migrate
dotnet ef database update -v
```

# HATEOAS

```

```

# Do you create a new project?

```bash
dotnet new webapi -o dotnet-rest-api
cd dotnet-rest-api
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 5.0.1
dotnet watch run
```

Jesus loves you! 🙌🙏
