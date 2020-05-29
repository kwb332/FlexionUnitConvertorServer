#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FlexionUnitConvertor/app/src/Services/Gateway/Flexion.Gateway.API/Flexion.Gateway.API.csproj", "FlexionUnitConvertor/app/src/Services/Gateway/Flexion.Gateway.API/"]
RUN dotnet restore "FlexionUnitConvertor/app/src/Services/Gateway/Flexion.Gateway.API/Flexion.Gateway.API.csproj"
COPY . .
WORKDIR "/src/FlexionUnitConvertor/app/src/Services/Gateway/Flexion.Gateway.API"
RUN dotnet build "Flexion.Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Flexion.Gateway.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Flexion.Gateway.API.dll"]