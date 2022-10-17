#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Help/Help.csproj", "Help/"]
RUN dotnet restore "Help/Help.csproj"
COPY . .
WORKDIR "/src/Help"
RUN dotnet build "Help.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Help.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Help.dll"]