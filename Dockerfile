FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["BookPlace.Api/BookPlace.Api.csproj", "BookPlace.Api/"]
COPY ["BookPlace.Core/BookPlace.Core.csproj", "BookPlace.Core/"]
COPY ["BookPlace.Infrastructure/BookPlace.Infrastructure.csproj", "BookPlace.Infrastructure/"]

RUN dotnet restore "BookPlace.Api/BookPlace.Api.csproj"

COPY . .
WORKDIR "/src/BookPlace.Api"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BookPlace.Api.dll"]