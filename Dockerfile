#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN dotnet dev-certs https
WORKDIR /src
COPY ["travel-analyzer-dijkstra.csproj", "."]
RUN dotnet restore "./travel-analyzer-dijkstra.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "travel-analyzer-dijkstra.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "travel-analyzer-dijkstra.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "travel-analyzer-dijkstra.dll"]