# ---------- build ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /repo

# Restore NuGet packages (layer-cached)
COPY ["src/CrudTask.API/CrudTask.API.csproj", "src/CrudTask.API/"]
COPY ["src/CrudTask.Application/CrudTask.Application.csproj", "src/CrudTask.Application/"]
COPY ["src/CrudTask.Domain/CrudTask.Domain.csproj", "src/CrudTask.Domain/"]
COPY ["src/CrudTask.Infrastructure/CrudTask.Infrastructure.csproj", "src/CrudTask.Infrastructure/"]
RUN dotnet restore "src/CrudTask.API/CrudTask.API.csproj"

# Copy everything and publish
COPY . .
RUN dotnet publish "src/CrudTask.API/CrudTask.API.csproj" -c Release -o /app/publish --no-restore

# ---------- migrate ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migrate
WORKDIR /repo
RUN dotnet tool install --global dotnet-ef --version 8.*
ENV PATH="${PATH}:/root/.dotnet/tools"
COPY ["src/CrudTask.API/CrudTask.API.csproj", "src/CrudTask.API/"]
COPY ["src/CrudTask.Application/CrudTask.Application.csproj", "src/CrudTask.Application/"]
COPY ["src/CrudTask.Domain/CrudTask.Domain.csproj", "src/CrudTask.Domain/"]
COPY ["src/CrudTask.Infrastructure/CrudTask.Infrastructure.csproj", "src/CrudTask.Infrastructure/"]
RUN dotnet restore "src/CrudTask.API/CrudTask.API.csproj"
COPY src/ src/
ENTRYPOINT ["dotnet", "ef", "database", "update", \
            "--project", "src/CrudTask.Infrastructure", \
            "--startup-project", "src/CrudTask.API"]

# ---------- runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "CrudTask.API.dll"]
