# Lanmar Code Test

A small .Net Core web API that provides the ability to manage contacts in a SQL database.

## Features

The following features have been implemented:

1. Add a contact
2. Update a contact
3. Display all contacts in a list
4. Search for a contact with results displayed in a list
5. Ability to edit a contact

In addition:

1. Pagination
2. Validation of fields, and types
3. Column sorting (Ascending and Descending)
4. Code is commented with summaries
5. API enforces required fields (First and Lastname)

## Notes

- I decided create a .Net Core API, in addition to the MVC project to showcase a different approach and design
- The database is auto populated with 20 contacts, on first run, for testing purposes
- I have not implemented EF migrations
- I did not write Controller unit tests for this API due to time constraints

## Requirements

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [Microsoft SQL Server 2020 Express](https://go.microsoft.com/fwlink/p/?linkid=2216019&clcid=0xc09&culture=en-au&country=au) (other SQL instances should be supported but not tested)
- [.Net Core 8](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-8.0.17-windows-x64-installer)

## Setup

### Clone Repository

```bash
git clone https://github.com/TheGreggle/ContactsManager.Core
```

### Notes for SQL Server (not Express)

Should you wish to use a version of Microsoft SQL Server that is not express the following changes may be required.

1. Update `<RootPath>\ContactManager.Core\appsettings.json` `<ConnectionStrings>` to support a configuration of Microsoft Server SQL other than Express.
2. You may need to add `TrustServerCertificate=true` to the SQL Server connection string.

## Start Project

1. Open the `ContactManager.Core\ContactManager.Core.sln` Solution in Visual Studio.
2. Trust the project if prompted
3. If prompted, click `Yes` twice to install the development SSL certificate.
4. Press F5.

   The first run will restore packages and create the database. This may take up to 30 seconds.

5. The Swagger Portal at `https://localhost:7180/` should now display. Leave this open.
