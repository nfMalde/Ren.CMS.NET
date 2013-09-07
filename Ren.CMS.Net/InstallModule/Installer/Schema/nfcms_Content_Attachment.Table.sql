CREATE TABLE nfcms_Content_Attachment (
		aTitle VARCHAR (150) NULL 
		, attachment_type VARCHAR (255) NOT NULL 
		, AttachmentArgument VARCHAR (250) NULL 
		, content_type VARCHAR (255) NOT NULL 
		, fName VARCHAR (255) NULL 
		, fPath VARCHAR (255) NULL 
		, NID INT NOT NULL 
		, PKID UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL 
		, thumpNail VARCHAR (255) NULL 
)
