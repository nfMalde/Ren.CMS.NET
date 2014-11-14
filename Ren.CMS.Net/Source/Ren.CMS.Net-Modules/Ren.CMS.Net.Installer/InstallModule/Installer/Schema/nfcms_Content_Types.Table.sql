CREATE TABLE ren_cms_Content_Types (
		actionpath VARCHAR (350) NOT NULL 
		, controller VARCHAR (250) NOT NULL 
		, createPartial VARCHAR (250) NULL DEFAULT ('default')
		, editPartial VARCHAR (250) NULL DEFAULT ('default')
		, name VARCHAR (250) NOT NULL 
)
