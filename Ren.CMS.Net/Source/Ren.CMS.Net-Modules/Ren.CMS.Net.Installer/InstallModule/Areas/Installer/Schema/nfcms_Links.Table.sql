CREATE TABLE ren_cms_Links (
		HoverStateClass VARCHAR (50) NULL 
		, id INT IDENTITY(1111,1212) NOT NULL 
		, LinkAction VARCHAR (255) NULL 
		, LinkController VARCHAR (255) NULL 
		, LinkHref VARCHAR (255) NULL 
		, LinkIsActive VARCHAR (50) NULL 
		, LinkRelationship VARCHAR (250) NULL 
		, LinkText VARCHAR (255) NOT NULL 
		, LinkType VARCHAR (255) NOT NULL 
		, NormalStateClass VARCHAR (50) NULL 
		, SublinkAction VARCHAR (250) NULL 
		, SublinkController VARCHAR (250) NULL 
		, SublinkFrom INT NULL 
)
