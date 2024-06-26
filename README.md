# README for Task Management Application
by **Gabriel Brancolini**

## Overview
This Task Management Application is designed to create, read, update, and delete tasks. The application includes a backend API built with ASP.NET Core and Entity Framework Core, a frontend interface with HTML and JavaScript, and a comprehensive suite of unit tests.

## Project Structure

TaskManagement/
│
├── TaskManagement.Api/           # ASP.NET Core Web API
│   ├── Controllers/              # API Controllers
│   ├── Properties/               # Project Properties
│   ├── wwwroot/                  # Static files (HTML, CSS, JS)
│   ├── Program.cs                # Main entry point
│   └── TaskManagement.Api.csproj # Project file
│
├── TaskManagement.Services/      # Business logic layer
│   ├── TaskService.cs            # Service implementation
│   └── TaskManagement.Services.csproj
│
├── TaskManagement.Repositories/  # Data access layer
│   ├── TaskRepository.cs         # Repository implementation
│   ├── TaskDbContext.cs          # EF Core DbContext
│   └── TaskManagement.Repositories.csproj
│
├── TaskManagement.Models/        # Data models
│   ├── Task.cs                   # Task model
│   └── TaskManagement.Models.csproj
│
├── TaskManagement.Contracts/     # Interfaces and contracts
│   ├── ITaskRepository.cs        # Repository interface
│   ├── ITaskService.cs           # Service interface
│   └── TaskManagement.Contracts.csproj
│
├── TaskManagement.Services.Tests/ # Unit tests for services
│   ├── TaskServiceTests.cs       # Service tests
│   └── TaskManagement.Services.Tests.csproj
│
└── TaskManagement.Repositories.Tests/ # Unit tests for repositories
    ├── TaskRepositoryTests.cs    # Repository tests
    └── TaskManagement.Repositories.Tests.csproj



## Setting Up and Running the Application

### Clone the repository:

**git clone https://github.com/gbrancolini/taskmanagement.git**
**cd taskmanagement**

### Database Initialization
The application uses SQL Server for data storage. Follow these steps to initialize the database:

1. Ensure SQL Server is installed and running on your machine or a remote server.

2. Run the ___init.sql___ script located in the __migration__ folder to create the necessary table:

* Open SQL Server Management Studio (SSMS) or any SQL query tool of your choice.
* Connect to your SQL Server instance.
* Open the init.sql script located in the migrations directory.
* Execute the script.

3. Update the connection string in the appsettings.json file of your API project to connect to your SQL Server instance:

* Open the ___appsettings.json___ file located in the TaskManagement.Api project directory.
* Update the ___ConnectionStrings___ section with your SQL Server connection details. Replace YourServerName, YourDatabaseName, YourUsername, and YourPassword with your actual server name, database name, username, and password.


    "ConnectionStrings": {
        "DefaultConnection": "Server=YourServerName;Database=YourDatabaseName;User Id=YourUsername;Password=YourPassword;TrustServerCertificate=True;"
    }


If you prefer to use integrated security (Windows Authentication), update the connection string as follows:


    "ConnectionStrings": {
        "DefaultConnection": "Server=YourServerName;Database=YourDatabaseName;Trusted_Connection=True;TrustServerCertificate=True;"
    }


#### Example of appsettings.json Update

    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=TaskManagementDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
    }


### Running the Application
Once the database is initialized and the connection string is updated, you can run the application using the following commands:
1. **Build and run the application:**
dotnet build
dotnet run --project TaskManagement.Api

2. **Access the application:**
* Frontend: http://localhost:8080
* API Documentation (Swagger): http://localhost:8080/swagger

## Testing
The solution includes unit tests for both the service and repository layers.

* **TaskManagement.Services.Tests**: Contains unit tests for the service layer.
* **TaskManagement.Repositories.Tests**: Contains unit tests for the repository layer.

To run the tests, use the following command:

**dotnet test**

## Frontend Functionality
The frontend is built with HTML and JavaScript. It provides the following features:

* **Task List**: Displays all tasks in a table format.
* **Add Task**: A form to add new tasks.
* **Edit Task**: Inline editing for existing tasks.
* **Delete Task**: Removes tasks from the list.

The JavaScript file (script.js) handles the interaction with the backend API using the Fetch API for AJAX requests.

## API Endpoints
* **GET /api/tasks**: Retrieve all tasks.
* **GET /api/tasks/{id}**: Retrieve a task by ID.
* **POST /api/tasks**: Add a new task.
* **PUT /api/tasks/{id}**: Update an existing task.
* **DELETE /api/tasks/{id}**: Delete a task by ID.

## Styles
The frontend is styled with custom CSS to ensure a clean and modern look. The styles are located in wwwroot/css/styles.css.
