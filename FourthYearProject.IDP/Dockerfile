#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 44365
EXPOSE 44333

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["FourthYearProject.IDP.csproj", ""]
RUN dotnet restore "./FourthYearProject.IDP.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "FourthYearProject.IDP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FourthYearProject.IDP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FourthYearProject.IDP.dll"]