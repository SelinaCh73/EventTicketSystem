# Car Service System

Layered ASP.NET Core Web API for managing cars, customers, and selling cars, backed by MongoDB.
## Project Layout

CarService.sln Solution entry
Dockerfile API container image (optional)
docker-compose.yml API + MongoDB stack (optional)
global.json .NET SDK pin

src/
CarService.Host/ Presentation layer (ASP.NET Core)
Controllers/ HTTP endpoints (Cars, Customers)
Validators/ FluentValidation validators (AddCarRequest)
Healthchecks/ Custom health check for MongoDB
Program.cs Startup & DI wiring (Swagger, Serilog, health checks)
appsettings*.json Environment configuration

CarService.BL/ Business logic layer
Services/ Core workflows (CarCrudService, SellCar)
Interfaces/ Service contracts (ICarCrudService, ISellCar)

CarService.DL/ Data access layer
Interfaces/ Repository contracts (ICarRepository, ICustomerRepository)
Repositories/ LocalDb / Mongo-backed repositories
LocalDb/ StaticDb for testing
DependencyInjection.cs DI wiring for DL

CarService.Models/ Shared models & DTOs
Dto/ Car, Customer
Requests/ AddCarRequest
Responses/ SellCarResult
Configurations/ MongoDbConfiguration
StaticDataBase/ Sample static database for cars

tests/
CarService.Test/ xUnit tests for SellCar and business logic
SellCarTests.cs Happy-path + missing customer cases

tests/
  CarService.Test/              xUnit tests for car selling and business logic
    SellCarTests.cs             Happy-path + missing customer cases


## Architecture Highlights

- Clean layering: Host (API) depends on BL, which depends on DL; repositories isolate Mongo/StaticDb concerns.  
- Health & Swagger: `/healthz` returns JSON status; Swagger UI enabled by default for quick exploration.  
- Car flow: `CarsController -> CarCrudService -> Repositories`, enforcing business rules (valid car, deletion, addition).  
- Customer & Sell flow: `SellCarService -> CarCrudService + CustomerRepository`, calculates price with discount and ensures car & customer exist.  
- Data access: LocalDb or MongoDB used for storing cars and customers; BL services remain agnostic of data source.

## Running Locally (without Docker)

 Start MongoDB (local or container, optional if using LocalDb):
```bash
docker run -d --name mongo -p 27017:27017 mongo:7
dotnet run --project src/CarService.Host
Browse Swagger UI: http://localhost:5058/swagger

## Running with Docker Compose

```bash
docker-compose up --build   # API + Mongo
docker-compose down         # stop

Configuration

appsettings.Development.json (overrides appsettings.json) carries sensible defaults. Key settings:

MongoDbConfiguration:ConnectionString, MongoDbConfiguration:DatabaseName

You can also configure logging levels and other app settings here.

Environment variables override the same keys in containerized runs.

## API Surface (default port 5058)

- `GET  /healthz`                     Mongo health check
- `GET  /api/cars`                    list all cars
- `GET  /api/cars/{id}`               fetch car by ID
- `POST /api/cars`                     add new car
- `DELETE /api/cars?id={id}`          delete car by ID

- `GET  /api/customers`               list all customers
- `GET  /api/customers/{id}`          fetch customer by ID
- `POST /api/customers`                add new customer
- `DELETE /api/customers?id={id}`     delete customer by ID

- `POST /api/sellcar`                  sell a car `{ carId, customerId }`
## Testing

```bash
dotnet test

##Tech Stack

.NET 9, ASP.NET Core Web API

MongoDB (MongoDB.Driver) or static in-memory DB for testing

xUnit + Moq for unit testing

Serilog for logging

Docker / Docker Compose for containerized runs (optional)

FluentValidation for request validation

Mapster for DTO mapping