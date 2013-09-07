CREATE TABLE nfcms_Users_Settings_Table (
		DataType VARCHAR (50) NULL 
		, id INT IDENTITY(1111,1212) NOT NULL 
		, s_type CHAR (10) NULL 
		, settingDefaultVal VARCHAR (250) NULL 
		, settingLangLine VARCHAR (250) NULL 
		, settingLongDescription VARCHAR (250) NULL 
		, settingName VARCHAR (255) NOT NULL 
		, SettingOrder INT NULL 
)
