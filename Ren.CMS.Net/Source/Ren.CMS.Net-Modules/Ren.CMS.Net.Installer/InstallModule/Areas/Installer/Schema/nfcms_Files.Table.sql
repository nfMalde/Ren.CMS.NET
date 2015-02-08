CREATE TABLE ren_cms_Files (
		active INT NOT NULL 
		, aliasName VARCHAR (255) NOT NULL 
		, fileSize INT NULL 
		, fpath TEXT NOT NULL 
		, id INT IDENTITY(1,1) NOT NULL 
		, needPermission VARCHAR (255) NOT NULL 
		, ProfileID INT NULL 
)
ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
