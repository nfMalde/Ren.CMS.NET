CREATE TABLE ren_cms_PermissionGroups (
		groupName VARCHAR (250) NOT NULL 
		, id INT IDENTITY(1,1) NOT NULL 
		, isDefaultGroup VARCHAR (50) NOT NULL 
		, isGuestGroup VARCHAR (50) NOT NULL 
)
