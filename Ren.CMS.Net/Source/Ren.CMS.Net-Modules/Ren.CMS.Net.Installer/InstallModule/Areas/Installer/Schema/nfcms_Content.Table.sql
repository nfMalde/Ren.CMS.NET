CREATE TABLE ren_cms_Content (
		cDate DATETIME NULL 
		, CID UNIQUEIDENTIFIER NOT NULL 
		, Content TEXT NOT NULL 
		, ContentRef INT NULL 
		, ContentType VARCHAR (100) NOT NULL 
		, CreatorPKID VARCHAR (500) NOT NULL 
		, CreatorSpecialName VARCHAR (250) NULL 
		, id INT IDENTITY(1111,1212) NOT NULL 
		, Locked VARCHAR (50) NOT NULL 
		, LongText TEXT NULL 
		, MetaDescription VARCHAR (255) NULL 
		, MetaKeyWords TEXT NULL 
		, PreviewText VARCHAR (255) NULL 
		, ratingGroupID INT NULL 
		, SEOname VARCHAR (255) NULL 
		, Title VARCHAR (255) NOT NULL 
)
ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
