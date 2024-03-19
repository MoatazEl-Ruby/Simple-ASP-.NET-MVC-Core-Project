#!/bin/sh
# Wait for the database to be ready
until dotnet ef database update; do
  echo "SQL Server is unavailable - sleeping"
  sleep 10
done

# Continue with the application startup
exec dotnet "ASP.NET Lab 4.dll"
