FROM mcr.microsoft.com/dotnet/aspnet:5.0.2 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src

COPY ./Common /src/Common
COPY ./EmailProcessor/EmailProcessor.Contracts /src/EmailProcessor/EmailProcessor.Contracts
COPY ./Logger/Logger.Contracts /src/Logger/Logger.Contracts
COPY ./Gateway /src/Gateway

WORKDIR /src/Common/Tests/Communication.Tests
RUN dotnet restore "Communication.Tests.csproj"
RUN dotnet test "Communication.Tests.csproj"

WORKDIR /src/Gateway/Gateway.Tests
RUN dotnet test "Gateway.Tests.csproj"

WORKDIR /src/Common/Tests/Communication.Tests
RUN dotnet test

WORKDIR /src/Gateway/Gateway.API
RUN dotnet publish "Gateway.API.csproj" -c Release -o /app

FROM runtime AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Gateway.API.dll"]
