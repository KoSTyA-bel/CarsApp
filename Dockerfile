#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarsApp/CarsApp.csproj", "CarsApp/"]
COPY ["CarsApp.BusinessLogic/CarsApp.Businesslogic.csproj", "CarsApp.BusinessLogic/"]
COPY ["CarsApp.MongoDatabase/CarsApp.MongoDatabase.csproj", "CarsApp.MongoDatabase/"]
COPY ["CarsApp.DataAnnotation/CarsApp.DataAnnotation.csproj", "CarsApp.DataAnnotation/"]
RUN dotnet restore "CarsApp/CarsApp.csproj"
COPY . .
WORKDIR "/src/CarsApp"
RUN dotnet build "CarsApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarsApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarsApp.dll"]