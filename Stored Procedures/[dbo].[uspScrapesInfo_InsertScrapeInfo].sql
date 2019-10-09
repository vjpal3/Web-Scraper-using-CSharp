SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspScrapesInfo_InsertScrapeInfo]
					@ScrapeDate datetime2, @TimeZone nchar(6)
AS
SET NOCOUNT ON
Insert into dbo.ScrapesInfo
			(ScrapeDate, TimeZone)
		values
			(@ScrapeDate, @TimeZone)
GO

