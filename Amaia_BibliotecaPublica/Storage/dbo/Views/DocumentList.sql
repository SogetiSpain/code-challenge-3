﻿--CREATE VIEW [dbo].[DocumentList]
--	AS SELECT document.Id AS Id,
--	document.Title AS Title,
--	documentProperties.CanBeLent as CanBeLent,
--	documentProperties.MaxDaysRent AS MaxDaysRent,
--	documentType.Code AS TypeCode
--	FROM [dbo].[Documents] AS document
--		INNER JOIN [dbo].[DocumentProperties] AS documentProperties ON document.DocumentPropertyId = documentProperties.Id
--		INNER JOIN [dbo].[DocumentTypes] AS documentType ON documentProperties.TypeId = documentType.Id		
