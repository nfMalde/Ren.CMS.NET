CREATE TABLE ren_cms_Categories (
		contentType VARCHAR (255) NOT NULL 
		, longName VARCHAR (255) NOT NULL 
		, PKID UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL 
		, shortName VARCHAR (255) NOT NULL 
		, subFrom VARCHAR (250) NULL 
)