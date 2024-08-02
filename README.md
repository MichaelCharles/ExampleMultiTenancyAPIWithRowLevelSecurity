# Example Multi-Tenancy API with Row-Level Security

This project demonstrates a multi-tenant API that uses row-level security in PostgreSQL to ensure data isolation between tenants. The API is built with C# and .NET Core, using Entity Framework Core for database interactions. This API was prepared as a reproducible example of a problem we're facing to be submitted along with a GitHub issue.

## Key Features

- Multi-tenant architecture
- PostgreSQL database with row-level security
- Entity Framework Core ORM
- JWT-based tenant identification (simulated in this example)
- JSONB column usage for flexible data storage

## Setup and Running

1. Start the database: `docker-compose up` in the root directory
2. Run the API: `dotnet run` in the root directory

The `init.sql` file sets up the database schema, dummy data, row-level security, and users.

## GitHub Issue

https://github.com/npgsql/efcore.pg/issues/3239
