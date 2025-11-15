FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY Ordering.Core/Ordering.Core.csproj Ordering.Core/
COPY Ordering.Application/Ordering.Application.csproj Ordering.Application/
COPY Ordering.Infrastructure/Ordering.Infrastructure.csproj Ordering.Infrastructure/
COPY Ordering.API/Ordering.API.csproj Ordering.API/

RUN dotnet restore Ordering.API/Ordering.API.csproj

# Copy source
COPY Ordering.Core/ Ordering.Core/
COPY Ordering.Application/ Ordering.Application/
COPY Ordering.Infrastructure/ Ordering.Infrastructure/
COPY Ordering.API/ Ordering.API/

RUN dotnet publish Ordering.API/Ordering.API.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Ordering.API.dll"]
