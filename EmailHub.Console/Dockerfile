FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /app
COPY ./Common/Common.Domain /Common/Common.Domain
COPY ./Common/Communication /Common/Communication
COPY ./EmailProcessor/EmailProcessor.Contracts /EmailProcessor/EmailProcessor.Contracts
COPY ./Logger/Logger.Contracts /Logger/Logger.Contracts
COPY ./Gateway/Gateway.Domain /Gateway/Gateway.Domain
COPY ./EmailHub.Console /EmailHub.Console

WORKDIR /EmailHub.Console

RUN dotnet publish "EmailHub.Console.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "EmailHub.Console.dll"]