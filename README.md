# Simple CRUD App for Drivers Management

This project is a simple CRUD application developed using .NET Core 8 and Angular 17. It allows users to manage drivers by performing basic operations such as creating, editing, retrieving, and deleting driver records.

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)
## Introduction
The application provides a user-friendly interface for managing drivers and includes features such as:

- CRUD Operations: Create, Read, Update, and Delete driver records.
- Verticle Slice Architecture: The backend is implemented - using the verticle slice architecture to ensure modularity and maintainability.
- Swagger Documentation: The backend APIs are fully documented using Swagger for easy integration and testing.
- Upload File Component: Additionally, the Angular frontend includes an upload file component for uploading text files, which can be useful for various purposes.
## Demo

https://github.com/Msoliman512/simple-crud/assets/60291327/9a1f84b3-33de-4d5a-9d70-7651a4f1501a

## Project Structure

```
> Src
  >> Api (BE)
     >>> Common
     >>> DataAccess
     >>> Db (would be generated after the first run)
     >>> Features
     >>> Middlewares
  >> Logs (would be generated after the first run)
  >> Tests
  >> WebApp  (FE Folder)
     >>> App (FE project)
```
## Technologies Used
**Backend**
- .NET Core 8: Backend development framework
- SQLite: Relational Database Management System
- Dapper: Micro ORM for data access
- Fluent Validation: For input validation
- Serilog: Logging library
- MediatR: Mediator pattern implementation
- NUnit: Unit testing framework
- Bogus: Fake data generator for testing
- Swagger UI: Documentation tool for backend APIs
Frontend
- Angular 17: Frontend development framework
- HTML, CSS, typescript: Core technologies
- Jasmine testing framework
## Getting Started
To run the project locally, follow these steps:

- Clone the repository.
- Navigate to the backend and frontend directories separately.
- Install dependencies using npm install for the frontend and - dotnet restore for the backend.
- Start the backend server using dotnet run; `hint: I have customized my launchSettings profile to configure backend to run on port 5000 so swagger link would be  http://localhost:5000/swagger/index.html.`
- Start the frontend application using npm serve.
- Access the application in your web browser at http://localhost:4200.
## Usages

Once the application is running, you can perform the following actions:

**CRUD Operations:**
- Navigate to the CRUD operations section to manage driver records.
- Pagination: Use pagination to efficiently browse through a large number of driver records.
- Sorting: Sort driver records based on different attributes such as first name, last name, email, or phone number.
- Search: Search for specific driver records using a search keyword based on attributes like first name, last name, email, or phone number.
- **Upload File Component**: The upload file component in the Angular application allows users to upload text files and view their contents. **Key features** include file upload functionality, displaying file contents, counting word occurrences, validating file types (.txt), and a responsive user interface. The component is extensively tested with unit tests to ensure reliability and includes input validation to maintain security and integrity by only accepting text files for upload.

**Additional Features:**
- Bulk Driver Creation: Utilize the feature to add a list of bulk random drivers. This feature generates random drivers with valid fake data and inserts them into the database, providing a quick way to populate the application with test data.
- Swagger Documentation: Access the Swagger documentation at http://localhost:5000/swagger to explore the backend APIs; `applicationUrl/swagger/index.html` .

## Contributing

Contributions are always welcome!


