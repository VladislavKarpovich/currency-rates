﻿CREATE TABLE [dbo].[Office]
(
	[OfficeId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[BankId] UNIQUEIDENTIFIER NOT NULL, 
    [CityId] UNIQUEIDENTIFIER NOT NULL, 
    [Address] NVARCHAR(MAX) NULL, 
    [Contacts] NVARCHAR(MAX) NULL, 
    [Tittle] NVARCHAR(MAX) NULL   
)
