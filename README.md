Ren.CMS.NET
===========

Ren.CMS.Net Open Source .NET 4 (MVC4) CMS
===============
How to:

1. Connect to you local SQL Server (MSSQL 2008+)
2. Create DB ncms_net
3. Open "Ren.CMS.Net\nCMS_NET\Install\DB_SETUP.sql"
4. Execute and see for errors.
5. If Successfull: Opel Solution "Ren.CMS.Net\Ren.CMS.sln" in Visual Studio
6. Look into Project Folder (In Project Explorer!) /WEB/ and go into Ren.CMS  Project
7. Open Web.Config and go to Line 12 (<connectionString> ... </connectionString>)
8. Please >dont< Change the connection Sting. Just Copy it and comment it out (better for other developers)
9. Create your Connectionstring regarding to you local sql server settings. (For security use Windows Authentifikation and Integrated Security)
10.Now Build the Solution and have fun with coding :)


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

  
