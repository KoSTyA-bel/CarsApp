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
COPY CarsApp.MongoDatabase/*.csproj ./CarsApp.MongoDatabase/
RUN dotnet restore ./CarsApp/CarsApp.csproj

COPY CarsApp/. ./CarsApp/
COPY CarsApp.BusinessLogic/. ./CarsApp.BusinessLogic/
COPY CarsApp.DataAnnotation/. ./CarsApp.DataAnnotation/
COPY CarsApp.MongoDatabase/. ./CarsApp.MongoDatabase/

WORKDIR "/src/CarsApp"
RUN dotnet build "CarsApp.csproj" -c Release -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "CarsApp.dll"]