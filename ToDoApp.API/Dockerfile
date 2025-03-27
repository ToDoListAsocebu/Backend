# Etapa base con ASP.NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa de compilación con el SDK de .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar la solución y restaurar dependencias
COPY *.sln ./
COPY ToDoApp.API/*.csproj ToDoApp.API/
COPY ToDoApp.Application/*.csproj ToDoApp.Application/
COPY ToDoApp.Domain/*.csproj ToDoApp.Domain/
COPY ToDoApp.Infrastructure/*.csproj ToDoApp.Infrastructure/
RUN dotnet restore

# Copiar todo el código y compilar
COPY . .
WORKDIR /app/ToDoApp.API
RUN dotnet publish -c Release -o /out

# Etapa final con la imagen base
FROM base AS final
WORKDIR /app
COPY --from=build /out . 
ENTRYPOINT ["dotnet", "ToDoApp.API.dll"]
