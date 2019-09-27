# Windows Service + WebApi
## Project
+ In ConfigurationService folder you will find windows service project
in order to install you need to open developer tool window as admin and type InstallUtil.exe + Your copied path + \your service name + .exe;
+ You need to run service in services.msc( may find in windows search);
+ In ConfigurationApi folder web api project;


## General info
+   Windows service that gathers the pc name, manufacturer, list of users (which have logged to this system), cpu/ram load
+ Uploads this information every 30 minutes over to a .NET Web API
+ Stores the information into a database and updates if an existing record is there (ms sql/ef)
## TODO
+ Manage problem with httpPost;
+ Make some changes to db structure;
+ Lots of refactoring to do;
