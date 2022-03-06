#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln .
COPY CarsApp/*.csproj ./CarsApp/
COPY CarsApp.BusinessLogic/*.csproj ./CarsApp.BusinessLogic/
COPY CarsApp.DataAnnotation/*.csproj ./CarsApp.DataAnnotation/
RUN dotnet restore

COPY CarsApp/. ./CarsApp/
COPY CarsApp.BusinessLogic/. ./CarsApp.BusinessLogic/
COPY CarsApp.DataAnnotation/. ./CarsApp.DataAnnotation/

WORKDIR "/src/CarsApp"
RUN dotnet build "CarsApp.csproj" -c Release -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "CarsApp.dll"]