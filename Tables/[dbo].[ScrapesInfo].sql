SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScrapesInfo](
	[ScrapeId] [int] IDENTITY(1,1) NOT NULL,
	[ScrapeDate] [datetime2](7) NOT NULL,
	[TimeZone] [nchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ScrapesInfo] ADD PRIMARY KEY CLUSTERED 
(
	[ScrapeId] ASC
) ON [PRIMARY]
GO

