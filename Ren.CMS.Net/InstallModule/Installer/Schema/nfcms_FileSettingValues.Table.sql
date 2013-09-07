CREATE TABLE nfcms_FileSettingValues (
		id INT IDENTITY(1,1) NOT NULL 
		, ProfileID INT NOT NULL 
		, SettingID INT NOT NULL 
		, SettingValue VARCHAR (50) NOT NULL 
)
