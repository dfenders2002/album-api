  # syntax=docker/dockerfile:1
  FROM mcr.microsoft.com/dotnet/aspnet:5.0
  COPY ["Album.Api/Album.Api.csproj", "Album.Api/"]
  WORKDIR /App
  ENTRYPOINT ["dotnet", "Album-Api.dll"]
