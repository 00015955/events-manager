# WIUT Events Manager

This application was developed for the **Web Application** module as a coursework portfolio project @ WIUT by **Student ID: 00015955**.

## Calculation:
**15955 / 20 = 797.75**, where the remainder **5** specifies the following topic: **Events Manager**.

## How to Clone
To get started, clone the project repository using the following command:

```bash
git clone https://github.com/00015955/events-manager.git
```

## Prerequisites

### Once the project has been copied, open the project in your IDE and create .env file inside backend folder and copy the properties of .env.example depending on which OS you use
```bash
#Usage for ConnectionString inside appsettings.json

#Windows
PCNAME=YourComputerName
DATABASENAME=YourDatabaseName

#Mac or Linux
DB_HOST=YourLocalHost
DB_NAME=YourDatabaseName
DB_USER=YourDatabaseUsername
DB_PASS=YourDatabasePassword
```

### Then, ensure to modify appsettings.json
#### For Windows
```bash
"ConnectionStrings": {
    "DefaultConnection": "Data Source={PCNAME};Initial Catalog={DATABASENAME};Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  }
```

#### For Mac/Linux
```bash
  "ConnectionStrings": {
    "DefaultConnection": "Data Source={DB_HOST};Initial Catalog={DB_NAME};User Id={DB_USER};Password={DB_PASS};Integrated Security=False;TrustServerCertificate=True;Trusted_Connection=False"
  }
```

### !!(For Mac/Linux users) Open Program.cs and uncomment the following code snippet and don't forget to comment Windows variable:
```bash
//Mac or Linux
/* var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!
  .Replace("{DB_HOST}", Env.GetString("DB_HOST"))
  .Replace("{DB_NAME}", Env.GetString("DB_NAME"))
  .Replace("{DB_USER}", Env.GetString("DB_USER"))
  .Replace("{DB_PASS}", Env.GetString("DB_PASS")); */
```

### Run the following commands to initiate migration and update database
```bash
dotnet ef migrations add init
dotnet ef database update
```

### Run the backend
```bash
dotnet watch run
```

### Now open frontend folder and update the libraries
```bash
npm i
```

### Make sure to create environment.ts and replace the apiBaseUrl with your localhost
```bash
export const environment = {
  production: false,
  apiBaseUrl: 'http://your_localhost'
};
```

###Run the frontend
```bash
ng serve
```
##You're now good to go!

