
# Dev Test TaskApp

Project Overview - Summary of the solution and its components
Solution Structure - Visual directory tree of all projects
Technologies Used - List of frameworks and tools
Detailed Project Descriptions - Information about each component (API, Web App, Tests)
Getting Started Section - Prerequisites, installation, and how to run the application
Configuration Guide - Notes about settings files
Development Tips - Including testing and database migration information
The README is now live on your repository and provides clear documentation for anyone looking to understand, set up, or contribute to your task management application.

# Dev Test TaskApp

A comprehensive .NET solution for task management consisting of an API backend and MVC web application.

## Project Overview

This repository contains a complete task management system built with .NET, featuring:

- **DevTest_TaskApi** - RESTful API backend for task operations
- **DevTest_TasksWeb** - MVC web application for task management UI
- **DevTest_TaskApp.Test** - Unit tests for API functionality

## Solution Structure

Dev_Test_TaskApp/ ├── DevTest_TaskApp/ # REST API Project │ ├── Controllers/ # API Controllers │ ├── Data/ # Database context and configurations │ ├── Models/ # Data models │ ├── Services/ # Business logic services │ ├── Migrations/ # Entity Framework migrations │ ├── Properties/ # Project properties │ ├── Program.cs # Application startup configuration │ ├── appsettings.json # Configuration settings │ └── DevTest_TaskApi.csproj # Project file │ ├── DevTest_TasksWeb/ # MVC Web Application │ ├── Controllers/ # MVC Controllers │ ├── Models/ # View models │ ├── Views/ # Razor views │ ├── wwwroot/ # Static files (CSS, JS, images) │ ├── Properties/ # Project properties │ ├── Program.cs # Application startup configuration │ ├── appsettings.json # Configuration settings │ └── DevTest_TasksWeb.csproj # Project file │ ├── DevTest_TaskApp.Test/ # Unit Tests │ ├── UnitTest1.cs # Test cases │ └── DevTest_TaskApp.Test.csproj # Test project file │ └── DevTest_TaskApp.sln # Solution file

Code

## Technologies Used

- **.NET** - Application framework
- **C#** - Programming language
- **ASP.NET Core** - Web framework for both API and MVC
- **Entity Framework Core** - ORM for database access
- **xUnit / MSTest** - Testing framework

## Projects

### DevTest_TaskApi
REST API backend providing task management operations.

**Features:**
- CRUD operations for tasks
- RESTful endpoints
- Database integration via Entity Framework Core
- Configuration-driven settings

**Key Files:**
- `Program.cs` - API configuration and startup
- `appsettings.json` - Environment-specific settings
- `Controllers/` - API endpoint handlers

### DevTest_TasksWeb
MVC web application providing a user interface for task management.

**Features:**
- Task management UI
- MVC architecture with separated concerns
- Integration with the API
- Static assets (CSS, JavaScript, images)

**Key Files:**
- `Program.cs` - Web application configuration and startup
- `Controllers/` - Request handlers
- `Views/` - Razor view templates
- `Models/` - View models
- `wwwroot/` - Client-side assets

### DevTest_TaskApp.Test
Unit test project for API validation and business logic verification.

**Contents:**
- Unit tests for core functionality
- Test configuration and setup

## Getting Started

### Prerequisites
- .NET SDK (version compatible with project files)
- Visual Studio or Visual Studio Code
- Git

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/mws980/Dev_Test_TaskApp.git
   cd Dev_Test_TaskApp
Restore NuGet packages:

bash
dotnet restore
Configure database settings in appsettings.json files for both projects

Apply database migrations 

bash
dotnet ef database update
Running the Application
API Backend
bash
cd DevTest_TaskApp
dotnet run
API will be available at configured endpoint (check launchSettings.json)

Web Application
bash
cd DevTest_TasksWeb
dotnet run
Web application will be available at configured endpoint

Running Tests
bash
cd DevTest_TaskApp.Test
dotnet test
Configuration
Each project includes appsettings.json for configuration:

DevTest_TaskApp/appsettings.json - API configuration
DevTest_TasksWeb/appsettings.json - Web app configuration
appsettings.Development.json - Development-specific overrides
Development
Testing Endpoints
The DevTest_TaskApp/DevTest_TaskApp.http file contains HTTP request definitions for testing API endpoints.

Database
Database migrations are managed in DevTest_TaskApp/Migrations/. To add new migrations:

bash
dotnet ef migrations add MigrationName
dotnet ef database update

Author
Created by mws980

