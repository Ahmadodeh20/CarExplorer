# Car Explorer 🚗

Car Explorer is a web application that allows users to search and explore vehicle information by selecting a car make, manufacture year, and vehicle type.

The application consumes the NHTSA Vehicle API to retrieve vehicle data and provides a simple user interface for browsing available models.

## Features

* Search vehicles by:

  * Car Make
  * Manufacture Year
  * Vehicle Type
* Display vehicle models in a paginated table
* Responsive user interface
* External API integration
* Memory caching to improve performance
* Global exception handling
* Structured logging using Serilog
* Dockerized application
* Hosted on AWS Elastic Beanstalk

## Technologies Used

### Backend

* ASP.NET Core (.NET 10)
* Dependency Injection
* HttpClient
* Memory Cache
* Serilog

### Frontend

* Razor Views
* Bootstrap
* JavaScript
* Font Awesome

### DevOps / Cloud

* Docker
* Docker Compose
* AWS Elastic Beanstalk

## External API

This project uses the NHTSA Vehicle API:

* Get all vehicle makes
* Get vehicle types by make
* Retrieve vehicle models

## Prerequisites

Before running the application locally, make sure you have:

* .NET 10 SDK
* Docker Desktop (optional)
* Visual Studio or any compatible IDE

## Running Locally

### Using .NET

Clone the repository:

```bash
git clone <repository-url>
```

Navigate to the project folder:

```bash
cd CarExplorer
```

Run the application:

```bash
dotnet run
```

The application will be available at:

```
http://localhost:5000
```

---

## Running with Docker

Build the Docker image:

```bash
docker build -t carexplorer .
```

Run the container:

```bash
docker run -p 8080:8080 carexplorer
```

The application will be available at:

```
http://localhost:8080
```

### Using Docker Compose

Run:

```bash
docker compose up --build
```

The application will be available at:

```
http://localhost:8080
```

---

## AWS Deployment

The application is deployed using:

* AWS Elastic Beanstalk
* Docker platform

Live Demo:

```
http://carexplorer-env.eba-vdpmzq9b.eu-north-1.elasticbeanstalk.com
```

## Project Structure

```
CarExplorer
│
├── Controllers
├── Models
│   ├── DTOs
│   └── Settings
├── Services
│   ├── Interfaces
│   └── Service
├── Middleware
├── Extensions
├── Views
└── wwwroot
```

## License

This project was developed as part of a technical assignment.
