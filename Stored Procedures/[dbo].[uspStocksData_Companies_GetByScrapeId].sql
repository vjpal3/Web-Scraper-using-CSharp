SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[uspStocksData_Companies_GetByScrapeId] 
		@ScrapeId int
as
begin
	set nocount on;
	SELECT 
		SymbolName, CompanyName,
		LastPrice, Change, PercentChange, Shares, TradeDate
	FROM Companies, StocksData
	WHERE Companies.Id = StocksData.SymbolId
	AND StocksData.ScrapeId = @ScrapeId;
	
end
GO

