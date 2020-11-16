FROM mcr.microsoft.com/dotnet/runtime:5.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src

COPy ./Common/Common.Domain /src/Common/Common.Domain
COPY ./Common/Communication /src/Common/Communication
COPY ./EmailProcessor /src/EmailProcessor

WORKDIR /src/EmailProcessor/EmailProcessor.Tests
RUN dotnet restore "EmailProcessor.Tests.csproj" --disable-parallel
RUN dotnet test

WORKDIR /src/EmailProcessor/EmailProcessor.API

RUN dotnet restore "EmailProcessor.API" --disable-parallel

RUN dotnet publish "EmailProcessor.API" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EmailProcessor.Worker.dll"]