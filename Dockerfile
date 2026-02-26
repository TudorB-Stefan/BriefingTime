FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["BriefingTime.Api/BriefingTime.Api.csproj", "BriefingTime.Api/"]
COPY ["BriefingTime.Core/BriefingTime.Core.csproj", "BriefingTime.Core/"]
COPY ["BriefingTime.Infrastructure/BriefingTime.Infrastructure.csproj", "BriefingTime.Infrastructure/"]

RUN dotnet restore "BriefingTime.Api/BriefingTime.Api.csproj"

COPY . .
WORKDIR "/src/BriefingTime.Api"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BriefingTime.Api.dll"]