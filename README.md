# A simple Rest API for managing a list of users
# Features
- Create, read, update, and delete users
- In-memory data 
- Unit-testable architecture using dependency injection
- Postman-compatible endpoints

# Endpoints
| Method        | Endpoint            | Description                   |
| ------------- | -------------       |-------------                  |
| GET           | /simple/users       |  Get all users                |
| GET           | /simple/users/{id}  |  Get user by ID               |
| POST          | /simple/users       |  Add new users (list of users)|
| PUT           | /simple/users/{id}  |  Update user by ID            |
| DELETE        | /simple/users/{id}  |  Delete user by ID            |

# Sample Request
```
POST /simple/users
[
  {
    "name": "John",
    "surname": "Doe",
    "address": "123 Main St",
    "phone": "123456789"
  }
]
```

# Getting Started
1. Clone the repo
2. Open SimpleRestAPI.sln from the root directory
3. Build the project
4. Open Postman and test the API via https://localhost:{port}/simple/users
5. Test can be found under SimpleTest or running **dotnet test**

# Architecture
- Controllers: Handle incoming HTTP requests
- Services: Logic layer (via IService)
- Models: Plain old C# classes for user data
- Dependency Injection: Configured in Program.cs using builder.Services.AddSingleton<IService, Service>()

# License
MIT License

