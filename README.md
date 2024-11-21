# GameGather - Sport games gather API

This is my work on task given in file "Senior Developer Exercise.pdf".  

The solution has 2 WebApi services:

1) GameGather - executes the logic of gathering the games across various external services.
When starting from VS, the app starts listening on address http://localhost:5250
By default, the app runs game gathering logic each X minutes, defined by parameter PeriodicGatherMinutes in appsettings.json file.
The app has single endpoint - POST /api/Gather/StartGatherData, intended for manual start of game gathering logic.

Game gathering is executed by classes implementing interface IExternalGameProvider. Providers are registered on app start. We can add logic for manual provider registration.
Currently, 3 sample providers are implemented:
StaticJsonProvider - returns games from file StaticGames.json in project root.
SelfGameProvider - sends request to the internal storage, thus, no new games will be added using this provider
RandomGamesProvider - returns random 10 games

2) GameQuery - exposes API for querying the stored games.
When starting from VS, the app starts listening on address http://localhost:5206
The app has single endpoint - POST /api/Query/QueryGames, it accepts GameQueryModel as a parameter and returns the stored games array:
```yaml
POST  
/api/Query/QueryGames
{  
    "FromDate": "datetime",
    "ToDate": "datetime",
    "SportType": "string",
    "Name": "string",
    "Team": "string",
    "RowCount": "int",
}
Response:
if success: status 200;
if error: status 400; error message in body 
```

Used SQLite database for data storage; DB file GameGatherData.s3db is in project root.
