FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS runtime
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src

COPY ./Common/Common.Domain /src/Common/Common.Domain
COPY ./Common/Communication /src/Common/Communication
COPY ./Logger /src/Logger

WORKDIR /src/Logger/Logger.Tests
RUN dotnet test

WORKDIR /src/Logger/Logger.Worker

RUN dotnet publish "Logger.Worker.csproj" -c Release -o /app

FROM runtime AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Logger.Worker.dll"]