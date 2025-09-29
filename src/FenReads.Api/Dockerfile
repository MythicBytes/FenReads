# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY src/FenReads.Api/FenReads.Api.csproj src/FenReads.Api/
COPY src/FenReads.Application/FenReads.Application.csproj src/FenReads.Application/
COPY src/FenReads.Domain/FenReads.Domain.csproj src/FenReads.Domain/
COPY src/FenReads.Infrastructure/FenReads.Infrastructure.csproj src/FenReads.Infrastructure/

# Restore dependencies for the API project only
RUN dotnet restore src/FenReads.Api/FenReads.Api.csproj

# Copy source code
COPY src/ src/

# Build the API project
WORKDIR /src/src/FenReads.Api
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Create non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Copy published app
COPY --from=publish /app/publish .

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

# Environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "FenReads.Api.dll"]