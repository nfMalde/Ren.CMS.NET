CREATE TABLE nfcms_SettingModels (
		CID INT NULL 
		, id INT IDENTITY(1,1) NOT NULL 
		, SettingDefaultValue TEXT NULL 
		, SettingLangLineDescr VARCHAR (150) NOT NULL 
		, SettingLangLineLabel VARCHAR (150) NOT NULL 
		, SettingName VARCHAR (150) NOT NULL 
		, SettingRelation VARCHAR (150) NOT NULL 
		, SettingType VARCHAR (150) NOT NULL 
		, ValueType VARCHAR (150) NOT NULL 
)
ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
