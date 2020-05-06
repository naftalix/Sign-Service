# RESTful Sign & Storage API Manager
 
 
* An ongoing multitech RESTful project for upload and save files as a binary repository

* Each file can be signed to user by digital signiture.

* This manager implemented by .NetCore.

* The manager's FrontEnd will be a React App.

* The Storage service implemented by Node.js.

* The Sign service module will be on Python.

#### Modules Repos
* The Node.js storage server uploaded here as a separate GitHub repository named [storage-api]( https://github.com/naftalix/storage-api)


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

