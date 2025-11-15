FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files (fast restore)
COPY User.Core/User.Core.csproj User.Core/
COPY User.Application/User.Application.csproj User.Application/
COPY User.Infrastructure/User.Infrastructure.csproj User.Infrastructure/
COPY User.API/User.API.csproj User.API/

RUN dotnet restore User.API/User.API.csproj 

# Copy all source code
COPY User.Core/ User.Core/
COPY User.Application/ User.Application/
COPY User.Infrastructure/ User.Infrastructure/
COPY User.API/ User.API/

# Publish
RUN dotnet publish User.API/User.API.csproj -c Release -o /app/publish


# ============================
# RUNTIME STAGE
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "User.API.dll"]
