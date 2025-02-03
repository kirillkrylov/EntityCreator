## Create new package in download it to file system
  - Create new package in Creatio and download it to file system, use [clio] to help.
  - Install [NET8]  **NOT** .NET9
  - Install clio if not exists  (`dotnet tool install clio -g`)
  - Register Creatio with clio [reg-web-app]
  - Create empty directory for a new worskpace  `> mkdir myWorkspace`
  - Create new workspace `> clio create-workspace -e ENV_NAME_FROM_PREVIOUS_STEP`
  - Restore workspace [restorew] `clio restorew -e ENV_NAME_FROM_PREVIOUS_STEP`

At this point you should have a new workspace with the package you created in Creatio. 
If you dont see packages check `.clio\workspaceSettings.json` file. 
It should contain a list pf packages in Packages array. If not add them manually, and try restore again

```json
{
  "Packages": ["MyPackage1"],
  "ApplicationVersion": "8.2.1"
}
```

Check that packages folder is not empty.


## Use EntityCreator to generate new Entity from db table

- update `packageFolderPath` in `appsettings.json`
- update `packageUId`, in workspace find `descriptor.json` file and copy `packageUId` value 
- update connection string in `appsettings.json`


## Execute EntityCreator
```ps
.\EntityCreator.exe add-entity EntityNameInCreatio TableNameinForeignDb
```  

For example to create Contact entity in Creatio from TableWithPeople in foreign db, use
```ps
.\EntityCreator.exe add-entity Contact TableWithPeople
```

## [Publish package][pushw] back to Creatio
```ps
clio pushw -e ENV_NAME_FROM_PREVIOUS_STEP
```

You should see new entity in Creatio.



[//]: # (Named links)
[clio]: https://github.com/Advance-Technologies-Foundation/clio
[NET8]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
[reg-web-app]: https://github.com/Advance-Technologies-Foundation/clio/blob/master/clio/Commands.md#reg-web-app
[restorew]: https://github.com/Advance-Technologies-Foundation/clio/blob/master/clio/Commands.md#restore-workspace
[pushw]: https://github.com/Advance-Technologies-Foundation/clio/blob/master/clio/Commands.md#push-workspace