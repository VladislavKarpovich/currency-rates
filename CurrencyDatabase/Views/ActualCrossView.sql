﻿CREATE VIEW [dbo].[ActualCrossView]
	AS (
	SELECT CR.*,
	CurrFR.Name AS CurrencyFrom, CurrTo.Name AS CurrencyTo,
	O.Address, O.Contacts, O.Tittle, O.City, O.Bank
	FROM ActualCrossRate CR
	INNER JOIN Currency CurrFR
		ON CurrFR.CurrencyId = CR.CurrencyIdFrom
	INNER JOIN Currency CurrTO
		ON CurrTO.CurrencyId = CR.CurrencyIdTo
	INNER JOIN OfficeView O
		ON O.OfficeId = CR.OfficeId
	);
