CREATE VIEW [dbo].[OfficeView]
	AS (
	SELECT O.*, B.Name AS Bank, C.Name AS City
	FROM Office O  
	INNER JOIN Bank B 
		ON B.BankId = O.BankId
	INNER JOIN City C 
		ON C.CityId = O.CityId
	);