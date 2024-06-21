**Introduction**
# <Property Inventory System>
This project is a simple property inventory system that manages information for properties and their owners. 
The system includes a backend API and a user interface (UI/UX) for managing properties and contacts, as well as a dashboard to display property transaction history.

**Features** 
# <Features>
Store information about properties and contacts.
Maintain a relationship between properties and their owners over time.
Record and display property price changes and ownership history.
Provide a dashboard to view property information and transaction history.
REST API for CRUD operations on properties and contacts.
Professional UI for property and contact management.

**Technologies**
ASP.NET Core 6,
Entity Framework Core,
SQL Server,
MVC Razor for UI,
RESTful API,
Visual Studio 2022.

**Prerequisites**
Visual Studio 2022,
.NET 6 SDK,
SQL Server.

### <Setup Instructions>
1. Clone the Repository
bash
Copy code
git clone https://github.com/yourusername/PropertyInventorySystem_Testing.git
cd property-inventory-system
2. Configure the Database
Update the appsettings.json file with your SQL Server connection string.

json
Copy code
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server_name;Database=PropertyInventoryDB;Trusted_Connection=True;"
  }
}

3. Run Database Migrations
Open the Package Manager Console and run the following commands:
powershell
Copy code
Add-Migration InitialCreate
Update-Database

5. Seed the Database
Run the seed script to populate the database with initial data:

powershell
Copy code
Update-Database -Migration:SeedData

5. Run the Application
Press F5 in Visual Studio to build and run the application.

**Project Structure**
1,Controllers: Contains API controllers for properties and contacts.
2,Models: Entity models for properties, contacts, and sold properties.
3,Data: Database context and configuration for Entity Framework Core.
4,Services: Business logic and service layer.
5,Views: Razor views for UI.
6,wwwroot: Static files for frontend (CSS, JS, images).

**API Endpoints**

**Properties**
GET /api/properties - Retrieve all properties with pagination and filtering.
GET /api/properties/{id} - Retrieve a single property.
POST /api/properties - Create a new property.
PUT /api/properties/{id} - Update an existing property.
DELETE /api/properties/{id} - Delete a property.

**Contacts**
GET /api/contacts - Retrieve all contacts with pagination and filtering.
GET /api/contacts/{id} - Retrieve a single contact.
POST /api/contacts - Create a new contact.
PUT /api/contacts/{id} - Update an existing contact.
DELETE /api/contacts/{id} - Delete a contact.

**UI Design**
The UI consists of two main pages:
Home: The Main Property Dashboard with owner information.
Create Property: A form to input property and contact information.
Sold Property: A page to view the full history of property ownership and transactions.

**Dashboard**
The dashboard displays a summary of properties, including the latest owner and transaction details. It supports CRUD operations and searching.

**Seed Script**
The seed script initializes the database with a set of sample data to test the application.

**Error Handling**
Comprehensive error handling is implemented in the API controllers and services to ensure robust and user-friendly error messages.

**Contribution**
Contributions are welcome! Please fork the repository and submit a pull request with your changes.

This README file provides an overview and setup instructions for the Property Inventory System.
Please follow the steps to set up the project and refer to the provided example code snippets for implementation details.
