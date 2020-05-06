# RESTful Sign & Storage API
 
 
* An ongoing multitech RESTful project for upload and save files as a binary repository

* Each file can be signed to user by digital signiture.

* The RESTful API and the Sign service are implemented over .NetCore, the Storage server over a Node.js.

* The Node.js storage server uploaded here as a separate GitHub repository named [StorageAPI]( https://github.com/naftalix/StorageAPI "sdfsdf")

* Will be added a React UI app for managment files and signs.


#### You can config your StorageAPI URI enviroment by "StorageServer" key under `appsettings.json` project file

### Example:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "StorageServer":  "localhost:3000/storage"
}

```

