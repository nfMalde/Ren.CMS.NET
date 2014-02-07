USE [ncms_net]

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__nfcms_Thu__Heigh__4119A21D]') AND type = 'D')

BEGIN

ALTER TABLE [dbo].[nfcms_Thumpnails_Module] DROP CONSTRAINT [DF__nfcms_Thu__Heigh__4119A21D]

END



GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__nfcms_Thu__Width__40257DE4]') AND type = 'D')

BEGIN

ALTER TABLE [dbo].[nfcms_Thumpnails_Module] DROP CONSTRAINT [DF__nfcms_Thu__Width__40257DE4]

END



GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_nfcms_Filemanagement_Crossbrowsers_FileType]') AND type = 'D')

BEGIN

ALTER TABLE [dbo].[nfcms_Filemanagement_Crossbrowsers] DROP CONSTRAINT [DF_nfcms_Filemanagement_Crossbrowsers_FileType]

END



GO

/****** Object:  Table [dbo].[nfmcs_Internal_Rating]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfmcs_Internal_Rating]') AND type in (N'U'))

DROP TABLE [dbo].[nfmcs_Internal_Rating]

GO

/****** Object:  Table [dbo].[nfcms_Users_Settings_Table]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Users_Settings_Table]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Users_Settings_Table]

GO

/****** Object:  Table [dbo].[nfcms_Users_Settings]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Users_Settings]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Users_Settings]

GO

/****** Object:  Table [dbo].[nfcms_Users]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Users]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Users]

GO

/****** Object:  Table [dbo].[nfcms_User2Settingvalues]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_User2Settingvalues]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_User2Settingvalues]

GO

/****** Object:  Table [dbo].[nfcms_Thumpnails_Module]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Thumpnails_Module]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Thumpnails_Module]

GO

/****** Object:  Table [dbo].[nfcms_Sub_Categories]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Sub_Categories]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Sub_Categories]

GO

/****** Object:  Table [dbo].[nfcms_SettingValues]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_SettingValues]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_SettingValues]

GO

/****** Object:  Table [dbo].[nfcms_SettingStores2Locales]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_SettingStores2Locales]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_SettingStores2Locales]

GO

/****** Object:  Table [dbo].[nfcms_SettingStores]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_SettingStores]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_SettingStores]

GO

/****** Object:  Table [dbo].[nfcms_Settings2Permissions]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Settings2Permissions]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Settings2Permissions]

GO

/****** Object:  Table [dbo].[nfcms_SettingModels]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_SettingModels]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_SettingModels]

GO

/****** Object:  Table [dbo].[nfcms_SettingCategories]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_SettingCategories]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_SettingCategories]

GO

/****** Object:  Table [dbo].[nfcms_Routing_Synonyms]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Routing_Synonyms]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Routing_Synonyms]

GO

/****** Object:  Table [dbo].[nfcms_RegisteredMIMETypes]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_RegisteredMIMETypes]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_RegisteredMIMETypes]

GO

/****** Object:  Table [dbo].[nfcms_Rating_Groups]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Rating_Groups]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Rating_Groups]

GO

/****** Object:  Table [dbo].[nfcms_Rating]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Rating]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Rating]

GO

/****** Object:  Table [dbo].[nfcms_Profile_Vars]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Profile_Vars]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Profile_Vars]

GO

/****** Object:  Table [dbo].[nfcms_Profile_User_Values]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Profile_User_Values]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Profile_User_Values]

GO

/****** Object:  Table [dbo].[nfcms_Permissions2Users]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Permissions2Users]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Permissions2Users]

GO

/****** Object:  Table [dbo].[nfcms_Permissions2Groups]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Permissions2Groups]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Permissions2Groups]

GO

/****** Object:  Table [dbo].[nfcms_Permissionkeys]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Permissionkeys]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Permissionkeys]

GO

/****** Object:  Table [dbo].[nfcms_PermissionGroups]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_PermissionGroups]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_PermissionGroups]

GO

/****** Object:  Table [dbo].[nfcms_Links2Identfiers]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Links2Identfiers]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Links2Identfiers]

GO

/****** Object:  Table [dbo].[nfcms_Links]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Links]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Links]

GO

/****** Object:  Table [dbo].[nfcms_Link_Identifiers]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Link_Identifiers]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Link_Identifiers]

GO

/****** Object:  Table [dbo].[nfcms_Language]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Language]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Language]

GO

/****** Object:  Table [dbo].[nfcms_Lang_Codes]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Lang_Codes]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Lang_Codes]

GO

/****** Object:  Table [dbo].[nfcms_Internal_Rating]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Internal_Rating]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Internal_Rating]

GO

/****** Object:  Table [dbo].[nfcms_Internal_Pro_Contra]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Internal_Pro_Contra]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Internal_Pro_Contra]

GO

/****** Object:  Table [dbo].[nfcms_Global_Settings]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Global_Settings]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Global_Settings]

GO

/****** Object:  Table [dbo].[nfcms_FileSettingValues]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FileSettingValues]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_FileSettingValues]

GO

/****** Object:  Table [dbo].[nfcms_Files]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Files]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Files]

GO

/****** Object:  Table [dbo].[nfcms_FileManagementProfiles2FileSettings]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FileManagementProfiles2FileSettings]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_FileManagementProfiles2FileSettings]

GO

/****** Object:  Table [dbo].[nfcms_FileManagementProfiles]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FileManagementProfiles]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_FileManagementProfiles]

GO

/****** Object:  Table [dbo].[nfcms_FileManagementFileSettings]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FileManagementFileSettings]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_FileManagementFileSettings]

GO

/****** Object:  Table [dbo].[nfcms_FilemanagementControllersAcceptProfiles]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FilemanagementControllersAcceptProfiles]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_FilemanagementControllersAcceptProfiles]

GO

/****** Object:  Table [dbo].[nfcms_FilemanagementControllersAcceptMimeTypes]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FilemanagementControllersAcceptMimeTypes]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_FilemanagementControllersAcceptMimeTypes]

GO

/****** Object:  Table [dbo].[nfcms_FilemanagementControllers]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FilemanagementControllers]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_FilemanagementControllers]

GO

/****** Object:  Table [dbo].[nfcms_Filemanagement_Crossbrowsers]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Filemanagement_Crossbrowsers]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Filemanagement_Crossbrowsers]

GO

/****** Object:  Table [dbo].[nfcms_Content_Types]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Types]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Content_Types]

GO

/****** Object:  Table [dbo].[nfcms_Content_Text]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Text]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Content_Text]

GO

/****** Object:  Table [dbo].[nfcms_Content_Tags2Content]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Tags2Content]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Content_Tags2Content]

GO

/****** Object:  Table [dbo].[nfcms_Content_Tags]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Tags]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Content_Tags]

GO

/****** Object:  Table [dbo].[nfcms_Content_ClickCounter]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_ClickCounter]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Content_ClickCounter]

GO

/****** Object:  Table [dbo].[nfcms_Content_Attachment_Roles]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Attachment_Roles]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Content_Attachment_Roles]

GO

/****** Object:  Table [dbo].[nfcms_Content_Attachment]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Attachment]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Content_Attachment]

GO

/****** Object:  Table [dbo].[nfcms_Content]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Content]

GO

/****** Object:  Table [dbo].[nfcms_Category_Text]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Category_Text]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Category_Text]

GO

/****** Object:  Table [dbo].[nfcms_Categories]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Categories]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Categories]

GO

/****** Object:  Table [dbo].[nfcms_Backend_Widgets]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Backend_Widgets]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Backend_Widgets]

GO

/****** Object:  Table [dbo].[nfcms_Backend_User_Desktops]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Backend_User_Desktops]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Backend_User_Desktops]

GO

/****** Object:  Table [dbo].[nfcms_Backend_Menu]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Backend_Menu]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Backend_Menu]

GO

/****** Object:  Table [dbo].[nfcms_Backend_Desktop_Icons]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Backend_Desktop_Icons]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Backend_Desktop_Icons]

GO

/****** Object:  Table [dbo].[nfcms_Backend_Desktop_Backgrounds]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Backend_Desktop_Backgrounds]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Backend_Desktop_Backgrounds]

GO

/****** Object:  Table [dbo].[nfcms_Article_Rating]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Article_Rating]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms_Article_Rating]

GO

/****** Object:  Table [dbo].[nfcms:Content_Text]    Script Date: 08.09.2013 16:33:51 ******/

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms:Content_Text]') AND type in (N'U'))

DROP TABLE [dbo].[nfcms:Content_Text]

GO

/****** Object:  Table [dbo].[nfcms:Content_Text]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms:Content_Text]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms:Content_Text](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[ContentId] [int] NOT NULL,

	[Title] [varchar](255) NOT NULL,

	[SEOname] [varchar](255) NULL,

	[MetaKeyWords] [text] NULL,

	[MetaDescription] [varchar](255) NULL,

	[PreviewText] [varchar](255) NULL,

	[LongText] [text] NULL,

	[LangCode] [varchar](10) NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Article_Rating]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Article_Rating]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Article_Rating](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[articleID] [int] NOT NULL,

	[ratingID] [int] NOT NULL,

	[stars] [int] NOT NULL

) ON [PRIMARY]

END

GO

/****** Object:  Table [dbo].[nfcms_Backend_Desktop_Backgrounds]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Backend_Desktop_Backgrounds]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Backend_Desktop_Backgrounds](

	[userid] [varchar](250) NOT NULL,

	[backgroundImage] [text] NULL,

	[backgroundColor] [varchar](50) NULL,

	[backgroundAlign] [varchar](50) NULL,

	[backgroundRepeat] [varchar](50) NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Backend_Desktop_Icons]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Backend_Desktop_Icons]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Backend_Desktop_Icons](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[langLine] [varchar](150) NULL,

	[Icon] [varchar](150) NULL,

	[Action] [varchar](150) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Backend_Menu]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Backend_Menu]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Backend_Menu](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[menuTextLang] [varchar](250) NOT NULL,

	[iconUrl] [varchar](250) NULL,

	[action] [varchar](100) NULL,

	[neededPermission] [varchar](150) NOT NULL,

	[headID] [int] NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Backend_User_Desktops]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Backend_User_Desktops]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Backend_User_Desktops](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[iconID] [int] NOT NULL,

	[Icon] [varchar](250) NULL,

	[xPos] [float] NOT NULL,

	[yPos] [float] NOT NULL,

	[userid] [varchar](250) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Backend_Widgets]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Backend_Widgets]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Backend_Widgets](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[widgetName] [varchar](50) NOT NULL,

	[widgetPartialView] [varchar](50) NOT NULL,

	[neededPermission] [varchar](50) NOT NULL,

	[definedWidth] [int] NULL,

	[definedHeight] [int] NULL,

	[Icon] [varchar](50) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Categories]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Categories]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Categories](

	[PKID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,

	[contentType] [varchar](255) NOT NULL,

	[shortName] [varchar](255) NOT NULL,

	[longName] [varchar](255) NOT NULL,

	[subFrom] [varchar](250) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Category_Text]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Category_Text]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Category_Text](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[CategoryTitle] [varchar](200) NOT NULL,

	[CategoryDescription] [text] NULL,

	[CategoryId] [varchar](200) NOT NULL,

	[LangCode] [varchar](10) NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Content]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Content](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[CID] [uniqueidentifier] NOT NULL,

	[CreatorPKID] [uniqueidentifier] NOT NULL,

	[Locked] [bit] NOT NULL,

	[ratingGroupID] [int] NULL,

	[cDate] [datetime] NULL,

	[ContentType] [varchar](100) NOT NULL,

	[ContentRef] [int] NULL,

	[CreatorSpecialName] [varchar](250) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Content_Attachment]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Attachment]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Content_Attachment](

	[PKID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,

	[NID] [int] NOT NULL,

	[attachment_type] [varchar](255) NOT NULL,

	[content_type] [varchar](255) NOT NULL,

	[fPath] [varchar](255) NULL,

	[fName] [varchar](255) NULL,

	[thumpNail] [varchar](255) NULL,

	[AttachmentArgument] [varchar](250) NULL,

	[aTitle] [varchar](150) NULL,

	[AttachmentRemarks] [text] NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Content_Attachment_Roles]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Attachment_Roles]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Content_Attachment_Roles](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[aType] [varchar](250) NOT NULL,

	[RoleName] [varchar](250) NOT NULL,

	[RoleLangLine] [varchar](250) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Content_ClickCounter]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_ClickCounter]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Content_ClickCounter](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[IP] [varchar](150) NULL,

	[cid] [int] NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Content_Tags]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Tags]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Content_Tags](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[contentType] [varchar](50) NOT NULL,

	[tagName] [varchar](250) NOT NULL,

	[enableBrowsing] [int] NOT NULL,

	[tagNameSEO] [varchar](250) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Content_Tags2Content]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Tags2Content]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Content_Tags2Content](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[contentID] [int] NOT NULL,

	[tagID] [int] NOT NULL

) ON [PRIMARY]

END

GO

/****** Object:  Table [dbo].[nfcms_Content_Text]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Text]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Content_Text](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[ContentId] [int] NOT NULL,

	[LangCode] [varchar](10) NOT NULL,

	[Title] [varchar](255) NOT NULL,

	[SEOname] [varchar](255) NULL,

	[MetaKeyWords] [text] NULL,

	[MetaDescription] [varchar](255) NULL,

	[PreviewText] [varchar](255) NULL,

	[LongText] [text] NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Content_Types]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Content_Types]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Content_Types](

	[name] [varchar](250) NOT NULL,

	[controller] [varchar](250) NOT NULL,

	[actionpath] [varchar](350) NOT NULL,

	[createPartial] [varchar](250) NULL,

	[editPartial] [varchar](250) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Filemanagement_Crossbrowsers]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Filemanagement_Crossbrowsers]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Filemanagement_Crossbrowsers](

	[Id] [int] IDENTITY(1,1) NOT NULL,

	[browserID] [varchar](50) NOT NULL,

	[browserFullName] [varchar](250) NOT NULL,

	[FileFormat] [varchar](50) NOT NULL,

	[FileType] [varchar](50) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_FilemanagementControllers]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FilemanagementControllers]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_FilemanagementControllers](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[ControllerName] [varchar](50) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_FilemanagementControllersAcceptMimeTypes]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FilemanagementControllersAcceptMimeTypes]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_FilemanagementControllersAcceptMimeTypes](

	[id] [int] NOT NULL,

	[MimeType] [varchar](50) NULL,

	[cid] [int] NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_FilemanagementControllersAcceptProfiles]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FilemanagementControllersAcceptProfiles]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_FilemanagementControllersAcceptProfiles](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[pid] [int] NULL,

	[cid] [int] NULL

) ON [PRIMARY]

END

GO

/****** Object:  Table [dbo].[nfcms_FileManagementFileSettings]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FileManagementFileSettings]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_FileManagementFileSettings](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[SettingName] [varchar](50) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_FileManagementProfiles]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FileManagementProfiles]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_FileManagementProfiles](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[ProfileName] [varchar](50) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_FileManagementProfiles2FileSettings]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FileManagementProfiles2FileSettings]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_FileManagementProfiles2FileSettings](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[ProfileID] [int] NOT NULL,

	[SettingID] [int] NOT NULL

) ON [PRIMARY]

END

GO

/****** Object:  Table [dbo].[nfcms_Files]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Files]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Files](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[fpath] [text] NOT NULL,

	[aliasName] [varchar](255) NOT NULL,

	[needPermission] [varchar](255) NOT NULL,

	[active] [int] NOT NULL,

	[fileSize] [int] NULL,

	[ProfileID] [int] NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_FileSettingValues]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_FileSettingValues]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_FileSettingValues](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[ProfileID] [int] NOT NULL,

	[SettingID] [int] NOT NULL,

	[SettingValue] [varchar](50) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Global_Settings]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Global_Settings]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Global_Settings](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[settingName] [varchar](250) NOT NULL,

	[settingValue] [varchar](250) NOT NULL,

	[ContentType] [varchar](50) NULL,

	[s_type] [char](10) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Internal_Pro_Contra]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Internal_Pro_Contra]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Internal_Pro_Contra](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[refid] [int] NOT NULL,

	[pType] [varchar](50) NOT NULL,

	[pText] [varchar](150) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Internal_Rating]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Internal_Rating]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Internal_Rating](

	[topic] [varchar](250) NOT NULL,

	[refid] [int] NOT NULL,

	[stars] [int] NOT NULL,

	[id] [int] IDENTITY(1,1) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Lang_Codes]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Lang_Codes]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Lang_Codes](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[code] [varchar](50) NOT NULL,

	[name] [varchar](250) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Language]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Language]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Language](

	[Id] [int] IDENTITY(1,1) NOT NULL,

	[Name] [varchar](255) NOT NULL,

	[Content] [text] NOT NULL,

	[Package] [varchar](255) NOT NULL,

	[Code] [varchar](128) NOT NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Link_Identifiers]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Link_Identifiers]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Link_Identifiers](

	[id] [int] IDENTITY(1,3) NOT NULL,

	[identiferName] [varchar](50) NOT NULL,

	[theme] [varchar](50) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Links]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Links]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Links](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[LinkType] [varchar](255) NOT NULL,

	[LinkController] [varchar](255) NULL,

	[LinkAction] [varchar](255) NULL,

	[LinkHref] [varchar](255) NULL,

	[LinkText] [varchar](255) NOT NULL,

	[LinkRelationship] [varchar](250) NULL,

	[LinkIsActive] [varchar](50) NULL,

	[SublinkController] [varchar](250) NULL,

	[SublinkAction] [varchar](250) NULL,

	[SublinkFrom] [int] NULL,

	[NormalStateClass] [varchar](50) NULL,

	[HoverStateClass] [varchar](50) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Links2Identfiers]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Links2Identfiers]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Links2Identfiers](

	[linkID] [int] NOT NULL,

	[identifierID] [int] NOT NULL

) ON [PRIMARY]

END

GO

/****** Object:  Table [dbo].[nfcms_PermissionGroups]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_PermissionGroups]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_PermissionGroups](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[groupName] [varchar](250) NOT NULL,

	[isGuestGroup] [varchar](50) NOT NULL,

	[isDefaultGroup] [varchar](50) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Permissionkeys]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Permissionkeys]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Permissionkeys](

	[pkey] [varchar](150) NOT NULL,

	[defaultVal] [varchar](50) NOT NULL,

	[langLine] [varchar](50) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Permissions2Groups]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Permissions2Groups]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Permissions2Groups](

	[groupID] [varchar](50) NULL,

	[val] [varchar](50) NULL,

	[pk] [varchar](150) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Permissions2Users]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Permissions2Users]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Permissions2Users](

	[pk] [varchar](150) NULL,

	[groupID] [varchar](150) NULL,

	[val] [varchar](150) NULL,

	[usr] [varchar](150) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Profile_User_Values]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Profile_User_Values]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Profile_User_Values](

	[VarName] [varchar](150) NULL,

	[VarValue] [text] NULL,

	[PKID] [varchar](250) NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Profile_Vars]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Profile_Vars]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Profile_Vars](

	[Name] [varchar](150) NULL,

	[LangLine] [varchar](150) NULL,

	[Section] [varchar](150) NULL,

	[ViewName] [varchar](150) NULL,

	[Active] [varchar](50) NULL,

	[ShowInProfile] [varchar](50) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Rating]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Rating]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Rating](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[GroupID] [int] NOT NULL,

	[Name] [varchar](255) NOT NULL,

	[langCode] [varchar](55) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Rating_Groups]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Rating_Groups]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Rating_Groups](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[InternalName] [varchar](255) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_RegisteredMIMETypes]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_RegisteredMIMETypes]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_RegisteredMIMETypes](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[fileExstension] [varchar](50) NOT NULL,

	[MIMEType] [varchar](50) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Routing_Synonyms]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Routing_Synonyms]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Routing_Synonyms](

	[name] [varchar](150) NOT NULL,

	[controller] [varchar](150) NOT NULL,

	[action] [varchar](150) NOT NULL,

	[rpath] [varchar](250) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_SettingCategories]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_SettingCategories]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_SettingCategories](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[Name] [varchar](50) NOT NULL,

	[CatRel] [varchar](150) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_SettingModels]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_SettingModels]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_SettingModels](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[SettingName] [varchar](150) NOT NULL,

	[SettingLangLineLabel] [varchar](150) NOT NULL,

	[SettingLangLineDescr] [varchar](150) NOT NULL,

	[SettingDefaultValue] [text] NULL,

	[SettingRelation] [varchar](150) NOT NULL,

	[SettingType] [varchar](150) NOT NULL,

	[ValueType] [varchar](150) NOT NULL,

	[CID] [int] NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Settings2Permissions]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Settings2Permissions]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Settings2Permissions](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[FrontEndPM] [varchar](50) NOT NULL,

	[BackEndPM] [varchar](50) NOT NULL,

	[sid] [int] NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_SettingStores]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_SettingStores]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_SettingStores](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[sid] [int] NOT NULL,

	[val] [varchar](150) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_SettingStores2Locales]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_SettingStores2Locales]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_SettingStores2Locales](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[stid] [int] NOT NULL,

	[langLine] [varchar](250) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_SettingValues]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_SettingValues]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_SettingValues](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[SettingValue] [text] NOT NULL,

	[SettingID] [int] NOT NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END

GO

/****** Object:  Table [dbo].[nfcms_Sub_Categories]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Sub_Categories]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Sub_Categories](

	[PKID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,

	[CID] [uniqueidentifier] NULL,

	[ref] [varchar](255) NOT NULL,

	[shortName] [varchar](255) NOT NULL,

	[longName] [varchar](255) NOT NULL,

	[langCode] [varchar](128) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Thumpnails_Module]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Thumpnails_Module]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Thumpnails_Module](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[atID] [uniqueidentifier] NOT NULL,

	[LastModification] [datetime] NOT NULL,

	[Path] [varchar](255) NULL,

	[Width] [int] NULL,

	[Height] [int] NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_User2Settingvalues]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_User2Settingvalues]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_User2Settingvalues](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[sid] [int] NOT NULL,

	[uid] [varchar](250) NOT NULL,

	[vid] [int] NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Users]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Users]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Users](

	[PKID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,

	[Username] [varchar](255) NOT NULL,

	[Loginname] [varchar](255) NOT NULL,

	[ApplicationName] [varchar](255) NOT NULL,

	[Email] [varchar](128) NOT NULL,

	[Comment] [varchar](255) NULL,

	[Password] [varchar](128) NOT NULL,

	[PasswordQuestion] [varchar](255) NULL,

	[PasswordAnswer] [varchar](255) NULL,

	[IsApproved] [varchar](10) NULL,

	[LastActivityDate] [datetime] NULL,

	[LastLoginDate] [datetime] NULL,

	[LastPasswordChangedDate] [datetime] NULL,

	[CreationDate] [datetime] NULL,

	[IsOnLine] [varchar](10) NULL,

	[IsLockedOut] [varchar](10) NULL,

	[LastLockedOutDate] [datetime] NULL,

	[FailedPasswordAttemptCount] [int] NULL,

	[FailedPasswordAttemptWindowStart] [datetime] NULL,

	[FailedPasswordAnswerAttemptCount] [int] NULL,

	[FailedPasswordAnswerAttemptWindowStart] [datetime] NULL,

	[IsSubscriber] [varchar](10) NULL,

	[CustomerID] [text] NULL,

	[PermissionGroup] [varchar](50) NULL

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Users_Settings]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Users_Settings]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Users_Settings](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[upkid] [varchar](255) NOT NULL,

	[settingName] [varchar](250) NOT NULL,

	[settingValue] [varchar](250) NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfcms_Users_Settings_Table]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfcms_Users_Settings_Table]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfcms_Users_Settings_Table](

	[id] [int] IDENTITY(1212,1111) NOT NULL,

	[settingName] [varchar](255) NOT NULL,

	[settingDefaultVal] [varchar](250) NULL,

	[settingLangLine] [varchar](250) NULL,

	[settingLongDescription] [varchar](250) NULL,

	[SettingOrder] [int] NULL,

	[DataType] [varchar](50) NULL,

	[s_type] [char](10) NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

/****** Object:  Table [dbo].[nfmcs_Internal_Rating]    Script Date: 08.09.2013 16:33:51 ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

SET ANSI_PADDING ON

GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[nfmcs_Internal_Rating]') AND type in (N'U'))

BEGIN

CREATE TABLE [dbo].[nfmcs_Internal_Rating](

	[id] [int] IDENTITY(1,1) NOT NULL,

	[topic] [varchar](50) NOT NULL,

	[stars] [int] NOT NULL,

	[refid] [int] NOT NULL

) ON [PRIMARY]

END

GO

SET ANSI_PADDING OFF

GO

INSERT [dbo].[nfcms_Backend_Desktop_Backgrounds] ([userid], [backgroundImage], [backgroundColor], [backgroundAlign], [backgroundRepeat]) VALUES (N'8cbbfe36-c96e-46fc-a8af-b6757181e799', N'none', N'#18a82b', N'center', N'no-repeat')

SET IDENTITY_INSERT [dbo].[nfcms_Backend_Desktop_Icons] ON 



INSERT [dbo].[nfcms_Backend_Desktop_Icons] ([id], [langLine], [Icon], [Action]) VALUES (1, N'LANG_DESKTOPICON_APPLICATIONS', N'Defaulticon.png', N'widget:APPS:open')

INSERT [dbo].[nfcms_Backend_Desktop_Icons] ([id], [langLine], [Icon], [Action]) VALUES (2, N'LANG_DESKTOPICON_MANAGECONTENT', N'Documents.png', N'widget:CONTENT_LIST:open')

INSERT [dbo].[nfcms_Backend_Desktop_Icons] ([id], [langLine], [Icon], [Action]) VALUES (3, N'LANG_DESKTOP_ICONS_CONTENT_CATEGORIES', N'OSX.png', N'widget:CONTENT_CATEGORIES:open')

INSERT [dbo].[nfcms_Backend_Desktop_Icons] ([id], [langLine], [Icon], [Action]) VALUES (4, N'LANG_DESKTOPICONS_CONTENT_TAGS', N'Favorite.png', N'widget:CONTENT_TAGS:open')

INSERT [dbo].[nfcms_Backend_Desktop_Icons] ([id], [langLine], [Icon], [Action]) VALUES (5, N'LANG_DESKTOPICON_USERMANAGEMENT', N'User.png', N'widget:USER_MANAGEMENT:open')

INSERT [dbo].[nfcms_Backend_Desktop_Icons] ([id], [langLine], [Icon], [Action]) VALUES (6, N'LANG_DESKTOPICONS_MAILER_SETTINGS', N'Mail.png', N'widget:SETTINGS_MAILER:open')

SET IDENTITY_INSERT [dbo].[nfcms_Backend_Desktop_Icons] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Backend_Menu] ON 



INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (1, N'Inhalte', N'/Filemanagement/Icons/default.ico.png', N'#', N'USR_CAN_ENTER_BACKEND', 0)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (2, N'Inhalte verwalten', N'/Filemanagement/Icons/default.ico.png', N'widget:CONTENT_LIST', N'USR_CAN_ENTER_BACKEND', 1)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (3, N'Inhalte erstellen', N'/Filemanagement/Icons/default.ico.png', N'widget:CONTENT_CREATE', N'USR_CAN_ENTER_BACKEND', 1)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (4, N'Kategorien verwalten', N'/Filemanagement/Icons/default.ico.png', N'widget:CONTENT_CATEGORIES', N'USR_CAN_ENTER_BACKEND', 1)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (5, N'Einstellungen', N'/Filemanagement/Icons/default.ico.png', N'widget:SETTINGS', N'USR_CAN_ENTER_BACKEND', 0)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (6, N'Globale Einstellungen', N'/Filemanagement/Icons/default.ico.png', N'widget:GLOBAL_SETTINGS', N'USR_CAN_ENTER_BACKEND', 5)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (7, N'Benutzer Einstellungen', N'/Filemanagement/Icons/default.ico.png', N'widget:USER_SETTINGS', N'USR_CAN_ENTER_BACKEND', 5)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (8, N'Benutzer', N'/Filemanagement/Icons/default.ico.png', N'#', N'USR_CAN_ENTER_BACKEND', 0)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (9, N'Benutzer erstellen', N'/Filemanagement/Icons/default.ico.png', N'widget:USER_CREATE', N'USR_CAN_ENTER_BACKEND', 8)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (10, N'Benutzer verwalten', N'/Filemanagement/Icons/default.ico.png', N'widget:USER_MANAGE', N'USR_CAN_ENTER_BACKEND', 8)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (11, N'Rechte verwalten', N'/Filemanagement/Icons/default.ico.png', N'widget:PERMISSION_MANAGE', N'USR_CAN_ENTER_BACKEND', 8)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (12, N'Rechtegruppen verwalten', N'/Filemanagement/Icons/default.ico.png', N'widget:PERMISSIONGROUPS_MANAGE', N'USR_CAN_ENTER_BACKEND', 8)

INSERT [dbo].[nfcms_Backend_Menu] ([id], [menuTextLang], [iconUrl], [action], [neededPermission], [headID]) VALUES (13, N'Tags verwalten', N'/Filemanagement/Icons/default.ico.png', N'widget:CONTENT_TAGS', N'USR_CAN_ENTER_BACKEND', 1)

SET IDENTITY_INSERT [dbo].[nfcms_Backend_Menu] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Backend_User_Desktops] ON 



INSERT [dbo].[nfcms_Backend_User_Desktops] ([id], [iconID], [Icon], [xPos], [yPos], [userid]) VALUES (1, 1, NULL, 460, 330, N'8CBBFE36-C96E-46FC-A8AF-B6757181E799')

INSERT [dbo].[nfcms_Backend_User_Desktops] ([id], [iconID], [Icon], [xPos], [yPos], [userid]) VALUES (13, 2, NULL, 935.9942626953125, 226.98863220214844, N'8cbbfe36-c96e-46fc-a8af-b6757181e799')

INSERT [dbo].[nfcms_Backend_User_Desktops] ([id], [iconID], [Icon], [xPos], [yPos], [userid]) VALUES (14, 3, NULL, 358, 48, N'8cbbfe36-c96e-46fc-a8af-b6757181e799')

INSERT [dbo].[nfcms_Backend_User_Desktops] ([id], [iconID], [Icon], [xPos], [yPos], [userid]) VALUES (1014, 4, NULL, 821, -86, N'8cbbfe36-c96e-46fc-a8af-b6757181e799')

INSERT [dbo].[nfcms_Backend_User_Desktops] ([id], [iconID], [Icon], [xPos], [yPos], [userid]) VALUES (1015, 5, NULL, 705.9801025390625, 158.97726440429688, N'8cbbfe36-c96e-46fc-a8af-b6757181e799')

INSERT [dbo].[nfcms_Backend_User_Desktops] ([id], [iconID], [Icon], [xPos], [yPos], [userid]) VALUES (1016, 6, NULL, 171, 159, N'8cbbfe36-c96e-46fc-a8af-b6757181e799')

SET IDENTITY_INSERT [dbo].[nfcms_Backend_User_Desktops] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Backend_Widgets] ON 



INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (1, N'APPS', N'widget_apps', N'USR_CAN_ENTER_BACKEND', NULL, NULL, NULL)

INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (2, N'CONTENT_CATEGORIES', N'content_categories', N'USR_CAN_ENTER_BACKEND', NULL, NULL, N'OSX.png')

INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (3, N'CHANGE_BACKGROUND', N'change_background', N'USR_CAN_ENTER_BACKEND', 600, 350, N'Pictures.png')

INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (4, N'UPLOAD_BACKGROUND_IMAGE', N'upload', N'USR_CAN_ENTER_BACKEND', 300, 200, N'Network.png')

INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (7, N'CONTENT_LIST', N'list', N'USR_CAN_ENTER_BACKEND', 500, 340, N'Documents.png')

INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (8, N'CREATE_CONTENT', N'create', N'USR_CAN_ENTER_BACKEND', 970, 250, N'Documents.png')

INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (9, N'EDIT_CONTENT', N'edit', N'USR_CAN_ENTER_BACKEND', 960, 250, N'Documents.png')

INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (10, N'CONTENT_TAGS', N'tags', N'USR_CAN_ENTER_BACKEND', 500, 400, N'Favorite.png')

INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (11, N'USER_MANAGEMENT', N'UserList', N'USR_CAN_ENTER_BACKEND', 950, 340, N'User.png')

INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (12, N'SETTINGS_MAILER', N'settings', N'USR_CAN_ENTER_BACKEND', 500, 350, NULL)

INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (5, N'NEW_CATEGORY', N'new', N'USR_CAN_ENTER_BACKEND', 400, 400, N'System.png')

INSERT [dbo].[nfcms_Backend_Widgets] ([id], [widgetName], [widgetPartialView], [neededPermission], [definedWidth], [definedHeight], [Icon]) VALUES (6, N'EDIT_CATEGORY', N'edit', N'USR_CAN_ENTER_BACKEND', 500, 250, N'System.png')

SET IDENTITY_INSERT [dbo].[nfcms_Backend_Widgets] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Content_Attachment_Roles] ON 



INSERT [dbo].[nfcms_Content_Attachment_Roles] ([id], [aType], [RoleName], [RoleLangLine]) VALUES (1, N'image', N'INDEXIMG', N'LANG_AARGUMENTS_INDEXIMG')

INSERT [dbo].[nfcms_Content_Attachment_Roles] ([id], [aType], [RoleName], [RoleLangLine]) VALUES (2, N'image', N'GALLERY', N'LANG_AARGUMENTS_GALLERY')

INSERT [dbo].[nfcms_Content_Attachment_Roles] ([id], [aType], [RoleName], [RoleLangLine]) VALUES (3, N'video', N'GALLERY', N'LANG_AARGUMENTS_GALLERY')

SET IDENTITY_INSERT [dbo].[nfcms_Content_Attachment_Roles] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Content_Tags] ON 



INSERT [dbo].[nfcms_Content_Tags] ([id], [contentType], [tagName], [enableBrowsing], [tagNameSEO]) VALUES (2, N'eNews', N'PC', 1, N'PC')

INSERT [dbo].[nfcms_Content_Tags] ([id], [contentType], [tagName], [enableBrowsing], [tagNameSEO]) VALUES (3, N'eNews', N'PS4', 1, N'PS4')

INSERT [dbo].[nfcms_Content_Tags] ([id], [contentType], [tagName], [enableBrowsing], [tagNameSEO]) VALUES (6, N'eArticle', N'PS4', 1, N'PS4')

SET IDENTITY_INSERT [dbo].[nfcms_Content_Tags] OFF

 


INSERT [dbo].[nfcms_Content_Types] ([name], [controller], [actionpath], [createPartial], [editPartial]) VALUES (N'eNews', N'News', N'{id}-{SEOname}-', NULL, NULL)

INSERT [dbo].[nfcms_Content_Types] ([name], [controller], [actionpath], [createPartial], [editPartial]) VALUES (N'eArticle', N'Article', N'Show/{id}-{SEOname}-', NULL, NULL)

SET IDENTITY_INSERT [dbo].[nfcms_Filemanagement_Crossbrowsers] ON 



INSERT [dbo].[nfcms_Filemanagement_Crossbrowsers] ([Id], [browserID], [browserFullName], [FileFormat], [FileType]) VALUES (1, N'chrome', N'Google Chrome', N'm4v', N'video')

INSERT [dbo].[nfcms_Filemanagement_Crossbrowsers] ([Id], [browserID], [browserFullName], [FileFormat], [FileType]) VALUES (2, N'firefox', N'Mozilla Firefox', N'webm', N'video')

INSERT [dbo].[nfcms_Filemanagement_Crossbrowsers] ([Id], [browserID], [browserFullName], [FileFormat], [FileType]) VALUES (3, N'default', N'Default Browser Detection', N'mp4', N'video')

SET IDENTITY_INSERT [dbo].[nfcms_Filemanagement_Crossbrowsers] OFF

SET IDENTITY_INSERT [dbo].[nfcms_FilemanagementControllers] ON 



INSERT [dbo].[nfcms_FilemanagementControllers] ([id], [ControllerName]) VALUES (1, N'UserAvatar')

INSERT [dbo].[nfcms_FilemanagementControllers] ([id], [ControllerName]) VALUES (2, N'FileManagement')

SET IDENTITY_INSERT [dbo].[nfcms_FilemanagementControllers] OFF

INSERT [dbo].[nfcms_FilemanagementControllersAcceptMimeTypes] ([id], [MimeType], [cid]) VALUES (0, N'image/*', 2)

INSERT [dbo].[nfcms_FilemanagementControllersAcceptMimeTypes] ([id], [MimeType], [cid]) VALUES (0, N'image/*', 1)

SET IDENTITY_INSERT [dbo].[nfcms_FilemanagementControllersAcceptProfiles] ON 



INSERT [dbo].[nfcms_FilemanagementControllersAcceptProfiles] ([id], [pid], [cid]) VALUES (1, 1, 1)

INSERT [dbo].[nfcms_FilemanagementControllersAcceptProfiles] ([id], [pid], [cid]) VALUES (2, 2, 2)

INSERT [dbo].[nfcms_FilemanagementControllersAcceptProfiles] ([id], [pid], [cid]) VALUES (3, 3, 2)

SET IDENTITY_INSERT [dbo].[nfcms_FilemanagementControllersAcceptProfiles] OFF

SET IDENTITY_INSERT [dbo].[nfcms_FileManagementFileSettings] ON 



INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (1, N'USE_WATERMARK_IMAGE')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (2, N'USE_WATERMARK_TEXT')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (3, N'WATERMARK_IMAGE_MARGIN')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (4, N'WATERMARK_IMAGE_PERC_SIZE')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (5, N'WATERMARK_IMAGE_OPACITY')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (7, N'WATERMARK_TEXT_FONTNAME')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (10, N'WATERMARK_TEXT_COLOR_GREEN')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (12, N'WATERMARK_TEXT_OPACITY')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (6, N'WATERMARK_IMAGE_LOCATION')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (8, N'WATERMARK_TEXT_LOCATION')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (9, N'WATERMARK_TEXT_COLOR_RED')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (11, N'WATERMARK_TEXT_COLOR_BLUE')

INSERT [dbo].[nfcms_FileManagementFileSettings] ([id], [SettingName]) VALUES (13, N'WATERMARK_TEXT_TEXT')

SET IDENTITY_INSERT [dbo].[nfcms_FileManagementFileSettings] OFF

SET IDENTITY_INSERT [dbo].[nfcms_FileManagementProfiles] ON 



INSERT [dbo].[nfcms_FileManagementProfiles] ([id], [ProfileName]) VALUES (1, N'UserAvatar')

INSERT [dbo].[nfcms_FileManagementProfiles] ([id], [ProfileName]) VALUES (2, N'default')

INSERT [dbo].[nfcms_FileManagementProfiles] ([id], [ProfileName]) VALUES (3, N'contentImages')

SET IDENTITY_INSERT [dbo].[nfcms_FileManagementProfiles] OFF

SET IDENTITY_INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ON 



INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (1, 1, 1)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (2, 1, 2)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (3, 1, 3)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (4, 1, 4)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (5, 1, 5)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (7, 1, 7)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (10, 1, 10)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (12, 1, 12)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (6, 1, 6)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (8, 1, 8)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (9, 1, 9)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (11, 1, 11)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (13, 1, 13)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (1, 1, 1)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (2, 1, 2)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (3, 1, 3)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (4, 1, 4)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (5, 1, 5)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (7, 1, 7)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (10, 1, 10)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (12, 1, 12)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (6, 1, 6)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (8, 1, 8)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (9, 1, 9)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (11, 1, 11)

INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] ([id], [ProfileID], [SettingID]) VALUES (13, 1, 13)

SET IDENTITY_INSERT [dbo].[nfcms_FileManagementProfiles2FileSettings] OFF



SET IDENTITY_INSERT [dbo].[nfcms_FileSettingValues] ON 



INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (1, 1, 1, N'FALSE')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (2, 1, 2, N'FALSE')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (3, 1, 3, N'2')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (4, 1, 4, N'0.19')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (5, 1, 5, N'0.60')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (7, 1, 7, N'Verdana')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (10, 1, 10, N'255')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (12, 1, 12, N'55')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (6, 1, 6, N'LEFT_BOTTOM')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (8, 1, 8, N'RIGHT_BOTTOM')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (9, 1, 9, N'255')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (11, 1, 11, N'255')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (13, 1, 13, N'Seen@YouSite.info')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (1, 1, 1, N'FALSE')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (2, 1, 2, N'FALSE')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (3, 1, 3, N'2')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (4, 1, 4, N'0.19')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (5, 1, 5, N'0.60')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (7, 1, 7, N'Verdana')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (10, 1, 10, N'255')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (12, 1, 12, N'55')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (6, 1, 6, N'LEFT_BOTTOM')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (8, 1, 8, N'RIGHT_BOTTOM')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (9, 1, 9, N'255')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (11, 1, 11, N'255')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (13, 1, 13, N'Seen@YouSite.info')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (14, 2, 0, N'TRUE')

INSERT [dbo].[nfcms_FileSettingValues] ([id], [ProfileID], [SettingID], [SettingValue]) VALUES (15, 3, 0, N'TRUE')

SET IDENTITY_INSERT [dbo].[nfcms_FileSettingValues] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Global_Settings] ON 



INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (1212, N'__USER__langCode', N'de-DE', N'MAIN', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (2323, N'GLOBAL_INDEX_MAX_ENTRIES', N'10', N'MAIN', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (3434, N'GLOBAL_NEWS_MAX_ENTRIES', N'10', N'eNews', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (4545, N'GLOBAL_GUESTPKID', N'BDE3F3BE-6B25-44B7-816A-CF3C134266BE', N'MAIN', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (5656, N'GLOBAL_NEWS_MAX_GALLERY_IMAGES', N'10', N'eNews', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (6767, N'GLOBAL_NEWS_MAX_GALLERY_VIDEOS', N'10', N'eNews', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (7878, N'GLOBAL_SOCIAL_FB_APPID', N'403659876312915', N'iSocial', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (8989, N'GLOBAL_SOCIAL_FB_ENABLED', N'true', N'iSocial', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (10100, N'GLOBAL_DEFAULT_LANGUAGE', N'de-DE', N'MAIN', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (11211, N'GLOBAL_SETTING_DEFAULT_THEME', N'nftheme', N'MAIN', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (12322, N'GLOBALSETTING.GLOBABL_MAINSITENAME', N'NetworkFreaks.de', N'MAIN', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (13433, N'GLOBAL_Article_MAX_GALLERY_IMAGES', N'10', N'eArticle', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (14544, N'GLOBAL_Article_MAX_GALLERY_VIDEOS', N'10', N'eArticle', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (15655, N'GLOBAL_ADDTHIS_ENABLED', N'true', N'iSocial', NULL)

INSERT [dbo].[nfcms_Global_Settings] ([id], [settingName], [settingValue], [ContentType], [s_type]) VALUES (16766, N'GLOBAL_ADDTHIS_USERNAME', N'networkfreaksde', N'iSocial', NULL)

SET IDENTITY_INSERT [dbo].[nfcms_Global_Settings] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Internal_Pro_Contra] ON 



INSERT [dbo].[nfcms_Internal_Pro_Contra] ([id], [refid], [pType], [pText]) VALUES (1, 1212, N'pro', N'Gute Grafik')

INSERT [dbo].[nfcms_Internal_Pro_Contra] ([id], [refid], [pType], [pText]) VALUES (2, 1212, N'pro', N'Guter Sound')

INSERT [dbo].[nfcms_Internal_Pro_Contra] ([id], [refid], [pType], [pText]) VALUES (3, 1212, N'contra', N'Gameplay ist nicht vorteilhaft')

INSERT [dbo].[nfcms_Internal_Pro_Contra] ([id], [refid], [pType], [pText]) VALUES (4, 1212, N'contra', N'Multiplayer d�nn')

INSERT [dbo].[nfcms_Internal_Pro_Contra] ([id], [refid], [pType], [pText]) VALUES (1, 1212, N'pro', N'Gute Grafik')

INSERT [dbo].[nfcms_Internal_Pro_Contra] ([id], [refid], [pType], [pText]) VALUES (2, 1212, N'pro', N'Guter Sound')

INSERT [dbo].[nfcms_Internal_Pro_Contra] ([id], [refid], [pType], [pText]) VALUES (3, 1212, N'contra', N'Gameplay ist nicht vorteilhaft')

INSERT [dbo].[nfcms_Internal_Pro_Contra] ([id], [refid], [pType], [pText]) VALUES (4, 1212, N'contra', N'Multiplayer d�nn')

SET IDENTITY_INSERT [dbo].[nfcms_Internal_Pro_Contra] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Internal_Rating] ON 



INSERT [dbo].[nfcms_Internal_Rating] ([topic], [refid], [stars], [id]) VALUES (N'Grafik', 1212, 5, 1)

INSERT [dbo].[nfcms_Internal_Rating] ([topic], [refid], [stars], [id]) VALUES (N'Sound', 1212, 2, 2)

INSERT [dbo].[nfcms_Internal_Rating] ([topic], [refid], [stars], [id]) VALUES (N'Gameplayk', 1212, 3, 3)

INSERT [dbo].[nfcms_Internal_Rating] ([topic], [refid], [stars], [id]) VALUES (N'Multiplayer', 1212, 1, 4)

INSERT [dbo].[nfcms_Internal_Rating] ([topic], [refid], [stars], [id]) VALUES (N'Grafik', 1212, 5, 1)

INSERT [dbo].[nfcms_Internal_Rating] ([topic], [refid], [stars], [id]) VALUES (N'Sound', 1212, 2, 2)

INSERT [dbo].[nfcms_Internal_Rating] ([topic], [refid], [stars], [id]) VALUES (N'Gameplayk', 1212, 3, 3)

INSERT [dbo].[nfcms_Internal_Rating] ([topic], [refid], [stars], [id]) VALUES (N'Multiplayer', 1212, 1, 4)

SET IDENTITY_INSERT [dbo].[nfcms_Internal_Rating] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Lang_Codes] ON 



INSERT [dbo].[nfcms_Lang_Codes] ([id], [code], [name]) VALUES (1212, N'de-DE', N'Deutsch')

INSERT [dbo].[nfcms_Lang_Codes] ([id], [code], [name]) VALUES (2323, N'en-US', N'English')

SET IDENTITY_INSERT [dbo].[nfcms_Lang_Codes] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Language] ON 



INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (1, N'LANG_LINKS_HOME', N'Startseite', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (2, N'LANG_LINKS_HOME', N'Home', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (3, N'LANG_LINKS_GAMES', N'Games', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (4, N'LANG_LINKS_GAMES', N'Games', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (5, N'LANG_LINKS_FORUM', N'Forum', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (6, N'LANG_LINKS_FORUM', N'Forums', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (7, N'LANG_LINKS_PC', N'PC', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (8, N'LANG_LINKS_PC', N'PC', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (9, N'LANG_LINKS_NEWSARCHIVE', N'Newsarchiv', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (10, N'LANG_LINKS_NEWSARCHIVE', N'News archive', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (11, N'LANG_LINKS_ARTICLEARCHIVE', N'Artikelarchiv', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (12, N'LANG_LINKS_ARTICLEARCHIVE', N'Article archive', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (13, N'LANG_PERMISSIONS_WRITE_COMMENTS', N'User kann Kommentare verfassen?', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (14, N'LANG_LINKS_CONTACT', N'Kontakt', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (15, N'LANG_LINKS_IMPRESSUM', N'Impressum', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (16, N'LANG_LINKS_RSS', N'RSS-Feed', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (17, N'LANG_LINKS_NEWS', N'News', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (18, N'LANG_LINKS_NEWS_ARCHIVE', N'Archiv', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (19, N'LANG_USR_SETTING_XTEST_LABEL', N'Test123', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (20, N'LANG_USR_SETTING_XTEST_DESCR', N'Test Test Test', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (21, N'LANG_USR_SETTING_TESTSETTING1_LABEL', N'Testststs', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (22, N'LANG_USR_SETTING_TESTSETTING1_DESCR', N'dassdaadadsads', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (23, N'LANG_USR_SETTING_ID0_TEST3_LABEL_GENERATED', N'dfsdfdfasasd', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (24, N'LANG_USR_SETTING_ID0_TEST3_DESCR_GENERATED', N'fdakdbhasdkhasd', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (25, N'LANG_USR_SETTING_ID0_TEST4_LABEL_GENERATED', N'Labeltest', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (26, N'LANG_USR_SETTING_ID0_TEST4_DESCR_GENERATED', N'adskjasdajksd', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (27, N'LANG_GLB_SETTING_ID0_GLOBAL_INDEX_MAX_ENTRIES_LABEL_GENERATED', N'Maximale Eintr�ge auf der Startseite', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (28, N'LANG_GLB_SETTING_ID0_GLOBAL_INDEX_MAX_ENTRIES_DESCR_GENERATED', N'Hier k�nnen Sie die Maximalanzahl der Beitr�ge auf der Startseite einstellen', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (29, N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_ENABLED_LABEL_GENERATED', N'AddThis aktiv', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (30, N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_ENABLED_DESCR_GENERATED', N'Soll Addthis(&copy) zum hinzuf�gen der Share Buttons (FaceBook, Google+ etc...) geladen werden?', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (31, N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_ENABLED_LABEL_GENERATED', N'AddThis aktiv', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (32, N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_ENABLED_DESCR_GENERATED', N'Add This laden?', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (33, N'LANG_USR_SETTING_ID0_VIEW_EMAIL_LABEL_GENERATED', N'eMail in Profil anzeigen', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (34, N'LANG_USR_SETTING_ID0_VIEW_EMAIL_DESCR_GENERATED', N'M�chstest du, dass deine eMail angezeigt wird?', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (35, N'LANG_USR_SETTING_ID0_PROFILE_PUBLIC_LABEL_GENERATED', N'Profil �ffentlich', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (36, N'LANG_USR_SETTING_ID0_PROFILE_PUBLIC_DESCR_GENERATED', N'Wer darf dein Profil sehen?', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (37, N'LANG_ACCOUNT_SUCCESS_TEXT', N'Deine Daten wurden erfolgreich gespeichert.', N'ACCOUNT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (38, N'LANG_ACCOUNT_SUCCESS_TEXT', N'Your data has been saved successfull.', N'ACCOUNT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (39, N'LANG_ACCOUNT_SUCCESS_TITLE', N'Daten gespeichert', N'ACCOUNT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (40, N'LANG_ACCOUNT_SUCCESS_TITLE', N'Data saved', N'ACCOUNT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (41, N'LANG_ACCOUNT_SUCCESS_TITLE_ERROR', N'Fehler beim Speichern', N'ACCOUNT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (42, N'LANG_ACCOUNT_SUCCESS_TITLE_ERROR', N'Error on saving data', N'ACCOUNT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (43, N'LANG_ACCOUNT_SUCCESS_TEXT_ERROR', N'Es konnten %%NUMBER%% Daten nicht gespeichert werden. Fehlende Zugriffsrechte oder Serverfehler.', N'ACCOUNT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (44, N'LANG_ACCOUNT_SUCCESS_TEXT_ERROR', N'Unable to save %%NUMBER%% of the data. Maybe missing permissions or internal server error.', N'ACCOUNT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (45, N'LANG_DESKTOPICON_APPLICATIONS', N'Anwendungen', N'DESKTOP_ICONS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (46, N'LANG_USR_SETTING_ID0_TESTSETTING1_LABEL_GENERATED', N'Testsetting Man!', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (47, N'LANG_USR_SETTING_ID0_TEST2_LABEL_GENERATED', N'hsjsad', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (48, N'LANG_USR_SETTING_ID0_TEST2_DESCR_GENERATED', N'dsjashasdh', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (49, N'LANG_USR_SETTING_ID0_TESTSETTING1_DESCR_GENERATED', N'Test settttttiiiiing', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (50, N'LANG_USR_SETTING_ID0_ASDDASDSA_LABEL_GENERATED', N'sadasdasdasd', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (51, N'LANG_USR_SETTING_ID0_ASDDASDSA_DESCR_GENERATED', N'sdaasdasdasd', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (52, N'LANG_USR_SETTING_ID0_RECIEVE_PMS_LABEL_GENERATED', N'Private Nachrichten empfangen', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (53, N'LANG_USR_SETTING_ID0_RECIEVE_PMS_DESCR_GENERATED', N'Andere Benutzer d�rfen mir Private Nachrichten senden.', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (54, N'LANG_USR_SETTING_ID0_REVIEVE_EMAILS_LABEL_GENERATED', N'M�chtest du E-Mails empfangen?', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (55, N'LANG_USR_SETTING_ID0_REVIEVE_EMAILS_DESCR_GENERATED', N'Test', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (56, N'LANG_STORE_SHOW_EMAIL_YES', N'Ja', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (57, N'LANG_STORE_SHOW_EMAIL_YES', N'Ja', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (58, N'LANG_STORE_SHOW_EMAIL_NO', N'Nein', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (59, N'LANG_STORE_SHOW_EMAIL_NO', N'Ja', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (60, N'LANG_STORE_RECIEVE_PMS_YES', N'Ja', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (61, N'LANG_STORE_RECIEVE_PMS_YES', N'Ja', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (62, N'LANG_STORE_RECIEVE_PMS_NO', N'Nein', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (63, N'LANG_STORE_RECIEVE_PMS_NO', N'Ja', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (64, N'LANG_STORE_PROFILE_PUBLIC_ALL', N'Alle Besucher', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (65, N'LANG_STORE_PROFILE_PUBLIC_ALL', N'All Visitors', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (66, N'LANG_STORE_PROFILE_PUBLIC_ONLY_REGGED', N'Nur angemeldete Besucher', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (67, N'LANG_STORE_PROFILE_PUBLIC_ONLY_REGGED', N'Only registered Vistors', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (68, N'LANG_STORE_PROFILE_PUBLIC_ONLY_FRIENDS', N'Nur meine Freunde', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (69, N'LANG_STORE_PROFILE_PUBLIC_ONLY_FRIENDS', N'Only my friends', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (70, N'LANG_STORE_PROFILE_PUBLIC_NOT', N'Nicht �ffentlich', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (71, N'LANG_STORE_PROFILE_PUBLIC_NOT', N'Not public', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (72, N'LANG_LINKS_HOME', N'Startseite', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (73, N'LANG_LINKS_HOME', N'Home', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (74, N'LANG_LINKS_GAMES', N'Games', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (75, N'LANG_LINKS_GAMES', N'Games', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (76, N'LANG_LINKS_FORUM', N'Forum', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (77, N'LANG_LINKS_FORUM', N'Forums', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (78, N'LANG_LINKS_PC', N'PC', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (79, N'LANG_LINKS_PC', N'PC', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (80, N'LANG_LINKS_NEWSARCHIVE', N'Newsarchiv', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (81, N'LANG_LINKS_NEWSARCHIVE', N'News archive', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (82, N'LANG_LINKS_ARTICLEARCHIVE', N'Artikelarchiv', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (83, N'LANG_LINKS_ARTICLEARCHIVE', N'Article archive', N'LINKS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (84, N'LANG_PERMISSIONS_WRITE_COMMENTS', N'User kann Kommentare verfassen?', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (85, N'LANG_LINKS_CONTACT', N'Kontakt', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (86, N'LANG_LINKS_IMPRESSUM', N'Impressum', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (87, N'LANG_LINKS_RSS', N'RSS-Feed', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (88, N'LANG_LINKS_NEWS', N'News', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (89, N'LANG_LINKS_NEWS_ARCHIVE', N'Archiv', N'LINKS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (90, N'LANG_USR_SETTING_XTEST_LABEL', N'Test123', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (91, N'LANG_USR_SETTING_XTEST_DESCR', N'Test Test Test', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (92, N'LANG_USR_SETTING_TESTSETTING1_LABEL', N'Testststs', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (93, N'LANG_USR_SETTING_TESTSETTING1_DESCR', N'dassdaadadsads', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (94, N'LANG_USR_SETTING_ID0_TEST3_LABEL_GENERATED', N'dfsdfdfasasd', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (95, N'LANG_USR_SETTING_ID0_TEST3_DESCR_GENERATED', N'fdakdbhasdkhasd', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (96, N'LANG_USR_SETTING_ID0_TEST4_LABEL_GENERATED', N'Labeltest', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (97, N'LANG_USR_SETTING_ID0_TEST4_DESCR_GENERATED', N'adskjasdajksd', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (98, N'LANG_GLB_SETTING_ID0_GLOBAL_INDEX_MAX_ENTRIES_LABEL_GENERATED', N'Maximale Eintr�ge auf der Startseite', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (99, N'LANG_GLB_SETTING_ID0_GLOBAL_INDEX_MAX_ENTRIES_DESCR_GENERATED', N'Hier k�nnen Sie die Maximalanzahl der Beitr�ge auf der Startseite einstellen', N'GLOBAL_SETTINGS', N'de-DE')

GO

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (100, N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_ENABLED_LABEL_GENERATED', N'AddThis aktiv', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (101, N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_ENABLED_DESCR_GENERATED', N'Soll Addthis(&copy) zum hinzuf�gen der Share Buttons (FaceBook, Google+ etc...) geladen werden?', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (102, N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_ENABLED_LABEL_GENERATED', N'AddThis aktiv', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (103, N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_ENABLED_DESCR_GENERATED', N'Add This laden?', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (104, N'LANG_USR_SETTING_ID0_VIEW_EMAIL_LABEL_GENERATED', N'eMail in Profil anzeigen', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (105, N'LANG_USR_SETTING_ID0_VIEW_EMAIL_DESCR_GENERATED', N'M�chstest du, dass deine eMail angezeigt wird?', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (106, N'LANG_USR_SETTING_ID0_PROFILE_PUBLIC_LABEL_GENERATED', N'Profil �ffentlich', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (107, N'LANG_USR_SETTING_ID0_PROFILE_PUBLIC_DESCR_GENERATED', N'Wer darf dein Profil sehen?', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (108, N'LANG_ACCOUNT_SUCCESS_TEXT', N'Deine Daten wurden erfolgreich gespeichert.', N'ACCOUNT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (109, N'LANG_ACCOUNT_SUCCESS_TEXT', N'Your data has been saved successfull.', N'ACCOUNT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (110, N'LANG_ACCOUNT_SUCCESS_TITLE', N'Daten gespeichert', N'ACCOUNT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (111, N'LANG_ACCOUNT_SUCCESS_TITLE', N'Data saved', N'ACCOUNT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (112, N'LANG_ACCOUNT_SUCCESS_TITLE_ERROR', N'Fehler beim Speichern', N'ACCOUNT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (113, N'LANG_ACCOUNT_SUCCESS_TITLE_ERROR', N'Error on saving data', N'ACCOUNT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (114, N'LANG_ACCOUNT_SUCCESS_TEXT_ERROR', N'Es konnten %%NUMBER%% Daten nicht gespeichert werden. Fehlende Zugriffsrechte oder Serverfehler.', N'ACCOUNT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (115, N'LANG_ACCOUNT_SUCCESS_TEXT_ERROR', N'Unable to save %%NUMBER%% of the data. Maybe missing permissions or internal server error.', N'ACCOUNT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (116, N'LANG_DESKTOPICON_APPLICATIONS', N'Anwendungen', N'DESKTOP_ICONS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (117, N'LANG_USR_SETTING_ID0_TESTSETTING1_LABEL_GENERATED', N'Testsetting Man!', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (118, N'LANG_USR_SETTING_ID0_TEST2_LABEL_GENERATED', N'hsjsad', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (119, N'LANG_USR_SETTING_ID0_TEST2_DESCR_GENERATED', N'dsjashasdh', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (120, N'LANG_USR_SETTING_ID0_TESTSETTING1_DESCR_GENERATED', N'Test settttttiiiiing', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (121, N'LANG_USR_SETTING_ID0_ASDDASDSA_LABEL_GENERATED', N'sadasdasdasd', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (122, N'LANG_USR_SETTING_ID0_ASDDASDSA_DESCR_GENERATED', N'sdaasdasdasd', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (123, N'LANG_USR_SETTING_ID0_RECIEVE_PMS_LABEL_GENERATED', N'Private Nachrichten empfangen', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (124, N'LANG_USR_SETTING_ID0_RECIEVE_PMS_DESCR_GENERATED', N'Andere Benutzer d�rfen mir Private Nachrichten senden.', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (125, N'LANG_USR_SETTING_ID0_REVIEVE_EMAILS_LABEL_GENERATED', N'M�chtest du E-Mails empfangen?', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (126, N'LANG_USR_SETTING_ID0_REVIEVE_EMAILS_DESCR_GENERATED', N'Test', N'USER_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (127, N'LANG_STORE_SHOW_EMAIL_YES', N'Ja', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (128, N'LANG_STORE_SHOW_EMAIL_YES', N'Ja', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (129, N'LANG_STORE_SHOW_EMAIL_NO', N'Nein', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (130, N'LANG_STORE_SHOW_EMAIL_NO', N'Ja', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (131, N'LANG_STORE_RECIEVE_PMS_YES', N'Ja', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (132, N'LANG_STORE_RECIEVE_PMS_YES', N'Ja', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (133, N'LANG_STORE_RECIEVE_PMS_NO', N'Nein', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (134, N'LANG_STORE_RECIEVE_PMS_NO', N'Ja', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (135, N'LANG_STORE_PROFILE_PUBLIC_ALL', N'Alle Besucher', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (136, N'LANG_STORE_PROFILE_PUBLIC_ALL', N'All Visitors', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (137, N'LANG_STORE_PROFILE_PUBLIC_ONLY_REGGED', N'Nur angemeldete Besucher', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (138, N'LANG_STORE_PROFILE_PUBLIC_ONLY_REGGED', N'Only registered Vistors', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (139, N'LANG_STORE_PROFILE_PUBLIC_ONLY_FRIENDS', N'Nur meine Freunde', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (140, N'LANG_STORE_PROFILE_PUBLIC_ONLY_FRIENDS', N'Only my friends', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (141, N'LANG_STORE_PROFILE_PUBLIC_NOT', N'Nicht �ffentlich', N'SETTING_STORES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (142, N'LANG_STORE_PROFILE_PUBLIC_NOT', N'Not public', N'SETTING_STORES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (143, N'LANG_BACKEND_WIDGETS_APPS_TITLE', N'Anwendungen', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (144, N'LANG_BACKEND_WIDGETS_APPS_TITLE', N'Applications', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (145, N'LANG_DESKTOPICON_MANAGECONTENT', N'Inhalte verwalten', N'DESKTOP_ICONS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (146, N'LANG_DESKTOP_ICONS_CONTENT_CATEGORIES', N'Kategorien verwalten', N'DESKTOP_ICONS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (147, N'LANG_DESKTOP_ICONS_CONTENT_CATEGORIES', N'Manage Categories', N'DESKTOP_ICONS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (148, N'LANG_BACKEND_WIDGETS_CONTENT_CATEGORIES_TITLE', N'Kategorien verwalten', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (149, N'LANG_BACKEND_WIDGETS_CONTENT_CATEGORIES_TITLE', N'Manage Categories', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (150, N'LANG_BACKEND_WIDGETS_CHANGE_BACKGROUND_TITLE', N'Hintergrund �ndern', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (151, N'LANG_BACKEND_WIDGETS_CHANGE_BACKGROUND_TITLE', N'Change Background', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (152, N'LANG_BACKEND_WIDGETS_UPLOAD_BACKGROUND_IMAGE_TITLE', N'Hintergrundbild hochladen', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (153, N'LANG_BACKEND_WIDGETS_UPLOAD_BACKGROUND_IMAGE_TITLE', N'Upload background image', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (154, N'LANG_BACKEND_WIDGETS_NEW_CATEGORY_TITLE', N'Neue Kategorie erstellen', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (155, N'LANG_BACKEND_WIDGETS_NEW_CATEGORY_TITLE', N'Create new category', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (156, N'LANG_BACKEND_WIDGETS_EDIT_CATEGORY_TITLE', N'Kategorie bearbeiten', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (157, N'LANG_BACKEND_WIDGETS_EDIT_CATEGORY_TITLE', N'Edit category', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (158, N'LANG_CTYPE_ENEWS', N'Newsbeitr�ge', N'CONTENT_TYPES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (159, N'LANG_CTYPE_ENEWS', N'News Posts', N'CONTENT_TYPES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (160, N'LANG_CTYPE_EARTICLE', N'Artikel', N'CONTENT_TYPES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (161, N'LANG_CTYPE_EARTICLE', N'Article', N'CONTENT_TYPES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (162, N'LANG_AARGUMENTS_INDEXIMG', N'Vorschaubild', N'ATTACHMENT_ROLES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (163, N'LANG_AARGUMENTS_INDEXIMG', N'Preview Image', N'ATTACHMENT_ROLES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (164, N'LANG_AARGUMENTS_GALLERY', N'Gallerie', N'ATTACHMENT_ROLES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (165, N'LANG_AARGUMENTS_GALLERY', N'Gallery', N'ATTACHMENT_ROLES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (166, N'LANG_BACKEND_WIDGETS_EDIT_CONTENT_TITLE', N'Inhalt bearbeiten', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (167, N'LANG_BACKEND_WIDGETS_EDIT_CONTENT_TITLE', N'Edit Content', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (168, N'LANG_BACKEND_WIDGETS_CREATE_CONTENT_TITLE', N'Inhalt anlegen', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (169, N'LANG_BACKEND_WIDGETS_CREATE_CONTENT_TITLE', N'Create Content', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (170, N'LANG_BACKEND_WIDGETS_CONTENT_LIST_TITLE', N'Inhalte verwalten', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (171, N'LANG_BACKEND_WIDGETS_CONTENT_LIST_TITLE', N'Manage Content', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (172, N'LANG_BACKEND_CATLIST_NEW', N'Neue Kategorie', N'BACKEND-CATEGORIES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (173, N'LANG_BACKEND_CATLIST_NEW', N'New Category', N'BACKEND-CATEGORIES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (174, N'LANG_BACKEND_CATLIST_PLEASE_SELECT_CT', N'Bitte w&auml;hlen Sie einen Inhaltstyp aus, um Kategorien zu verwalten.', N'BACKEND-CATEGORIES', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (175, N'LANG_BACKEND_CATLIST_PLEASE_SELECT_CT', N'Please select a content type', N'BACKEND-CATEGORIES', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (176, N'LANG_GLOBAL_EDIT_SINGLE', N'Bearbeiten', N'GLOBAL', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (177, N'LANG_GLOBAL_EDIT_SINGLE', N'Edit', N'GLOBAL', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (178, N'LANG_GLOBAL_DELETE_SINGLE', N'L&ouml;schen', N'GLOBAL', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (179, N'LANG_GLOBAL_DELETE_SINGLE', N'Delete', N'GLOBAL', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (180, N'LANG_GLOBAL_REFRESH_SINGLE', N'Aktualisieren', N'GLOBAL', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (181, N'LANG_GLOBAL_REFRESH_SINGLE', N'Refresh', N'GLOBAL', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (182, N'LANG_BACKEND_WIDGETS_CONTENT_TAGS_TITLE', N'Inhalte-Tags verwalten', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (183, N'LANG_BACKEND_WIDGETS_CONTENT_TAGS_TITLE', N'Manage Content Tags', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (184, N'LANG_DESKTOPICONS_CONTENT_TAGS', N'Inhalte-Tags verwalten', N'DESKTOP_ICONS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (185, N'LANG_DESKTOPICONS_CONTENT_TAGS', N'Manage Content Tags', N'DESKTOP_ICONS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (186, N'C_TAGS_CREATE', N'Tag erstellen', N'CONTENT_TAGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (187, N'C_TAGS_CREATE', N'Create Tag', N'CONTENT_TAGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (188, N'C_TAGS_EDIT', N'Tag bearbeiten', N'CONTENT_TAGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (189, N'C_TAGS_EDIT', N'Edit Tag', N'CONTENT_TAGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (190, N'LANG_CT_TAGS_DELETE_MSG', N'M&ouml;chten Sie das Tag wirklich l�schen?', N'CONTENT_TAGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (191, N'LANG_CT_TAGS_DELETE_MSG', N'Are you sure you want to delete this tag?', N'CONTENT_TAGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (192, N'LANG_SHARED_YES', N'Ja', N'ROOT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (193, N'LANG_SHARED_YES', N'Yes', N'ROOT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (194, N'LANG_SHARED_SUCCESS', N'Erfolg:', N'Root', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (195, N'LANG_SHARED_SUCCESS', N'Success:', N'Root', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (196, N'LANG_CONTENT_TAGS_ERROR_DELETE_SUCC', N'Das Tag wurde erfolgreich gel&ouml;scht.', N'CONTENT_TAGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (197, N'LANG_CONTENT_TAGS_ERROR_DELETE_SUCC', N'The Tag was deleted successfully.', N'CONTENT_TAGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (198, N'LANG_SHARED_ERROR', N'Fehler:', N'Root', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (199, N'LANG_SHARED_ERROR', N'Error:', N'Root', N'en-US')

GO

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (200, N'LANG_CONTENT_TAGS_ERROR_DELETE_FAILED', N'Das Tag konnte nicht gel&ouml;scht werden. Grund hierf�r k&ouml;nnen fehlende Rechte sein.', N'CONTENT_TAGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (201, N'LANG_CONTENT_TAGS_ERROR_DELETE_FAILED', N'Unable to delete the Tag. Reason for that can be a missing user right.', N'CONTENT_TAGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (202, N'LANG_SHARED_NO', N'Nein', N'ROOT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (203, N'LANG_SHARED_NO', N'No', N'ROOT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (204, N'LANG_SHARED_SAVE', N'Speichern', N'Root', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (205, N'LANG_SHARED_SAVE', N'Save', N'Root', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (206, N'LANG_SHARED_ABORT', N'Abbrechen', N'Root', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (207, N'LANG_SHARED_ABORT', N'Abort', N'Root', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (208, N'LANG_CT_GRID_CONTENTTYPE', N'Inhaltstyp', N'CONTENT_TAGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (209, N'LANG_CT_GRID_CONTENTTYPE', N'Content type', N'CONTENT_TAGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (210, N'LANG_CT_GRID_TAGNAME', N'Tag Name', N'CONTENT_TAGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (211, N'LANG_CT_GRID_TAGNAME', N'Tag name', N'CONTENT_TAGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (212, N'LANG_CT_GRID_BROWSING', N'Durchsuchen aktiviert', N'CONTENT_TAGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (213, N'LANG_CT_GRID_BROWSING', N'Browsing activated', N'CONTENT_TAGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (214, N'LANG_SHARED_CLICK_HERE_FOR_INFO', N'Klicken Sie hier um die Hilfe anzuzeigen', N'Root', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (215, N'LANG_SHARED_CLICK_HERE_FOR_INFO', N'Please click here to get help with this', N'Root', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (216, N'LANG_CT_TAGS_HELPTEXT', N'     <b>Tags</b> sind Stichworte/Besonderheiten von Inhalten, welche oft f�r die Suchmaschinen hervorgehoben werden oder Benutzern die M�glichkeit bietet weitere getaggte Inhalte zu lesen. Tags sind �hnlich wie Kategorien und k�nnen je nach Bedarf auch eine eigene Unterseite in diversen Modulen bekommen (Sofern das Modul diese Funktion unterst�tzt).Beliebt als Tags sind Marken, Hersteller oder Platformen (z.B. Windows, Linux, X-Box...)        <br />', N'CONTENT_TAGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (217, N'LANG_CT_TAGS_HELPTEXT', N'<b>Tags</b> are special keywords for contents that can be interesting for visitors. Tagged contents cann me browsed like an category and will show you visitors more contents with this tag. Mostly tags will be used to highlight trademarks or manufacturers for example Windows, Linux, X-Box...', N'CONTENT_TAGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (218, N'LANG_SHARED_HIDE_HELP', N'Hilfe ausblenden', N'Root', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (219, N'LANG_SHARED_HIDE_HELP', N'Hide help', N'Root', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (220, N'LANG_SHARED_ADD', N'Hinzuf&uuml;gen', N'Root', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (221, N'LANG_SHARED_ADD', N'Add', N'Root', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (222, N'LANG_SHARED_EDIT', N'Bearbeiten', N'Root', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (223, N'LANG_SHARED_EDIT', N'Edit', N'Root', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (224, N'LANG_SHARED_DELETE', N'L&ouml;schen', N'Root', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (225, N'LANG_SHARED_DELETE', N'DELETE', N'Root', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (226, N'LANG_CT_GRID_TITLE', N'Auflistung aller Tags', N'CONTENT_TAGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (227, N'LANG_CT_GRID_TITLE', N'List of all tags', N'CONTENT_TAGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (228, N'LANG_DESKTOPICON_USERMANAGEMENT', N'Benutzerverwaltung', N'DESKTOP_ICONS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (229, N'LANG_DESKTOPICON_USERMANAGEMENT', N'Usermanagement', N'DESKTOP_ICONS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (230, N'LANG_BACKEND_WIDGETS_USER_MANAGEMENT_TITLE', N'Benutzerverwaltung', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (231, N'LANG_BACKEND_WIDGETS_USER_MANAGEMENT_TITLE', N'Usermanagement', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (232, N'LANG_M_USERS_PKID', N'Eind. ID', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (233, N'LANG_M_USERS_PKID', N'Unique ID', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (234, N'LANG_M_USERS_UNAME', N'Benutzername', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (235, N'LANG_M_USERS_UNAME', N'Username', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (236, N'LANG_M_USERS_PGROUP', N'Benutzergruppe', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (237, N'LANG_M_USERS_PGROUP', N'User group', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (238, N'LANG_M_USERS_LOCKED', N'Gesperrt?', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (239, N'LANG_M_USERS_LOCKED', N'Locked out?', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (240, N'LANG_M_USERS_GRID_TITLE', N'Benutzerverwaltung', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (241, N'LANG_M_USERS_GRID_TITLE', N'User Management', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (242, N'LANG_M_USERS_GRID_BTN_ADD', N'Benutzer erstellen', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (243, N'LANG_M_USERS_GRID_BTN_ADD', N'Create User', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (244, N'LANG_FM_DIALOG_CREATE_TITLE', N'Benutzer anlegen', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (245, N'LANG_FM_DIALOG_CREATE_TITLE', N'Create a new User', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (246, N'LANG_FM_DIALOG_CREATE', N'Erstellen', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (247, N'LANG_FM_DIALOG_CREATE', N'Create', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (248, N'LANG_FM_DIALOG_GENERATE_PW', N'Passwort generieren', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (249, N'LANG_FM_DIALOG_GENERATE_PW', N'Generate password', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (250, N'LANG_FM_DIALOG_PASSWORD', N'Passwort', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (251, N'LANG_FM_DIALOG_PASSWORD', N'Password', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (252, N'LANG_FM_DIALOG_PASSWORD_CONFIRM', N'Passwort best&auml;tigen', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (253, N'LANG_FM_DIALOG_PASSWORD_CONFIRM', N'Confirm password', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (254, N'LANG_FM_DIALOG_SECRET_QUESTION', N'Geheime Frage', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (255, N'LANG_FM_DIALOG_SECRET_QUESTION', N'Secret question', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (256, N'LANG_FM_DIALOG_STATUS', N'Benutzerstatus', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (257, N'LANG_FM_DIALOG_STATUS', N'User status', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (258, N'LANG_FM_DIALOG_STATUS_ACTIVATED', N'Aktiviert', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (259, N'LANG_FM_DIALOG_STATUS_ACTIVATED', N'Activated', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (260, N'LANG_FM_DIALOG_STATUS_DEACTIVATED', N'Deaktiviert', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (261, N'LANG_FM_DIALOG_STATUS_DEACTIVATED', N'Deactivated', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (262, N'LANG_FM_DIALOG_LOCK', N'Benutzer sperren', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (263, N'LANG_FM_DIALOG_LOCK', N'Lock user', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (264, N'LANG_FM_DIALOG_EMAIL', N'E-Mail Adresse', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (265, N'LANG_FM_DIALOG_EMAIL', N'eMail address', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (266, N'LANG_FM_DIALOG_INFORM_USER', N'Benutzer benachrichtigen', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (267, N'LANG_FM_DIALOG_INFORM_USER', N'Inform user', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (268, N'LANG_FM_DIALOG_INFORM_USER_YES', N'Benutzer benachrichtigen', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (269, N'LANG_FM_DIALOG_INFORM_USER_YES', N'Inform user', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (270, N'LANG_FM_DIALOG_INFORM_USER_NO', N'Benutzer nicht benachrichtigen', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (271, N'LANG_FM_DIALOG_INFORM_USER_NO', N'Dont inform user', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (272, N'LANG_MAILER_SMTPSERVER', N'SMTP Server', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (273, N'LANG_MAILER_SMTPSERVER', N'SMTP server', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (274, N'LANG_MAILER_SMTP_PORT', N'SMTP Port', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (275, N'LANG_MAILER_SMTP_PORT', N'SMTP port', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (276, N'LANG_CAN_MANAGE_MAIL_LAYOUTS_DESCRIPTION', N'Benutzer darf Mail Layouts verwalten.', N'PERMISSION_KEYS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (277, N'LANG_CAN_MANAGE_MAIL_LAYOUTS_DESCRIPTION', N'User can manage mail layouts', N'PERMISSION_KEYS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (278, N'LANG_CAN_MANAGE_SMTP_DESCRIPTION', N'Benutzer darf Mail Layouts verwalten.', N'PERMISSION_KEYS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (279, N'LANG_CAN_MANAGE_SMTP_DESCRIPTION', N'User can manage mail layouts', N'PERMISSION_KEYS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (280, N'LANG_CT_TAGS_NOT_EXISTING', N'Das angeforderte Tag existiert nicht. M&ouml;glicher Weise wurde es bereits gel&ouml;scht!', N'CONTENT_TAGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (281, N'LANG_CT_TAGS_NOT_EXISTING', N'The requested tag does not exists. Maybe it was deleted allready!', N'CONTENT_TAGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (282, N'LANG_MAILER_SMTPLOGIN', N'Benutzername', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (283, N'LANG_MAILER_SMTPLOGIN', N'Login', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (284, N'LANG_MAILER_SMTP_PASSWORD', N'Passwort', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (285, N'LANG_MAILER_SMTP_PASSWORD', N'Password', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (286, N'LANG_MAILER_SENDER_EMAILADDRESS', N'Absender Emailadresse', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (287, N'LANG_MAILER_SENDER_EMAILADDRESS', N'Sender eMail Address', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (288, N'LANG_MAILER_SENDERNAME', N'Abesendername', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (289, N'LANG_MAILER_SENDERNAME', N'Sender name', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (290, N'LANG_MAILER_SMTP_REQUIRES_AUTH', N'Ausgangsserver erfordert Authorisierung', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (291, N'LANG_MAILER_SMTP_REQUIRES_AUTH', N'SMPT Server requires Credentials', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (292, N'LANG_MAILER_SMTP_REQUIRES_HTTPS', N'Ausgangsserver erfordert HTTPS', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (293, N'LANG_MAILER_SMTP_REQUIRES_HTTPS', N'SMPT Server requires HTTPS', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (294, N'LANG_DESKTOPICONS_MAILER_SETTINGS', N'E-Mail Einstellungen', N'DESKTOP_ICONS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (295, N'LANG_DESKTOPICONS_MAILER_SETTINGS', N'eMail Settings', N'DESKTOP_ICONS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (296, N'LANG_BACKEND_WIDGETS_SETTINGS_MAILER_TITLE', N'E-Mail Einstellungen', N'BACKEND_WIDGETS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (297, N'LANG_BACKEND_WIDGETS_SETTINGS_MAILER_TITLE', N'eMail Settings', N'BACKEND_WIDGETS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (298, N'LANG_NFTHEME_NEWS_CBOX_HEADER', N'News', N'NFTHEME', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (299, N'LANG_NFTHEME_NEWS_CBOX_HEADER', N'News', N'NFTHEME', N'en-US')

GO

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (300, N'LANG_NFTHEME_ARTICLE_CBOX_HEADER', N'Spieletests', N'NFTHEME', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (301, N'LANG_NFTHEME_ARTICLE_CBOX_HEADER', N'Game tests', N'NFTHEME', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (302, N'LANG_NFTHEME_FORUM_CBOX_HEADER', N'Neues aus dem Forum', N'NFTHEME', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (303, N'LANG_NFTHEME_FORUM_CBOX_HEADER', N'Latest forum posts', N'NFTHEME', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (304, N'LANG_FM_DIALOG_COMMENT', N'Internes Kommentar', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (305, N'LANG_FM_DIALOG_COMMENT', N'Internal comment', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (306, N'LANG_FM_DIALOG_COMMENT_NO_COMMENT', N'Kein Kommentar', N'USERS_BACKEND', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (307, N'LANG_FM_DIALOG_COMMENT_NO_COMMENT', N'No comment', N'USERS_BACKEND', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (308, N'LANG_GLB_CONVERT_MOVIE', N'Aktivieren, um die automatische Umwandlung in *.mp4 von Video An�ngen zu erm�glichen. Ben�tigt Ausf�hrrechte der FFMEG.exe', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (309, N'LANG_GLB_CONVERT_MOVIE', N'Enable to activate the automatic convert to *.mp4 of video attachments. Requires exec. rights for FFMPEG.exe', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (310, N'LANG_CAN_CONFIGURE_CONTENTTYPES_DESCRIPTION', N'Benutzer kann Inhaltstypen konfigurieren', N'PERMISSION_KEYS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (311, N'LANG_CAN_CONFIGURE_CONTENTTYPES_DESCRIPTION', N'User cann configure content types', N'PERMISSION_KEYS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (312, N'LANG_PERMISSIONS_TEST_KEY_DESCRIPTION', N'Test Deutsch', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (313, N'LANG_PERMISSIONS_TEST_KEY_DESCRIPTION', N'TEST ENG!', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (314, N'LANG_PERMISSIONS_USR_CAN_DELETE_CONTENT_TAGS_DESCRIPTION', N'Benutzer darf Tags f�r Inhalte l�schen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (315, N'LANG_PERMISSIONS_USR_CAN_DELETE_CONTENT_TAGS_DESCRIPTION', N'User can delete content tags', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (316, N'LANG_PERMISSIONS_USR_CAN_EDIT_CONTENT_TAGS_DESCRIPTION', N'Benutzer darf Tags f�r Inhalte bearbeiten', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (317, N'LANG_PERMISSIONS_USR_CAN_EDIT_CONTENT_TAGS_DESCRIPTION', N'User can edit content tags', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (318, N'LANG_PERMISSIONS_USR_CAN_ADD_CONTENT_TAGS_DESCRIPTION', N'Benutzer darf Tags f�r Inhalte hinzuf�gen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (319, N'LANG_PERMISSIONS_USR_CAN_ADD_CONTENT_TAGS_DESCRIPTION', N'User can add content tags', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (320, N'LANG_PERMISSIONS_USR_CAN_EDIT_CONTENT_CATEGORY_DESCRIPTION', N'Benutzer darf Kategorien f�r Inhalte bearbeiten', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (321, N'LANG_PERMISSIONS_USR_CAN_EDIT_CONTENT_CATEGORY_DESCRIPTION', N'User can edit content categories', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (322, N'LANG_PERMISSIONS_USR_CAN_UPLOAD_CONTENT_ATTACHMENTS_DESCRIPTION', N'Benutzer darf Dateien f�r Inhalte hochladen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (323, N'LANG_PERMISSIONS_USR_CAN_UPLOAD_CONTENT_ATTACHMENTS_DESCRIPTION', N'User can upload file attachments for contents', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (324, N'LANG_PERMISSIONS_USR_CAN_DELETE_CONTENT_ATTACHMENTS_DESCRIPTION', N'Benutzer darf Dateien f�r Inhalte l�schen (Wirkungslos wenn Benutzer Inhalte l�schen darf!)', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (325, N'LANG_PERMISSIONS_USR_CAN_DELETE_CONTENT_ATTACHMENTS_DESCRIPTION', N'User can delete file attachments for contents (Does not affect users who are allowed to delete contents)', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (326, N'LANG_PERMISSIONS_USR_CAN_EDIT_CONTENT_ATTACHMENTS_DESCRIPTION', N'Benutzer darf hochgeladene Dateien f�r Inhalte anpassen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (327, N'LANG_PERMISSIONS_USR_CAN_EDIT_CONTENT_ATTACHMENTS_DESCRIPTION', N'User can modify uploaded content files', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (328, N'LANG_PERMISSIONS_USR_CAN_DELETE_CONTENTS_DESCRIPTION', N'Benutzer darf Inhalte l�schen (Erlaubt den Benutzer ebenfalls Dateien f�r Inhalte zu l�schen!)', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (329, N'LANG_PERMISSIONS_USR_CAN_DELETE_CONTENTS_DESCRIPTION', N'User can delete contents (Users gets also permission for deleting content files!)', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (330, N'LANG_PERMISSIONS_USR_CAN_DELETE_CONTENT_CATEGORIES_DESCRIPTION', N'Benutzer darf Kategorien f�r Inhalte l�schen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (331, N'LANG_PERMISSIONS_USR_CAN_DELETE_CONTENT_CATEGORIES_DESCRIPTION', N'User can delete content categories.', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (332, N'LANG_PERMISSIONS_USR_CAN_ADD_CONTENT_CATEGORY_DESCRIPTION', N'Benutzer darf Kategorien f�r Inhalte hinzuf�gen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (333, N'LANG_PERMISSIONS_USR_CAN_ADD_CONTENT_CATEGORY_DESCRIPTION', N'User can add content categories.', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (334, N'USR_CAN_DELETE_CONTENT_TAGS', N'Benutzer darf Tags f�r Inhalte l�schen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (335, N'USR_CAN_DELETE_CONTENT_TAGS', N'User can delete content tags', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (336, N'USR_CAN_EDIT_CONTENT_TAGS', N'Benutzer darf Tags f�r Inhalte bearbeiten', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (337, N'USR_CAN_EDIT_CONTENT_TAGS', N'User can edit content tags', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (338, N'USR_CAN_ADD_CONTENT_TAGS', N'Benutzer darf Tags f�r Inhalte hinzuf�gen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (339, N'USR_CAN_ADD_CONTENT_TAGS', N'User can add content tags', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (340, N'USR_CAN_EDIT_CONTENT_CATEGORY', N'Benutzer darf Kategorien f�r Inhalte bearbeiten', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (341, N'USR_CAN_EDIT_CONTENT_CATEGORY', N'User can edit content categories', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (342, N'USR_CAN_UPLOAD_CONTENT_ATTACHMENTS', N'Benutzer darf Dateien f�r Inhalte hochladen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (343, N'USR_CAN_UPLOAD_CONTENT_ATTACHMENTS', N'User can upload file attachments for contents', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (344, N'USR_CAN_DELETE_CONTENT_ATTACHMENTS', N'Benutzer darf Dateien f�r Inhalte l�schen (Wirkungslos wenn Benutzer Inhalte l�schen darf!)', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (345, N'USR_CAN_DELETE_CONTENT_ATTACHMENTS', N'User can delete file attachments for contents (Does not affect users who are allowed to delete contents)', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (346, N'USR_CAN_EDIT_CONTENT_ATTACHMENTS', N'Benutzer darf hochgeladene Dateien f�r Inhalte anpassen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (347, N'USR_CAN_EDIT_CONTENT_ATTACHMENTS', N'User can modify uploaded content files', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (348, N'USR_CAN_DELETE_CONTENTS', N'Benutzer darf Inhalte l�schen (Erlaubt den Benutzer ebenfalls Dateien f�r Inhalte zu l�schen!)', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (349, N'USR_CAN_DELETE_CONTENTS', N'User can delete contents (Users gets also permission for deleting content files!)', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (350, N'USR_CAN_DELETE_CONTENT_CATEGORIES', N'Benutzer darf Kategorien f�r Inhalte l�schen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (351, N'USR_CAN_DELETE_CONTENT_CATEGORIES', N'User can delete content categories.', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (352, N'USR_CAN_ADD_CONTENT_CATEGORY', N'Benutzer darf Kategorien f�r Inhalte hinzuf�gen', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (353, N'USR_CAN_ADD_CONTENT_CATEGORY', N'User can add content categories.', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (354, N'USR_CAN_EDIT_CONTENT', N'Benutzer darf Inhalte bearbeiten.', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (355, N'USR_CAN_EDIT_CONTENT', N'User can delete contents.', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (356, N'USR_CAN_CREATE_CONTENT', N'Benutzer darf Inhalte erstellen.', N'Permissions', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (357, N'USR_CAN_CREATE_CONTENT', N'User can create contents.', N'Permissions', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (358, N'LANG_NFTHEME_MOST_DISCUSSED_CBOX_HEADER', N'Hei� diskutiert', N'NFTHEME', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (359, N'LANG_NFTHEME_MOST_DISCUSSED_CBOX_HEADER', N'Most Discussed', N'NFTHEME', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (360, N'LANG_GLB_CONVERT_MOVIE_LABEL', N'Videos automatisch umwandeln', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (361, N'LANG_GLB_CONVERT_MOVIE_LABEL', N'Convert video files automated', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (362, N'LANG_GLB_CONVERT_MOVIE_FFMEG_PATH', N'Geben Sie hier den kompletten physikalischen Pfad zur FFMPEG.exe an.', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (363, N'LANG_GLB_CONVERT_MOVIE_FFMEG_PATH', N'Enter here the complete physical path to FFMPEG.exe', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (364, N'LANG_GLB_CONVERT_MOVIE_FFMEG_PATH_LABEL', N'Pfad zur FFMEG.exe', N'GLOBAL_SETTINGS', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (365, N'LANG_GLB_CONVERT_MOVIE_FFMEG_PATH_LABEL', N'Path to FFMEG.exe', N'GLOBAL_SETTINGS', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (366, N'LANG_CONTENT_DIALOG_DELETE_TEXT', N'M�chten Sie diesen Inhalt wirklich l�schen?', N'CONTENT_MANAGEMENT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (367, N'LANG_CONTENT_DIALOG_DELETE_TEXT', N'Are you sure, that you want to delete this content?', N'CONTENT_MANAGEMENT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (368, N'LANG_CONTENT_TITLE', N'Titel', N'CONTENT_MANAGEMENT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (369, N'LANG_CONTENT_TITLE', N'Title', N'CONTENT_MANAGEMENT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (370, N'LANG_CONTENT_CREATOR', N'Erstellt von', N'CONTENT_MANAGEMENT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (371, N'LANG_CONTENT_CREATOR', N'Created by', N'CONTENT_MANAGEMENT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (372, N'LANG_CONTENT_CATEGORY', N'Kategorie', N'CONTENT_MANAGEMENT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (373, N'LANG_CONTENT_CATEGORY', N'Category', N'CONTENT_MANAGEMENT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (374, N'LANG_CONTENT_CREATION_DATE', N'Erstelldatum', N'CONTENT_MANAGEMENT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (375, N'LANG_CONTENT_CREATION_DATE', N'Creation date', N'CONTENT_MANAGEMENT', N'en-US')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (376, N'LANG_CONTENTS', N'Inhalte', N'CONTENT_MANAGEMENT', N'de-DE')

INSERT [dbo].[nfcms_Language] ([Id], [Name], [Content], [Package], [Code]) VALUES (377, N'LANG_CONTENTS', N'Contents', N'CONTENT_MANAGEMENT', N'en-US')

SET IDENTITY_INSERT [dbo].[nfcms_Language] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Link_Identifiers] ON 



INSERT [dbo].[nfcms_Link_Identifiers] ([id], [identiferName], [theme]) VALUES (1, N'TOP', N'nftheme')

INSERT [dbo].[nfcms_Link_Identifiers] ([id], [identiferName], [theme]) VALUES (4, N'NAVI', N'nftheme')

SET IDENTITY_INSERT [dbo].[nfcms_Link_Identifiers] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Links] ON 



INSERT [dbo].[nfcms_Links] ([id], [LinkType], [LinkController], [LinkAction], [LinkHref], [LinkText], [LinkRelationship], [LinkIsActive], [SublinkController], [SublinkAction], [SublinkFrom], [NormalStateClass], [HoverStateClass]) VALUES (1212, N'MAIN', N'home', N'*', N'', N'LANG_LINKS_HOME', N'', N'1', N'', N'', 0, N'nav_home', N'nav_home_hover')

INSERT [dbo].[nfcms_Links] ([id], [LinkType], [LinkController], [LinkAction], [LinkHref], [LinkText], [LinkRelationship], [LinkIsActive], [SublinkController], [SublinkAction], [SublinkFrom], [NormalStateClass], [HoverStateClass]) VALUES (3434, N'MAIN', N'home', N'index', N'/Home/Index', N'LANG_LINKS_HOME', N'', N'1', N'', N'', 1212, N'nav_home', N'nav_home_hover')

INSERT [dbo].[nfcms_Links] ([id], [LinkType], [LinkController], [LinkAction], [LinkHref], [LinkText], [LinkRelationship], [LinkIsActive], [SublinkController], [SublinkAction], [SublinkFrom], [NormalStateClass], [HoverStateClass]) VALUES (4545, N'MAIN', N'home', N'contact', N'/Home/Contact', N'LANG_LINKS_CONTACT', N'', N'1', N'', N'', 1212, N'nav_home', N'nav_home_hover')

INSERT [dbo].[nfcms_Links] ([id], [LinkType], [LinkController], [LinkAction], [LinkHref], [LinkText], [LinkRelationship], [LinkIsActive], [SublinkController], [SublinkAction], [SublinkFrom], [NormalStateClass], [HoverStateClass]) VALUES (5656, N'MAIN', N'home', N'impressum', N'/Home/Impressum', N'LANG_LINKS_IMPRESSUM', N'', N'1', N'', N'', 1212, N'nav_home', N'nav_home_hover')

INSERT [dbo].[nfcms_Links] ([id], [LinkType], [LinkController], [LinkAction], [LinkHref], [LinkText], [LinkRelationship], [LinkIsActive], [SublinkController], [SublinkAction], [SublinkFrom], [NormalStateClass], [HoverStateClass]) VALUES (6767, N'MAIN', N'home', N'rss', N'/Home/RSS', N'LANG_LINKS_RSS', N'', N'1', N'', N'', 1212, N'nav_home', N'nav_home_hover')

INSERT [dbo].[nfcms_Links] ([id], [LinkType], [LinkController], [LinkAction], [LinkHref], [LinkText], [LinkRelationship], [LinkIsActive], [SublinkController], [SublinkAction], [SublinkFrom], [NormalStateClass], [HoverStateClass]) VALUES (7878, N'MAIN', N'news', N'*', N'', N'LANG_LINKS_NEWS', N'', N'1', N'', N'', 0, N'nav_news', N'nav_news_hover')

INSERT [dbo].[nfcms_Links] ([id], [LinkType], [LinkController], [LinkAction], [LinkHref], [LinkText], [LinkRelationship], [LinkIsActive], [SublinkController], [SublinkAction], [SublinkFrom], [NormalStateClass], [HoverStateClass]) VALUES (8989, N'MAIN', N'news', N'archive', N'/News/Archive', N'LANG_LINKS_NEWS_ARCHIVE', N'', N'1', N'', N'', 7878, N'nav_news', N'nav_news_hover')

SET IDENTITY_INSERT [dbo].[nfcms_Links] OFF

INSERT [dbo].[nfcms_Links2Identfiers] ([linkID], [identifierID]) VALUES (1212, 1)

INSERT [dbo].[nfcms_Links2Identfiers] ([linkID], [identifierID]) VALUES (7878, 1)

INSERT [dbo].[nfcms_Links2Identfiers] ([linkID], [identifierID]) VALUES (3434, 4)

INSERT [dbo].[nfcms_Links2Identfiers] ([linkID], [identifierID]) VALUES (4545, 4)

INSERT [dbo].[nfcms_Links2Identfiers] ([linkID], [identifierID]) VALUES (5656, 4)

INSERT [dbo].[nfcms_Links2Identfiers] ([linkID], [identifierID]) VALUES (6767, 4)

INSERT [dbo].[nfcms_Links2Identfiers] ([linkID], [identifierID]) VALUES (8989, 4)

SET IDENTITY_INSERT [dbo].[nfcms_PermissionGroups] ON 



INSERT [dbo].[nfcms_PermissionGroups] ([id], [groupName], [isGuestGroup], [isDefaultGroup]) VALUES (1, N'guestG', N'true', N'false')

INSERT [dbo].[nfcms_PermissionGroups] ([id], [groupName], [isGuestGroup], [isDefaultGroup]) VALUES (2, N'admins', N'false', N'false')

INSERT [dbo].[nfcms_PermissionGroups] ([id], [groupName], [isGuestGroup], [isDefaultGroup]) VALUES (3, N'registered_users', N'false', N'true')

SET IDENTITY_INSERT [dbo].[nfcms_PermissionGroups] OFF

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'write_comments', N'true', N'LANG_PERMISSIONS_WRITE_COMMENTS')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'CAN_VIEW_PAGE', N'true', N'')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_IS_ADMIN', N'true', N'')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'CAN_SEE_FORUM', N'true', N'')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'CAN_CREATE_THREAD', N'true', N'')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_VIEW_ACCOUNT_SETTINGS', N'true', N'')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_ENTER_BACKEND', N'FALSE', N'USR_CAN_ENTER_BACKEND')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'write_comments', N'true', N'LANG_PERMISSIONS_WRITE_COMMENTS')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'CAN_VIEW_PAGE', N'true', N'')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_IS_ADMIN', N'true', N'')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'CAN_SEE_FORUM', N'true', N'')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'CAN_CREATE_THREAD', N'true', N'')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_VIEW_ACCOUNT_SETTINGS', N'true', N'')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_ENTER_BACKEND', N'FALSE', N'USR_CAN_ENTER_BACKEND')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'CAN_MANAGE_MAIL_LAYOUTS', N'False', N'LANG_CAN_MANAGE_MAIL_LAYOUTS_DESCRIPTION')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'CAN_MANAGE_SMTP', N'False', N'LANG_CAN_MANAGE_SMTP_DESCRIPTION')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'CAN_CONFIGURE_CONTENTTYPES', N'True', N'LANG_CAN_CONFIGURE_CONTENTTYPES_DESCRIPTION')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'TEST_KEY', N'false', N'LANG_PERMISSIONS_TEST_KEY_DESCRIPTION')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_DELETE_CONTENT_TAGS', N'false', N'USR_CAN_DELETE_CONTENT_TAGS')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_EDIT_CONTENT_TAGS', N'false', N'USR_CAN_EDIT_CONTENT_TAGS')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_ADD_CONTENT_TAGS', N'false', N'USR_CAN_ADD_CONTENT_TAGS')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_EDIT_CONTENT_CATEGORY', N'false', N'USR_CAN_EDIT_CONTENT_CATEGORY')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_UPLOAD_CONTENT_ATTACHMENTS', N'false', N'USR_CAN_UPLOAD_CONTENT_ATTACHMENTS')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_DELETE_CONTENT_ATTACHMENTS', N'false', N'USR_CAN_DELETE_CONTENT_ATTACHMENTS')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_EDIT_CONTENT_ATTACHMENTS', N'false', N'USR_CAN_EDIT_CONTENT_ATTACHMENTS')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_DELETE_CONTENTS', N'false', N'USR_CAN_DELETE_CONTENTS')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_DELETE_CONTENT_CATEGORIES', N'false', N'USR_CAN_DELETE_CONTENT_CATEGORIES')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_ADD_CONTENT_CATEGORY', N'false', N'USR_CAN_ADD_CONTENT_CATEGORY')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_EDIT_CONTENT', N'false', N'USR_CAN_EDIT_CONTENT')

INSERT [dbo].[nfcms_Permissionkeys] ([pkey], [defaultVal], [langLine]) VALUES (N'USR_CAN_CREATE_CONTENT', N'false', N'USR_CAN_CREATE_CONTENT')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'guestG', N'true', N'write_comments')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'can_view_page')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_VIEW_ACCOUNT_SETTINGS')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'TRUE', N'USR_CAN_ENTER_BACKEND')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'guestG', N'true', N'write_comments')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'can_view_page')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_VIEW_ACCOUNT_SETTINGS')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'TRUE', N'USR_CAN_ENTER_BACKEND')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'registered_users', N'true', N'write_comments')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'registered_users', N'true', N'can_view_page')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'registered_users', N'true', N'USR_CAN_VIEW_ACCOUNT_SETTINGS')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'registered_users', N'false', N'USR_CAN_ENTER_BACKEND')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'TEST_KEY')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_DELETE_CONTENT_TAGS')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_EDIT_CONTENT_TAGS')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_ADD_CONTENT_TAGS')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_EDIT_CONTENT_CATEGORY')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_UPLOAD_CONTENT_ATTACHMENTS')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_DELETE_CONTENT_ATTACHMENTS')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_EDIT_CONTENT_ATTACHMENTS')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_DELETE_CONTENTS')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_DELETE_CONTENT_CATEGORIES')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_ADD_CONTENT_CATEGORY')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_EDIT_CONTENT')

INSERT [dbo].[nfcms_Permissions2Groups] ([groupID], [val], [pk]) VALUES (N'admins', N'true', N'USR_CAN_CREATE_CONTENT')

INSERT [dbo].[nfcms_Profile_User_Values] ([VarName], [VarValue], [PKID]) VALUES (N'ProfileImage', N'/UserAvatar/Malte-Picture.jpg', N'8cbbfe36-c96e-46fc-a8af-b6757181e799')

INSERT [dbo].[nfcms_Profile_User_Values] ([VarName], [VarValue], [PKID]) VALUES (N'ProfileImage', N'/UserAvatar/Malte-Picture.jpg', N'8cbbfe36-c96e-46fc-a8af-b6757181e799')

INSERT [dbo].[nfcms_Profile_Vars] ([Name], [LangLine], [Section], [ViewName], [Active], [ShowInProfile]) VALUES (N'ProfileImage', N'LANG_PROFILE_IMAGE', N'ProfileImage', N'', N'true', N'true')

INSERT [dbo].[nfcms_Profile_Vars] ([Name], [LangLine], [Section], [ViewName], [Active], [ShowInProfile]) VALUES (N'ProfileImage', N'LANG_PROFILE_IMAGE', N'ProfileImage', N'', N'true', N'true')

SET IDENTITY_INSERT [dbo].[nfcms_RegisteredMIMETypes] ON 



INSERT [dbo].[nfcms_RegisteredMIMETypes] ([id], [fileExstension], [MIMEType]) VALUES (1, N'.jpg', N'image/jpeg')

INSERT [dbo].[nfcms_RegisteredMIMETypes] ([id], [fileExstension], [MIMEType]) VALUES (2, N'.bmp', N'image/bmp')

INSERT [dbo].[nfcms_RegisteredMIMETypes] ([id], [fileExstension], [MIMEType]) VALUES (3, N'.png', N'image/png')

INSERT [dbo].[nfcms_RegisteredMIMETypes] ([id], [fileExstension], [MIMEType]) VALUES (4, N'.avi', N'video/x-msvideo')

INSERT [dbo].[nfcms_RegisteredMIMETypes] ([id], [fileExstension], [MIMEType]) VALUES (5, N'.wmv', N'video/x-ms-wmv')

INSERT [dbo].[nfcms_RegisteredMIMETypes] ([id], [fileExstension], [MIMEType]) VALUES (6, N'.m4v', N'video/mp4')

SET IDENTITY_INSERT [dbo].[nfcms_RegisteredMIMETypes] OFF

SET IDENTITY_INSERT [dbo].[nfcms_SettingCategories] ON 



INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (1, N'Contents', N'GLOBAL_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (1, N'adsdsaasasd', N'USER_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (2, N'TestCat', N'USER_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (3, N'Testsettings', N'USER_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (16, N'SMTP', N'GLOBAL_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (59, N'MAIL_LAYOUT', N'GLOBAL_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (4, N'adsadsasd', N'USER_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (5, N'sdasdaasd', N'USER_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (7, N'Test2', N'USER_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (8, N'Contents', N'GLOBAL_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (9, N'ADDTHIS', N'GLOBAL_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (10, N'NEWS', N'GLOBAL_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (11, N'MAIN', N'GLOBAL_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (14, N'GENERICUSERSETTINGS', N'USER_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (15, N'User', N'GLOBAL_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (6, N'Notifications', N'USER_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (12, N'FACEBOOK', N'GLOBAL_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (13, N'ARTICLE', N'GLOBAL_SETTINGS')

INSERT [dbo].[nfcms_SettingCategories] ([id], [Name], [CatRel]) VALUES (60, N'CONTENT_SETTINGS', N'GLOBAL_SETTINGS')

SET IDENTITY_INSERT [dbo].[nfcms_SettingCategories] OFF

SET IDENTITY_INSERT [dbo].[nfcms_SettingModels] ON 



INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (61, N'MAILER_DEFAULT_LAYOUT', N'LANG_MAILER_DEFAULT_LAYOUT', N'LANG_GLB_SETTING_ID0_MAILER_DEFAULT_LAYOUT_DESCR_GENERATED', N'', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 59)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (78, N'MAILER_SMTPSERVER', N'LANG_MAILER_SMTPSERVER', N'LANG_GLB_SETTING_ID0_MAILER_SMTPSERVER_DESCR_GENERATED', N'mail.example.com', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 16)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (79, N'MAILER_SMTP_PORT', N'LANG_MAILER_SMTP_PORT', N'LANG_GLB_SETTING_ID0_MAILER_SMTP_PORT_DESCR_GENERATED', N'95', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 16)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (80, N'MAILER_SMTPLOGIN', N'LANG_MAILER_SMTPLOGIN', N'LANG_GLB_SETTING_ID0_MAILER_SMTPLOGIN_DESCR_GENERATED', N'', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 16)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (81, N'MAILER_SMTP_PASSWORD', N'LANG_MAILER_SMTP_PASSWORD', N'LANG_GLB_SETTING_ID0_MAILER_SMTP_PASSWORD_DESCR_GENERATED', N'', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 16)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (82, N'MAILER_SENDER_EMAILADDRESS', N'LANG_MAILER_SENDER_EMAILADDRESS', N'LANG_GLB_SETTING_ID0_MAILER_SENDER_EMAILADDRESS_DESCR_GENERATED', N'', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 16)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (83, N'MAILER_SENDERNAME', N'LANG_MAILER_SENDERNAME', N'LANG_GLB_SETTING_ID0_MAILER_SENDERNAME_DESCR_GENERATED', N'', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 16)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (84, N'MAILER_SMTP_REQUIRES_AUTH', N'LANG_MAILER_SMTP_REQUIRES_AUTH', N'LANG_GLB_SETTING_ID0_MAILER_SMTP_REQUIRES_AUTH_DESCR_GENERATED', N'', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 16)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (85, N'MAILER_SMTP_REQUIRES_HTTPS', N'LANG_MAILER_SMTP_REQUIRES_HTTPS', N'LANG_GLB_SETTING_ID0_MAILER_SMTP_REQUIRES_HTTPS_DESCR_GENERATED', N'', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 16)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (2, N'dasasddas', N'LANG_USR_SETTING_DASASDDAS_LABEL', N'LANG_USR_SETTING_DASASDDAS_DESCR', N'asdddasdas', N'USER_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 1)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (3, N'test1', N'LANG_USR_SETTING_TEST1_LABEL', N'LANG_USR_SETTING_TEST1_DESCR', N'233', N'USER_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 2)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (4, N'xtest', N'LANG_USR_SETTING_XTEST_LABEL', N'LANG_USR_SETTING_XTEST_DESCR', N'true', N'USER_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 3)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (7, N'TestSetting1', N'LANG_USR_SETTING_ID0_TESTSETTING1_LABEL_GENERATED', N'LANG_USR_SETTING_ID0_TESTSETTING1_DESCR_GENERATED', N'1', N'USER_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 3)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (11, N'Test3', N'LANG_USR_SETTING_ID0_TEST3_LABEL_GENERATED', N'LANG_USR_SETTING_ID0_TEST3_DESCR_GENERATED', N'1', N'USER_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 3)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (12, N'Test4', N'LANG_USR_SETTING_ID0_TEST4_LABEL_GENERATED', N'LANG_USR_SETTING_ID0_TEST4_DESCR_GENERATED', N'4', N'USER_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 7)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (13, N'GLOBAL_INDEX_MAX_ENTRIES', N'LANG_GLB_SETTING_ID0_GLOBAL_INDEX_MAX_ENTRIES_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_INDEX_MAX_ENTRIES_DESCR_GENERATED', N'10', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 8)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (20, N'GLOBAL_ADDTHIS_ENABLED', N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_ENABLED_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_ENABLED_DESCR_GENERATED', N'true', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 9)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (24, N'GLOBAL_NEWS_MAX_GALLERY_VIDEOS', N'LANG_GLB_SETTING_ID0_GLOBAL_NEWS_MAX_GALLERY_VIDEOS_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_NEWS_MAX_GALLERY_VIDEOS_DESCR_GENERATED', N'10', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 10)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (26, N'GLOBAL_SOCIAL_FB_ENABLED', N'LANG_GLB_SETTING_ID0_GLOBAL_SOCIAL_FB_ENABLED_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_SOCIAL_FB_ENABLED_DESCR_GENERATED', N'false', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 12)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (27, N'GLOBAL_DEFAULT_LANGUAGE', N'LANG_GLB_SETTING_ID0_GLOBAL_DEFAULT_LANGUAGE_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_DEFAULT_LANGUAGE_DESCR_GENERATED', N'de-DE', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 11)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (28, N'GLOBAL_SETTING_DEFAULT_THEME', N'LANG_GLB_SETTING_ID0_GLOBAL_SETTING_DEFAULT_THEME_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_SETTING_DEFAULT_THEME_DESCR_GENERATED', N'nftheme', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 11)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (31, N'GLOBAL_Article_MAX_GALLERY_VIDEOS', N'LANG_GLB_SETTING_ID0_GLOBAL_ARTICLE_MAX_GALLERY_VIDEOS_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_ARTICLE_MAX_GALLERY_VIDEOS_DESCR_GENERATED', N'10', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 13)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (32, N'GLOBAL_ADDTHIS_USERNAME', N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_USERNAME_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_ADDTHIS_USERNAME_DESCR_GENERATED', N'networkfreaksde', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 9)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (33, N'VIEW_EMAIL', N'LANG_USR_SETTING_ID0_VIEW_EMAIL_LABEL_GENERATED', N'LANG_USR_SETTING_ID0_VIEW_EMAIL_DESCR_GENERATED', N'false', N'USER_SETTINGS', N'Core.SettingType.Array', N'Core.ValueType.String', 14)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (34, N'RECIEVE_PMS', N'LANG_USR_SETTING_ID0_RECIEVE_PMS_LABEL_GENERATED', N'LANG_USR_SETTING_ID0_RECIEVE_PMS_DESCR_GENERATED', N'true', N'USER_SETTINGS', N'Core.SettingType.Array', N'Core.ValueType.String', 14)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (35, N'PROFILE_PUBLIC', N'LANG_USR_SETTING_ID0_PROFILE_PUBLIC_LABEL_GENERATED', N'LANG_USR_SETTING_ID0_PROFILE_PUBLIC_DESCR_GENERATED', N'all', N'USER_SETTINGS', N'Core.SettingType.Array', N'Core.ValueType.String', 14)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (36, N'ACCOUNT_PIX_FILETYPES', N'LANG_GLB_SETTING_ID0_ACCOUNT_PIX_FILETYPES_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_ACCOUNT_PIX_FILETYPES_DESCR_GENERATED', N'a:{ ".jpeg" ".jpg" ".png" ".gif:" ".bmp" }', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.Array', 15)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (37, N'ACCOUNT_MAX_PROFILEPIC_FILE_SIZE', N'LANG_GLB_SETTING_ID0_ACCOUNT_MAX_PROFILEPIC_FILE_SIZE_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_ACCOUNT_MAX_PROFILEPIC_FILE_SIZE_DESCR_GENERATED', N'5242880', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 15)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (39, N'ACCOUNT_PIC_MIN_HEIGHT', N'LANG_GLB_SETTING_ID0_ACCOUNT_PIC_MIN_HEIGHT_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_ACCOUNT_PIC_MIN_HEIGHT_DESCR_GENERATED', N'100', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 15)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (40, N'ACCOUNT_PIC_MAX_WIDTH', N'LANG_GLB_SETTING_ID0_ACCOUNT_PIC_MAX_WIDTH_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_ACCOUNT_PIC_MAX_WIDTH_DESCR_GENERATED', N'600', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 15)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (41, N'ACCOUNT_PIC_MAX_HEIGHT', N'LANG_GLB_SETTING_ID0_ACCOUNT_PIC_MAX_HEIGHT_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_ACCOUNT_PIC_MAX_HEIGHT_DESCR_GENERATED', N'600', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 15)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (23, N'GLOBAL_NEWS_MAX_GALLERY_IMAGES', N'LANG_GLB_SETTING_ID0_GLOBAL_NEWS_MAX_GALLERY_IMAGES_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_NEWS_MAX_GALLERY_IMAGES_DESCR_GENERATED', N'10', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 10)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (38, N'ACCOUNT_PIC_MIN_WIDTH', N'LANG_GLB_SETTING_ID0_ACCOUNT_PIC_MIN_WIDTH_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_ACCOUNT_PIC_MIN_WIDTH_DESCR_GENERATED', N'100', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 15)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (21, N'GLOBAL_NEWS_MAX_ENTRIES', N'LANG_GLB_SETTING_ID0_GLOBAL_NEWS_MAX_ENTRIES_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_NEWS_MAX_ENTRIES_DESCR_GENERATED', N'10', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 10)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (22, N'GLOBAL_GUESTPKID', N'LANG_GLB_SETTING_ID0_GLOBAL_GUESTPKID_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_GUESTPKID_DESCR_GENERATED', N'BDE3F3BE-6B25-44B7-816A-CF3C134266BE', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 11)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (25, N'GLOBAL_SOCIAL_FB_APPID', N'LANG_GLB_SETTING_ID0_GLOBAL_SOCIAL_FB_APPID_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_SOCIAL_FB_APPID_DESCR_GENERATED', N' ', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 12)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (29, N'GLOBALSETTING.GLOBABL_MAINSITENAME', N'LANG_GLB_SETTING_ID0_GLOBALSETTING.GLOBABL_MAINSITENAME_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBALSETTING.GLOBABL_MAINSITENAME_DESCR_GENERATED', N'NetworkFreaks.de', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 11)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (30, N'GLOBAL_Article_MAX_GALLERY_IMAGES', N'LANG_GLB_SETTING_ID0_GLOBAL_ARTICLE_MAX_GALLERY_IMAGES_LABEL_GENERATED', N'LANG_GLB_SETTING_ID0_GLOBAL_ARTICLE_MAX_GALLERY_IMAGES_DESCR_GENERATED', N'10', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 13)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (106, N'GLB_CONVERT_MOVIE', N'LANG_GLB_CONVERT_MOVIE_LABEL', N'LANG_GLB_CONVERT_MOVIE', N'True', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 60)

INSERT [dbo].[nfcms_SettingModels] ([id], [SettingName], [SettingLangLineLabel], [SettingLangLineDescr], [SettingDefaultValue], [SettingRelation], [SettingType], [ValueType], [CID]) VALUES (107, N'GLB_CONVERT_MOVIE_FFMEG_PATH', N'LANG_GLB_CONVERT_MOVIE_FFMEG_PATH_LABEL', N'LANG_GLB_CONVERT_MOVIE_FFMEG_PATH', N'C:\Users\Malte\Documents\Visual Studio 2012\Projects\Ren.CMS.Net\nCMS_NET\Binaries\Converter\FFMPEG.exe', N'GLOBAL_SETTINGS', N'Core.SettingType.String', N'Core.ValueType.String', 60)

SET IDENTITY_INSERT [dbo].[nfcms_SettingModels] OFF

SET IDENTITY_INSERT [dbo].[nfcms_Settings2Permissions] ON 



INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (1, N'write_comments', N'write_comments', 11)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (2, N'write_comments', N'write_comments', 12)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (3, N'can_view_page', N'can_view_page', 20)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (4, N'can_view_page', N'can_view_page', 21)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (5, N'can_view_page', N'can_view_page', 22)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (6, N'can_view_page', N'can_view_page', 23)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (7, N'can_view_page', N'can_view_page', 24)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (9, N'can_view_page', N'can_view_page', 26)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (10, N'can_view_page', N'can_view_page', 27)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (11, N'can_view_page', N'can_view_page', 28)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (14, N'can_view_page', N'can_view_page', 31)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (15, N'can_view_page', N'can_view_page', 32)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (16, N'can_view_page', N'USR_IS_ADMIN', 33)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (17, N'USR_CAN_VIEW_ACCOUNT_SETTINGS', N'USR_IS_ADMIN', 34)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (18, N'USR_CAN_VIEW_ACCOUNT_SETTINGS', N'USR_IS_ADMIN', 35)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (19, N'USR_CAN_VIEW_ACCOUNT_SETTINGS', N'USR_IS_ADMIN', 36)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (20, N'USR_CAN_VIEW_ACCOUNT_SETTINGS', N'USR_IS_ADMIN', 37)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (21, N'USR_CAN_VIEW_ACCOUNT_SETTINGS', N'USR_IS_ADMIN', 38)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (22, N'USR_CAN_VIEW_ACCOUNT_SETTINGS', N'USR_IS_ADMIN', 39)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (23, N'USR_CAN_VIEW_ACCOUNT_SETTINGS', N'USR_IS_ADMIN', 40)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (24, N'USR_CAN_VIEW_ACCOUNT_SETTINGS', N'USR_IS_ADMIN', 41)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (8, N'can_view_page', N'can_view_page', 25)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (12, N'can_view_page', N'can_view_page', 29)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (13, N'can_view_page', N'can_view_page', 30)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (25, N'CAN_MANAGE_MAIL_LAYOUTS', N'CAN_MANAGE_MAIL_LAYOUTS', 61)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (26, N'CAN_MANAGE_SMTP', N'CAN_MANAGE_SMTP', 78)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (27, N'CAN_MANAGE_SMTP', N'CAN_MANAGE_SMTP', 79)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (28, N'CAN_MANAGE_SMTP', N'CAN_MANAGE_SMTP', 80)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (29, N'CAN_MANAGE_SMTP', N'CAN_MANAGE_SMTP', 81)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (30, N'CAN_MANAGE_SMTP', N'CAN_MANAGE_SMTP', 82)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (31, N'CAN_MANAGE_SMTP', N'CAN_MANAGE_SMTP', 83)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (32, N'CAN_MANAGE_SMTP', N'CAN_MANAGE_SMTP', 84)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (33, N'CAN_MANAGE_SMTP', N'CAN_MANAGE_SMTP', 85)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (1023, N'CAN_CONFIGURE_CONTENTTYPES', N'CAN_CONFIGURE_CONTENTTYPES', 106)

INSERT [dbo].[nfcms_Settings2Permissions] ([id], [FrontEndPM], [BackEndPM], [sid]) VALUES (1024, N'CAN_CONFIGURE_CONTENTTYPES', N'CAN_CONFIGURE_CONTENTTYPES', 107)

SET IDENTITY_INSERT [dbo].[nfcms_Settings2Permissions] OFF

SET IDENTITY_INSERT [dbo].[nfcms_SettingStores] ON 



INSERT [dbo].[nfcms_SettingStores] ([id], [sid], [val]) VALUES (1, 33, N'true')

INSERT [dbo].[nfcms_SettingStores] ([id], [sid], [val]) VALUES (2, 33, N'false')

INSERT [dbo].[nfcms_SettingStores] ([id], [sid], [val]) VALUES (3, 34, N'yes')

INSERT [dbo].[nfcms_SettingStores] ([id], [sid], [val]) VALUES (4, 34, N'no')

INSERT [dbo].[nfcms_SettingStores] ([id], [sid], [val]) VALUES (5, 35, N'all')

INSERT [dbo].[nfcms_SettingStores] ([id], [sid], [val]) VALUES (6, 35, N'only_registered')

INSERT [dbo].[nfcms_SettingStores] ([id], [sid], [val]) VALUES (7, 35, N'only_friends')

INSERT [dbo].[nfcms_SettingStores] ([id], [sid], [val]) VALUES (8, 35, N'not_public')

SET IDENTITY_INSERT [dbo].[nfcms_SettingStores] OFF

SET IDENTITY_INSERT [dbo].[nfcms_SettingStores2Locales] ON 



INSERT [dbo].[nfcms_SettingStores2Locales] ([id], [stid], [langLine]) VALUES (1, 1, N'LANG_STORE_SHOW_EMAIL_YES')

INSERT [dbo].[nfcms_SettingStores2Locales] ([id], [stid], [langLine]) VALUES (2, 2, N'LANG_STORE_SHOW_EMAIL_NO')

INSERT [dbo].[nfcms_SettingStores2Locales] ([id], [stid], [langLine]) VALUES (3, 3, N'LANG_STORE_RECIEVE_PMS_YES')

INSERT [dbo].[nfcms_SettingStores2Locales] ([id], [stid], [langLine]) VALUES (4, 4, N'LANG_STORE_RECIEVE_PMS_NO')

INSERT [dbo].[nfcms_SettingStores2Locales] ([id], [stid], [langLine]) VALUES (5, 5, N'LANG_STORE_PROFILE_PUBLIC_ALL')

INSERT [dbo].[nfcms_SettingStores2Locales] ([id], [stid], [langLine]) VALUES (6, 6, N'LANG_STORE_PROFILE_PUBLIC_ONLY_REGGED')

INSERT [dbo].[nfcms_SettingStores2Locales] ([id], [stid], [langLine]) VALUES (7, 7, N'LANG_STORE_PROFILE_PUBLIC_ONLY_FRIENDS')

INSERT [dbo].[nfcms_SettingStores2Locales] ([id], [stid], [langLine]) VALUES (8, 8, N'LANG_STORE_PROFILE_PUBLIC_NOT')

SET IDENTITY_INSERT [dbo].[nfcms_SettingStores2Locales] OFF

SET IDENTITY_INSERT [dbo].[nfcms_SettingValues] ON 



INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (53, N'', 61)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (70, N'', 78)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (71, N'', 79)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (72, N'', 80)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (73, N'', 81)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (74, N'', 82)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (75, N'', 83)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (76, N'', 84)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (77, N'', 85)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (1, N'4', 12)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (2, N'4', 12)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (3, N'10', 13)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (9, N'true', 20)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (12, N'10', 23)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (13, N'10', 24)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (15, N'false', 26)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (16, N'de-DE', 27)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (17, N'nftheme', 28)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (20, N'10', 31)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (21, N'networkfreaksde', 32)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (22, N'false', 33)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (23, N'false', 33)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (24, N'yes', 34)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (25, N'true', 34)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (28, N'', 36)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (29, N'', 37)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (30, N'', 38)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (31, N'', 39)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (32, N'', 40)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (33, N'', 41)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (10, N'10', 21)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (11, N'BDE3F3BE-6B25-44B7-816A-CF3C134266BE', 22)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (14, N' ', 25)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (18, N'NetworkFreaks.de', 29)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (19, N'10', 30)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (26, N'only_friends', 35)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (27, N'all', 35)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (1073, N'True', 106)

INSERT [dbo].[nfcms_SettingValues] ([id], [SettingValue], [SettingID]) VALUES (1074, N'C:\Users\Malte\Documents\Visual Studio 2012\Projects\Ren.CMS.Net\nCMS_NET\Binaries\Converter\FFMEG.exe', 107)

SET IDENTITY_INSERT [dbo].[nfcms_SettingValues] OFF

INSERT [dbo].[nfcms_Sub_Categories] ([PKID], [CID], [ref], [shortName], [longName], [langCode]) VALUES (N'50ba5b12-fce8-44f5-b2ca-68afa52dfbfb', N'51cf5feb-46a3-42cc-8a10-f3500095634e', N'news', N'test', N'Test', N'deDE')

SET IDENTITY_INSERT [dbo].[nfcms_Thumpnails_Module] ON 



INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1, N'f6f05cb6-276e-4c88-b028-b8396e90058c', CAST(0x0000A1C50009FD08 AS DateTime), N'C:\Users\Malte\Documents\Visual Studio 2012\Projects\Ren.CMS.Net\nCMS_NET\Thumpnails\Storage\1\64x64\f6f05cb6-276e-4c88-b028-b8396e90058c.thump.jpeg', 64, 64)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (3, N'f6f05cb6-276e-4c88-b028-b8396e90058c', CAST(0x0000A1C5000C30B4 AS DateTime), N'/Thumpnails/Storage/3/128x128/f6f05cb6-276e-4c88-b028-b8396e90058c.thump.jpeg', 128, 128)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1002, N'5f9dc9ba-3292-4b00-a3aa-3face9158827', CAST(0x0000A1C70149A510 AS DateTime), N'/Thumpnails/Storage/1002/64x64/5f9dc9ba-3292-4b00-a3aa-3face9158827.thump.jpeg', 64, 64)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1007, N'd934e15b-7fe9-4916-a8d2-fc544505d738', CAST(0x0000A1C70149A3E4 AS DateTime), N'/Thumpnails/Storage/1007/64x64/d934e15b-7fe9-4916-a8d2-fc544505d738.thump.jpeg', 64, 64)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1006, N'a6b98545-867c-4322-927e-08a35af01023', CAST(0x0000A1C70149A510 AS DateTime), N'/Thumpnails/Storage/1006/64x64/a6b98545-867c-4322-927e-08a35af01023.thump.jpeg', 64, 64)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1004, N'4cb5fe4f-26d6-455f-8de7-a9a38dd76a5a', CAST(0x0000A1C70149A3E4 AS DateTime), N'/Thumpnails/Storage/1004/64x64/4cb5fe4f-26d6-455f-8de7-a9a38dd76a5a.thump.jpeg', 64, 64)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1010, N'16dddf15-f977-4796-a099-3b9bd714de04', CAST(0x0000A1C70149A63C AS DateTime), N'/Thumpnails/Storage/1010/64x64/16dddf15-f977-4796-a099-3b9bd714de04.thump.jpeg', 64, 64)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1011, N'c8f6b9ba-28f7-4e80-92d0-62de084b4edb', CAST(0x0000A1C70149A63C AS DateTime), N'/Thumpnails/Storage/1011/64x64/c8f6b9ba-28f7-4e80-92d0-62de084b4edb.thump.jpeg', 64, 64)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1012, N'a6b98545-867c-4322-927e-08a35af01023', CAST(0x0000A1C7014A5604 AS DateTime), N'/Thumpnails/Storage/1012/128x128/a6b98545-867c-4322-927e-08a35af01023.thump.jpeg', 128, 128)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1014, N'd6b7366a-f330-453d-90cf-111653e34330', CAST(0x0000A1C7014DDC5C AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1016, N'a6b98545-867c-4322-927e-08a35af01023', CAST(0x0000A1C7014DDB30 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1018, N'5f9dc9ba-3292-4b00-a3aa-3face9158827', CAST(0x0000A1C7014DDD88 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1022, N'c8f6b9ba-28f7-4e80-92d0-62de084b4edb', CAST(0x0000A1C7014E194C AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1023, N'a6b98545-867c-4322-927e-08a35af01023', CAST(0x0000A1C7014E4250 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1024, N'd6b7366a-f330-453d-90cf-111653e34330', CAST(0x0000A1C7014E4250 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1025, N'5f9dc9ba-3292-4b00-a3aa-3face9158827', CAST(0x0000A1C7014E4250 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1026, N'b132d119-9dcc-4fe7-8bf7-ecc889166d99', CAST(0x0000A1C7014E4250 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1029, N'21d46344-35b2-46a3-a317-373e1340e7f6', CAST(0x0000A1C7014EDF58 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1032, N'c8f6b9ba-28f7-4e80-92d0-62de084b4edb', CAST(0x0000A1C7014EDF58 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1034, N'a6b98545-867c-4322-927e-08a35af01023', CAST(0x0000A1C7014F63C4 AS DateTime), N'/Thumpnails/Storage/1034/128x72/a6b98545-867c-4322-927e-08a35af01023.thump.jpeg', 128, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1036, N'b132d119-9dcc-4fe7-8bf7-ecc889166d99', CAST(0x0000A1C7014F63C4 AS DateTime), N'/Thumpnails/Storage/1036/128x72/b132d119-9dcc-4fe7-8bf7-ecc889166d99.thump.jpeg', 128, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1038, N'd934e15b-7fe9-4916-a8d2-fc544505d738', CAST(0x0000A1C7014F63C4 AS DateTime), N'/Thumpnails/Storage/1038/128x72/d934e15b-7fe9-4916-a8d2-fc544505d738.thump.jpeg', 128, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1003, N'b132d119-9dcc-4fe7-8bf7-ecc889166d99', CAST(0x0000A1C70149A3E4 AS DateTime), N'/Thumpnails/Storage/1003/64x64/b132d119-9dcc-4fe7-8bf7-ecc889166d99.thump.jpeg', 64, 64)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1008, N'1d9deced-8e5a-4528-8302-d3cfe0169215', CAST(0x0000A1C70149A63C AS DateTime), N'/Thumpnails/Storage/1008/64x64/1d9deced-8e5a-4528-8302-d3cfe0169215.thump.jpeg', 64, 64)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1009, N'21d46344-35b2-46a3-a317-373e1340e7f6', CAST(0x0000A1C70149A63C AS DateTime), N'/Thumpnails/Storage/1009/64x64/21d46344-35b2-46a3-a317-373e1340e7f6.thump.jpeg', 64, 64)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1013, N'b132d119-9dcc-4fe7-8bf7-ecc889166d99', CAST(0x0000A1C7014DDB30 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1015, N'd934e15b-7fe9-4916-a8d2-fc544505d738', CAST(0x0000A1C7014DDC5C AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1017, N'4cb5fe4f-26d6-455f-8de7-a9a38dd76a5a', CAST(0x0000A1C7014DDC5C AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1019, N'1d9deced-8e5a-4528-8302-d3cfe0169215', CAST(0x0000A1C7014E16F4 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1020, N'21d46344-35b2-46a3-a317-373e1340e7f6', CAST(0x0000A1C7014E194C AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1021, N'16dddf15-f977-4796-a099-3b9bd714de04', CAST(0x0000A1C7014E194C AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1027, N'd934e15b-7fe9-4916-a8d2-fc544505d738', CAST(0x0000A1C7014E4250 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1028, N'4cb5fe4f-26d6-455f-8de7-a9a38dd76a5a', CAST(0x0000A1C7014E4250 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1030, N'16dddf15-f977-4796-a099-3b9bd714de04', CAST(0x0000A1C7014EDF58 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1031, N'1d9deced-8e5a-4528-8302-d3cfe0169215', CAST(0x0000A1C7014EDF58 AS DateTime), NULL, 72, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1033, N'4cb5fe4f-26d6-455f-8de7-a9a38dd76a5a', CAST(0x0000A1C7014F63C4 AS DateTime), N'/Thumpnails/Storage/1033/128x72/4cb5fe4f-26d6-455f-8de7-a9a38dd76a5a.thump.jpeg', 128, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1035, N'5f9dc9ba-3292-4b00-a3aa-3face9158827', CAST(0x0000A1C7014F63C4 AS DateTime), N'/Thumpnails/Storage/1035/128x72/5f9dc9ba-3292-4b00-a3aa-3face9158827.thump.jpeg', 128, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1037, N'd6b7366a-f330-453d-90cf-111653e34330', CAST(0x0000A1C7014F63C4 AS DateTime), N'/Thumpnails/Storage/1037/128x72/d6b7366a-f330-453d-90cf-111653e34330.thump.jpeg', 128, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1039, N'1d9deced-8e5a-4528-8302-d3cfe0169215', CAST(0x0000A1C7014F64F0 AS DateTime), N'/Thumpnails/Storage/1039/128x72/1d9deced-8e5a-4528-8302-d3cfe0169215.thump.jpeg', 128, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1040, N'21d46344-35b2-46a3-a317-373e1340e7f6', CAST(0x0000A1C7014F661C AS DateTime), N'/Thumpnails/Storage/1040/128x72/21d46344-35b2-46a3-a317-373e1340e7f6.thump.jpeg', 128, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1041, N'c8f6b9ba-28f7-4e80-92d0-62de084b4edb', CAST(0x0000A1C7014F661C AS DateTime), N'/Thumpnails/Storage/1041/128x72/c8f6b9ba-28f7-4e80-92d0-62de084b4edb.thump.jpeg', 128, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1042, N'16dddf15-f977-4796-a099-3b9bd714de04', CAST(0x0000A1C7014F661C AS DateTime), N'/Thumpnails/Storage/1042/128x72/16dddf15-f977-4796-a099-3b9bd714de04.thump.jpeg', 128, 72)

INSERT [dbo].[nfcms_Thumpnails_Module] ([id], [atID], [LastModification], [Path], [Width], [Height]) VALUES (1005, N'd6b7366a-f330-453d-90cf-111653e34330', CAST(0x0000A1C70149A3E4 AS DateTime), N'/Thumpnails/Storage/1005/64x64/d6b7366a-f330-453d-90cf-111653e34330.thump.jpeg', 64, 64)

SET IDENTITY_INSERT [dbo].[nfcms_Thumpnails_Module] OFF

SET IDENTITY_INSERT [dbo].[nfcms_User2Settingvalues] ON 



INSERT [dbo].[nfcms_User2Settingvalues] ([id], [sid], [uid], [vid]) VALUES (3, 11, N'8cbbfe36-c96e-46fc-a8af-b6757181e799', 5)

INSERT [dbo].[nfcms_User2Settingvalues] ([id], [sid], [uid], [vid]) VALUES (4, 11, N'bde3f3be-6b25-44b7-816a-cf3c134266be', 6)

INSERT [dbo].[nfcms_User2Settingvalues] ([id], [sid], [uid], [vid]) VALUES (5, 12, N'8cbbfe36-c96e-46fc-a8af-b6757181e799', 1)

INSERT [dbo].[nfcms_User2Settingvalues] ([id], [sid], [uid], [vid]) VALUES (6, 12, N'bde3f3be-6b25-44b7-816a-cf3c134266be', 2)

INSERT [dbo].[nfcms_User2Settingvalues] ([id], [sid], [uid], [vid]) VALUES (7, 33, N'8cbbfe36-c96e-46fc-a8af-b6757181e799', 22)

INSERT [dbo].[nfcms_User2Settingvalues] ([id], [sid], [uid], [vid]) VALUES (8, 33, N'bde3f3be-6b25-44b7-816a-cf3c134266be', 23)

INSERT [dbo].[nfcms_User2Settingvalues] ([id], [sid], [uid], [vid]) VALUES (9, 34, N'8cbbfe36-c96e-46fc-a8af-b6757181e799', 24)

INSERT [dbo].[nfcms_User2Settingvalues] ([id], [sid], [uid], [vid]) VALUES (10, 34, N'bde3f3be-6b25-44b7-816a-cf3c134266be', 25)

INSERT [dbo].[nfcms_User2Settingvalues] ([id], [sid], [uid], [vid]) VALUES (11, 35, N'8cbbfe36-c96e-46fc-a8af-b6757181e799', 26)

INSERT [dbo].[nfcms_User2Settingvalues] ([id], [sid], [uid], [vid]) VALUES (12, 35, N'bde3f3be-6b25-44b7-816a-cf3c134266be', 27)

SET IDENTITY_INSERT [dbo].[nfcms_User2Settingvalues] OFF

INSERT [dbo].[nfcms_Users] ([PKID], [Username], [Loginname], [ApplicationName], [Email], [Comment], [Password], [PasswordQuestion], [PasswordAnswer], [IsApproved], [LastActivityDate], [LastLoginDate], [LastPasswordChangedDate], [CreationDate], [IsOnLine], [IsLockedOut], [LastLockedOutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [IsSubscriber], [CustomerID], [PermissionGroup]) VALUES (N'5fa9ac51-823f-45f5-ad6e-946765c40f48', N'admin', N'admin', N'/', N'admin@localhost', N'Created on <08.09.2013 16:29:32>', N'bF1tO81pMd0SegDiAciD5w==', NULL, NULL, N'No', CAST(0x0000A23301108AE6 AS DateTime), NULL, CAST(0x0000A233010FC99D AS DateTime), CAST(0x0000A233010FC99D AS DateTime), NULL, N'No', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'admins')

SET IDENTITY_INSERT [dbo].[nfcms_Users_Settings_Table] ON 



INSERT [dbo].[nfcms_Users_Settings_Table] ([id], [settingName], [settingDefaultVal], [settingLangLine], [settingLongDescription], [SettingOrder], [DataType], [s_type]) VALUES (1212, N'test', N'testtest', NULL, NULL, 100, NULL, NULL)

INSERT [dbo].[nfcms_Users_Settings_Table] ([id], [settingName], [settingDefaultVal], [settingLangLine], [settingLongDescription], [SettingOrder], [DataType], [s_type]) VALUES (2323, N'test2', N'testtest2', NULL, NULL, 100, NULL, NULL)

SET IDENTITY_INSERT [dbo].[nfcms_Users_Settings_Table] OFF

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_nfcms_Filemanagement_Crossbrowsers_FileType]') AND type = 'D')

BEGIN

ALTER TABLE [dbo].[nfcms_Filemanagement_Crossbrowsers] ADD  CONSTRAINT [DF_nfcms_Filemanagement_Crossbrowsers_FileType]  DEFAULT ('video') FOR [FileType]

END



GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__nfcms_Thu__Width__40257DE4]') AND type = 'D')

BEGIN

ALTER TABLE [dbo].[nfcms_Thumpnails_Module] ADD  DEFAULT ((64)) FOR [Width]

END



GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__nfcms_Thu__Heigh__4119A21D]') AND type = 'D')

BEGIN

ALTER TABLE [dbo].[nfcms_Thumpnails_Module] ADD  DEFAULT ((64)) FOR [Height]

END



GO

