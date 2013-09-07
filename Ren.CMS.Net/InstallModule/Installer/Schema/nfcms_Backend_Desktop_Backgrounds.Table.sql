CREATE TABLE nfcms_Backend_Desktop_Backgrounds (
		backgroundAlign VARCHAR (50) NULL 
		, backgroundColor VARCHAR (50) NULL DEFAULT ('#000000')
		, backgroundImage TEXT NULL 
		, backgroundRepeat VARCHAR (50) NULL 
		, userid VARCHAR (250) NOT NULL 
)
ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
