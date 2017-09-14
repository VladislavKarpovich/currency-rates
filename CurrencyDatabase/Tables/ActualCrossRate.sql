﻿CREATE TABLE [dbo].[ActualCrossRate]
(
	[ActualCrossRateId] UNIQUEIDENTIFIER  DEFAULT NEWID() PRIMARY KEY, 
    [CurrencyIdFrom] UNIQUEIDENTIFIER NOT NULL, 
    [CurrencyIdTo] UNIQUEIDENTIFIER NOT NULL, 
    [DateTime] DATETIME NULL, 
    [Bid] FLOAT NOT NULL, 
    [Ask] FLOAT NOT NULL, 
    [OfficeId] UNIQUEIDENTIFIER NOT NULL
)