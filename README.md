Ren.CMS.NET
===========

Ren.CMS.Net Open Source .NET 4 (MVC4) CMS
===============
How to Install Pre-Releases:
1. Go to Releases 

2. Download the latest Pre-Release

3. Choose one of the Packages _fullpackage or _webdeploypackage

3.1 Installing via fullpackage

3.1.1 Extract the ZIP and Copy the Contents to your Website / FTP

3.1.2 Open Web.Config and Change the Connectionstring.

3.2 Installing via Webdeploy

3.2.1 Download the Package and then open Commandline

3.2.2 Change Directory (cd "PATH" ) to the Webdeploy Folder (http://www.iis.net/downloads/microsoft/web-deploy) Execute the following Command:

```text
msdeploy -verb:sync -source:package=c:\<your-download-path>\XXXX_webdeploypackage.zip -dest:iisApp="domain.com/subfolder01",wmsvc=domain.com,username=IIS_username,password=IIS_password,skipAppCreation=false -allowUntrusted=true
```
4. Download the SQL_DEPLOY_XXX.zip

4.1 Change Directory to the Extract Folder (cd "Path") and type in (Alternative Look at 4.2):

4.1.1

```text
start deploy.bat SQL_SERVER SQL_DB
```
4.1.2 Check the DB if Structure is created.

4.2 ALTERNATIVE: Import into you Webinterfache for your SQL Server every SQL File beginning from the numbers on the End -000.sql

Admin Login (Default)

Username: admin
Password: admin

How to Develope and Help:
First at all Fork and Clone the Repository.
Then follow this Steps:
1. Connect to you local SQL Server (MSSQL 2008+)

2. Create your DB for testing the CMS.

3. Install Structure + Sample Data

3.1 Open Command Line

3.2 Change Directory to the Deploy Folder (cd "Path") and type in:

3.2.1

```text
start deploy.bat SQL_SERVER SQL_DB
```

4. Check the DB if Structure is created.

5. If Successfull: Opel Solution "Ren.CMS.Net\Source\Ren.CMS.sln" in Visual Studio

6. Look into Project Folder (In Project Explorer!) /WEB/ and go into Ren.CMS  Project

7. Open Web.Config and go to Line 12 (<connectionString> ... </connectionString>)

8. Setup your connectionstring


Important URLs:
===============

Url Syntax
* {localhost}/{ISO LangCode}/
 
 Example
 localhost:7999/en-US/Home/Index
 
 List of Language Codes included in DB_Setup.sql
 * de-DE
 * en-US

 Backend
 * {url} /Backend/OS

Admin Login (Default)

Username: admin
Password: admin

  
