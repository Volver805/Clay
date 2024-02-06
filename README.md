# Lock Control System (Clay Project) 🔐
Author: Ahmed Sayed ✍️

## How to Build & Run this project
### Migrations
in solution root folder open CLI and run 
```
dotnent ef database update --project Clay.Infrastructure --startup-project Clay.Api
```

### Running API
1. Change Connection String and JWT attributes in Clay.Api/appsettings.json
2. Run migrations to create database tables and seed data.
3. Open the Solution in root directory and set Clay.API as your startup project.
4. Authenticate by using api\auth\login endpoint (Login credentials will be listed below)
5. Add JWT token granted from authentication to Authorization Header (i.e. Authorization: Bearer {Paste your token here}

*Optional: you can setup visiual studio to start AutoDoorLocking as well to enable the autolocking functionality but make sure to add the connection string to appsettings.json inside AutoDoorLocking project.*

### Application Data
Our little team only have 3 memebers Ahmed (Employee), Nikita (Employee), Nadiia (Manager) 
Employees have access to lock 1 (entrance) while manager have access to (storage room) and as well can view the event history.
### Login
```
{
    "username": "Nikita",
    "password": "billiondollarcode"
}
{
    "username": "Ahmed",
    "password": "moonlightsword" 
}

{
    "username": "Nadiia",
    "password": "sett5000" 
}
```

## Design Document
[DESIGN.md](./DESIGN.md) have a **Solution Design** section containing all information about design decisions and overall the architecture for this repository. also if you have a time you can take a look into the hall document it's about 4-5 minutes read. 