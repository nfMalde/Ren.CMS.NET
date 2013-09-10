CREATE TABLE nfcms_Backend_Widgets (
		definedHeight INT NULL 
		, definedWidth INT NULL 
		, Icon VARCHAR (50) NULL DEFAULT ('Defaulticon.png')
		, id INT IDENTITY(1,1) NOT NULL 
		, neededPermission VARCHAR (50) NOT NULL 
		, widgetName VARCHAR (50) NOT NULL 
		, widgetPartialView VARCHAR (50) NOT NULL 
)
