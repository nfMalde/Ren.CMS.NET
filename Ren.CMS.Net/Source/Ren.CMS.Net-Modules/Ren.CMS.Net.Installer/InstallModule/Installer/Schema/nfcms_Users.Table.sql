CREATE TABLE nfcms_Users (
		ApplicationName VARCHAR (255) NOT NULL 
		, Comment VARCHAR (255) NULL 
		, CreationDate DATETIME NULL 
		, CustomerID TEXT NULL 
		, Email VARCHAR (128) NOT NULL 
		, FailedPasswordAnswerAttemptCount INT NULL 
		, FailedPasswordAnswerAttemptWindowStart DATETIME NULL 
		, FailedPasswordAttemptCount INT NULL 
		, FailedPasswordAttemptWindowStart DATETIME NULL 
		, IsApproved VARCHAR (10) NULL 
		, IsLockedOut VARCHAR (10) NULL 
		, IsOnLine VARCHAR (10) NULL 
		, IsSubscriber VARCHAR (10) NULL 
		, LastActivityDate DATETIME NULL 
		, LastLockedOutDate DATETIME NULL 
		, LastLoginDate DATETIME NULL 
		, LastPasswordChangedDate DATETIME NULL 
		, Loginname VARCHAR (255) NOT NULL 
		, Password VARCHAR (128) NOT NULL 
		, PasswordAnswer VARCHAR (255) NULL 
		, PasswordQuestion VARCHAR (255) NULL 
		, PermissionGroup VARCHAR (50) NULL 
		, PKID UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL 
		, Username VARCHAR (255) NOT NULL 
)
ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
