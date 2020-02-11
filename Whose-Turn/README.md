WHOSE TURN .NET CORE 3.1 API
====================================

This application is the API providing business logic to the front en

Preparations on Whose Turn
---------------------
To run this sample you must have the latest version of Microsoft Visual Studio installed from https://visualstudio.microsoft.com/vs/.

Configure the application to run only on HTTPS by opening 'Properties/launchSettings.json' and adding the following (You can use any free port)

```json
  "iisSettings": {
    ...
    "iisExpress": {
      "applicationUrl": "http://localhost:21558",
      "sslPort": 44336
    }
  },
...
 "Whose_Turn": {
      ...
      "applicationUrl": "https://localhost:5001;http://localhost:5000"
  }
```

Create a Send Grid API key:
1) Log into SendGrid 
2) Go to Account Settings in the My Account dropdown.  
3) Generate an API key to use for the API 
4) Place the API key in SendGrid:ApiKey inside appsettings.json

Configurations (appsettings.json)
-------------

```json
{
  "JwtTokens": {
    "secret": "my secret key",
    "issuer": "whoseturn.co.uk",
    "audience": "whoseturn.api",
    "accessExpiration": 30,
    "refreshExpiration": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "React": {
    "Uri": ""
  },
  "SendGrid": {
    "ApiKey": "my secrete key"
  },
  "ServiceBus": {
    "ErrorQueue": "Whose_Turn.Error",
    "ApiQueue": "Whose_Turn.Api"
  }
}
```
Put the above json in appSettings.json file in the root directory of the project.


Starting Server
----
To start the server, simply load the solution in visual studio press f5. This will resolve all dependencies and start the server.
