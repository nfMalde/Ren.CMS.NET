 IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN('nfcms_Content_Attachment','dbo.nfcms_Content_Attachment')) > 0
	DROP TABLE [dbo].[nfcms_Content_Attachment]
/****** Object:  Table [dbo].[nfcms_Content_Attachment]    Script Date: 15.06.2014 18:21:21 ******/

GO

/****** Object:  Table [dbo].[nfcms_Content_Attachment]    Script Date: 15.06.2014 18:21:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[nfcms_Content_Attachment](
	[PKID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[contentID] [int] NOT NULL,
	[attachment_type_id] [int] NOT NULL,
	[filePath] [varchar](255) NOT NULL,
	[thumnailPath] [varchar](255) NULL,
	[usage] [int] NULL,
	[title] [varchar](150) NULL
	) ON [PRIMARY] 

GO

 IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN('nfcms_Content_Attachment_Remark_Types','dbo.nfcms_Content_Attachment_Remark_Types')) > 0
	DROP TABLE [dbo].[nfcms_Content_Attachment_Remark_Types]

CREATE TABLE [dbo].[nfcms_Content_Attachment_Remark_Types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Remarkname] [varchar](250) NOT NULL,
	[RemarkLocalLine] [varchar](250) NULL,
	[RemarkLocalPackage] [varchar](250) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
 IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN('nfcms_Content_Attachment_Remarks','dbo.nfcms_Content_Attachment_Remarks')) > 0
	DROP TABLE [dbo].[nfcms_Content_Attachment_Remarks]

CREATE TABLE [dbo].[nfcms_Content_Attachment_Remarks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[remarkType] [int] NOT NULL,
	[RemarkVarchar250] [varchar](250) NULL,
	[RemarkText] [text] NULL,
	[attachmentID] [varchar](255) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
 IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN('nfcms_Content_AttachmentTypes','dbo.nfcms_Content_AttachmentTypes')) > 0
	DROP TABLE [dbo].[nfcms_Content_AttachmentTypes]

CREATE TABLE [dbo].[nfcms_Content_AttachmentTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
	[StoragePath] [varchar](255) NOT NULL,
	[HandlerNamespace] [varchar] (255) NULL DEFAULT 'Ren.CMS.Content.ContentAttachmentHandlers.ContentAttachmentHandlerBase'
) ON [PRIMARY]

GO
 IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN('nfcms_Content_AttachmentTypes_ExtensionSettings','dbo.nfcms_Content_AttachmentTypes_ExtensionSettings')) > 0
	DROP TABLE [dbo].[nfcms_Content_AttachmentTypes_ExtensionSettings]

CREATE TABLE [dbo].[nfcms_Content_AttachmentTypes_ExtensionSettings](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[attachmentTypeId] [int] NOT NULL,
	[extension] [varchar](50) NOT NULL,
	[maxFileSize] [bigint] NULL,
	[convertFile] [bit] NOT NULL
) ON [PRIMARY]


GO

 IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN('nfcms_Content_Attachment_Roles','dbo.nfcms_Content_Attachment_Roles')) > 0
	DROP TABLE [dbo].[nfcms_Content_Attachment_Roles]


CREATE TABLE [dbo].[nfcms_Content_Attachment_Roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[SubPath] [varchar](250) NOT NULL,/** Sub Path for Filemanagement **/
	[RoleName] [varchar](250) NOT NULL,
	[RoleLangLine] [varchar](250) NULL,
	[RoleLangPackage] [varchar] (250) NULL
) ON [PRIMARY]


GO
 IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN('nfcms_Content_Attachment_Roles_Bindings','dbo.nfcms_Content_Attachment_Roles_Bindings')) > 0
	DROP TABLE [dbo].[nfcms_Content_Attachment_Roles_Bindings]

CREATE TABLE [dbo].[nfcms_Content_Attachment_Roles_Bindings](
[id] [INT] IDENTITY(1,1) NOT NULL,
[RoleID] INT NOT NULL,
[BindingType] VARCHAR(10) NOT NULL,
[BindingValue] VARCHAR(255) NOT NULL

) ON [PRIMARY]