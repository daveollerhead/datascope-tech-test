DatascopeTest

You can run the API either straight from VS or using "dotnet run" command from command line (inside the folder with the .csproj).

When the application first runs it will run the migrations for you to setup the database, aslong as you set a valid connection string to a SQL server instance in the appsettings.json file, "DbConnectionString" setting.
Also in the appsettings.json file is a "ClientUrl" setting, which is needed to setup the CORS policy to allow requests from the client app.

Run the client app using "npm run start".
There is also an appsettings.json file in the client app, with a single setting "apiBaseUrl". You must set this to the URL your API is running on in order to make requests.


