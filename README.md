# Student Management System

ASP.NET Core Web API for managing students with CRUD endpoints, soft delete, and Excel import.

## Requirements
- .NET 8 SDK
- MySQL (or compatible) database

## Setup
1. Update the connection string in `Student_Management_System/appsettings.json`.
2. Run migrations and update the database:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
3. Run the API:
   ```bash
   dotnet run --project Student_Management_System
   ```

Swagger is available at `/swagger`.

## API Endpoints
- `GET /api/students` - list all students
- `GET /api/students/{id}` - get student by id
- `GET /api/students/course/{course}` - list students by course
- `GET /api/students/courses` - list distinct courses
- `POST /api/students` - create student
- `PUT /api/students/{id}` - update student
- `DELETE /api/students/{id}` - soft delete student
- `POST /api/students/upload-excel` - import students from Excel

## Excel Import
Upload a `.xlsx` file with the following headers:
- `Name`
- `Course`
- `Marks`

Rows are imported starting from row 2.
