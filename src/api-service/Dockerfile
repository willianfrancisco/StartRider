FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["/Adapters/Primary/StartRiderApi/StartRiderApi.csproj","/src/Adapters/Primary/StartRiderApi/"] 
COPY ["/Adapters/Secondary/Infra/DataMySql/DataMySql.csproj","/src/Adapters/Secondary/Infra/DataMySql/"]
COPY ["/Adapters/Secondary/Infra/LoggerService/LoggerService.csproj","/src/Adapters/Secondary/Infra/LoggerService/"]
COPY ["/Adapters/Secondary/Infra/RabbitMqConsumerService/RabbitMqConsumerService.csproj","/src/Adapters/Secondary/Infra/RabbitMqConsumerService/"]
COPY ["/Adapters/Secondary/Infra/RabbitMqPublishService/RabbitMqPublishService.csproj","/src/Adapters/Secondary/Infra/RabbitMqPublishService/"] 
COPY ["/Core/Application/Application.csproj","/src/Core/Application/"]
COPY ["/Core/Domain/Domain.csproj","/src/Core/Domain/"] 

RUN dotnet restore "/src/Adapters/Primary/StartRiderApi/StartRiderApi.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "/src/Adapters/Primary/StartRiderApi/StartRiderApi.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "/src/Adapters/Primary/StartRiderApi/StartRiderApi.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","StartRiderApi.dll"]