# Email Hub
## Motive
* Microservices to serve the request in a asynchoronise way.
* Factory pattern to resolve the dependency at runtime.
* Fallback pattern to use other services during fallback.
* Impleimenting CQS(AKA CQRS).
* Implimanting Unit of Work pattern.
* Add minimum unit tests.
* Containerize deployment.

## System Requirement for Development
* .NET 5 SDK and Runtime
* EF Core Tools
* POSTGRES
* Visual Studio/Visual Studio Code
* Docker

## Runtime System Requirement
* Docker

## Envoirnment Setup
* Add servicebus primary connection string inside appsettings of ```Gateway->Gateway.Api->appsettings.json/appsettings.development.json```.
* Add servicebus primary connection string inside appsettings of ```EmailProcessor->EmailProcessor.Worker->appsettings.json/appsettings.development.json```.
* Add servicebus primary connection string inside appsettings of ```Logger->Logger.Worker->appsettings.json/appsettings.development.json```.
* Change email processor settings under EmailProcessor section of ```EmailProcessor->EmailProcessor.Worker->appsettings.json/appsettings.development.json```
* Set Sandbox value to false, when you have configured the dns of specific service. Otherwise you have to use sandobx.
* Default email will be used to send email under sandbox.
* Provide POSTGRES database connection string inside ```appsettings.json/appsettings.development.json``` of ```Logger->Logger.Worker``` folder.

## Executing Application
* Option 1. Use Gateway.Api, Logger.Worker and EmailProcessor.Worker as start-up project. And run the application from VS.
* Option 2. Go to the root folder of the project. And run ```docker-compose up```. Use ```docker-compose up -f docker-compose-slow-connection.yml``` if you face any issue to restore nuget packages.

### Running console application
* Execute ```docker build .\EmailHub.Console\Dockerfile . -t emailhub-console```.
* Execute ```docker run -i emailhub-console```.
* Make sure, EmailProcessor and Logger worker services are up and running.

## Architecture
![Email Hub Architecture](https://user-images.githubusercontent.com/24603959/99696370-d3c23000-2ab8-11eb-99d7-528e5283ba63.jpg)
