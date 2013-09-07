CREATE TABLE nfcms_Content_Tags (
		contentType VARCHAR (50) NOT NULL 
		, enableBrowsing INT NOT NULL 
		, id INT IDENTITY(1,1) NOT NULL 
		, tagName VARCHAR (250) NOT NULL 
		, tagNameSEO VARCHAR (250) NULL 
)
