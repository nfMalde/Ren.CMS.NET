CREATE TABLE ren_cms_Backend_Menu (
		action VARCHAR (100) NULL 
		, headID INT NULL 
		, iconUrl VARCHAR (250) NULL 
		, id INT IDENTITY(1,1) NOT NULL 
		, menuTextLang VARCHAR (250) NOT NULL 
		, neededPermission VARCHAR (150) NOT NULL 
)
