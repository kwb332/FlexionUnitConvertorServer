FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FlexionUnitConvertor/app/src/Services/Report/Flexion.Report.API/Flexion.Report.API.csproj", "FlexionUnitConvertor/app/src/Services/Report/Flexion.Report.API/"]
RUN dotnet restore "FlexionUnitConvertor/app/src/Services/Report/Flexion.Report.API/Flexion.Report.API.csproj"
COPY . .
WORKDIR "/src/FlexionUnitConvertor/app/src/Services/Report/Flexion.Report.API"
RUN dotnet build "Flexion.Report.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Flexion.Report.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Flexion.Report.API.dll"]