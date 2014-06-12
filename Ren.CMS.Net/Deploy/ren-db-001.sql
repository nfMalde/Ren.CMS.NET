 IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN('nfcms_Tasks','dbo.nfcms_Tasks')) > 0
	DROP TABLE dbo.nfcms_Tasks

/****** Object:  Table [dbo].[nfcms_Tasks]    Script Date: 12.06.2014 23:40:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[nfcms_Tasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NOT NULL,
	[Running] [bit] NOT NULL,
	[TaskName] [varchar](250) NOT NULL,
	[CurrentAction] [varchar](250) NULL,
	[ModuleDBTable] [varchar](250) NULL,
	[ModuleDBIdentifier] [varchar](250) NULL,
	[ModuleDBidValue] [varchar](250) NULL,
	[Percentage] [decimal](18, 2) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[nfcms_Tasks] ADD  CONSTRAINT [DF_nfcms_Tasks_Percentage]  DEFAULT ((0)) FOR [Percentage]
GO


