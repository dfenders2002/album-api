  # syntax=docker/dockerfile:1
  #FROM mcr.microsoft.com/dotnet/aspnet:5.0
  #COPY ["Album.Api/Album.Api.csproj", "Album.Api/"]
  #WORKDIR /App
  #ENTRYPOINT ["dotnet", "Album-Api.dll"]


#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Album.Api/Album.Api.csproj", "Album.Api/"]
RUN dotnet restore "Album.Api/Album.Api.csproj"
COPY . .
WORKDIR "/src/Album.Api"
RUN dotnet build "Album.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Album.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Album.Api.dll"]