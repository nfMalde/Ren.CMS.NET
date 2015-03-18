Ren.CMS.NET
===========
>
## Installation Guide 
### Installation: Full Package
* Extract the Archive
* Go to {your extracted folder}\Deploy
* Deploy with Root Access or on local machines (Windows Auth. Only!):
  * Create a Database for Ren.CMS.NET (e.g. "rencms")
  * Open CMD.exe (Commandline)
  * Go to the Deploy Directory (cd ./....)
  * exec the following command: 
  ```  
  start deploy.bat  {Your Server} {Your DB}
  ```
  * Great! Now you have DB Structure + Sample Data set.
* Deploy without Root Access / on non local machines.
  * NOTE: Ren.CMS is currently not ready for productive usage! Be sure it is a test system. We recommend a local test server.
  * Go to your Webinterface / Controlpanel for your SQL Server
  * Locate the File ren-db-000.sql and import it to your SQL Server.
  * If existing execute all other versions also (for example ren-db-001.sql, ren-db-002.sql...)
  * Great! Now you have DB Structure + Sample Data set.
* Go back to your extracted Folder. Copy the Contents of the Folder "Application" to your Webfolder  / Local Website.
* Open Web.config and locate this line:
``` XML
  <connectionStrings>
 
    <add name="nfCMS" connectionString="..." providerName="System.Data.SqlClient" />
  </connectionStrings>
```
* Change the connection string matching your server data.
* Open Ren.CMS in the Browser. 
* Thats it. Important links and login can be found at the end of the Readme.
*

### Installation: Webdeploy Package
Note: For the Webdeploy Package you need MS Webdeploy (http://www.iis.net/downloads/microsoft/web-deploy)
* Extract the Archive
* Go to {your extracted folder}\Deploy
* Deploy with Root Access or on local machines (Windows Auth. Only!):
  * Create a Database for Ren.CMS.NET (e.g. "rencms")
  * Open CMD.exe (Commandline)
  * Go to the Deploy Directory (cd ./....)
  * exec the following command: 
  ```  
  start deploy.bat  {Your Server} {Your DB}
  ```
  * Great! Now you have DB Structure + Sample Data set.
* Deploy without Root Access / on non local machines.
  * NOTE: Ren.CMS is currently not ready for productive usage! Be sure it is a test system. We recommend a local test server.
  * Go to your Webinterface / Controlpanel for your SQL Server
  * Locate the File ren-db-000.sql and import it to your SQL Server.
  * If existing execute all other versions also (for example ren-db-001.sql, ren-db-002.sql...)
  * Great! Now you have DB Structure + Sample Data set.
* Go back to your extracted Folder.
* Execute the following command:
```
msdeploy -verb:sync -source:package=c:\<your-download-path>\XXXX_webdeploypackage.zip -dest:iisApp="domain.com/subfolder01",wmsvc=domain.com,username=IIS_username,password=IIS_password,skipAppCreation=false -allowUntrusted=true
```
* Go to the target folder
* Open Web.config and locate this line:
``` XML
  <connectionStrings>
 
    <add name="nfCMS" connectionString="..." providerName="System.Data.SqlClient" />
  </connectionStrings>
```
* Change the connection string matching your server data.
* Open Ren.CMS in the Browser. 
* Thats it. Important links and login can be found at the end of the Readme.
*

  
 
 
Important URLs:
===============

### Url Syntax
* {localhost}/{ISO LangCode}/
 
 Example
 localhost:7999/en-US/Home/Index
 
 List of Language Codes included in DB_Setup.sql
 * de-DE
 * en-US

 Backend
 * {url} /Backend/OS

### Admin Login (Default)
```
Username: admin
Password: admin
```
  
