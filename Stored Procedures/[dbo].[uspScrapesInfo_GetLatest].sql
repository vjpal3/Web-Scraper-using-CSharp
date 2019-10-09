SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[uspScrapesInfo_GetLatest]
as
begin
	set nocount on;
	SELECT TOP 1 ScrapeId FROM dbo.ScrapesInfo ORDER BY ScrapeId DESC
end
GO

