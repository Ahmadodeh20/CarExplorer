# CarExplorer

CarExplorer is a web application that allows users to search for vehicle information by selecting a car make, manufacture year, and vehicle type.

The application retrieves vehicle data from the NHTSA Vehicle API and displays available vehicle types and models based on user criteria.

## Technologies

- ASP.NET Core MVC (.NET 10)
- Entity Framework Core
- Docker
- Docker Compose
- NHTSA Vehicle API

## Features

- Browse available car makes
- Select manufacture year
- Retrieve vehicle types
- Retrieve vehicle models
- Responsive user interface
- Dockerized application

## Prerequisites

Before running the application, make sure you have the following installed:

- .NET 10 SDK
- Visual Studio 2026 (or any compatible IDE)
- Docker Desktop (for running with containers)


## Running Locally

1. Clone the repository:

```bash
git clone <repository-url>


## Running with Docker

Make sure Docker Desktop is installed and running.

### Build and Run Using Docker Compose

From the project root directory, run:

```bash
docker compose up