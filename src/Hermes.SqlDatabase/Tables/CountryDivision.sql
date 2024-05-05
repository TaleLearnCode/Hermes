﻿CREATE TABLE dbo.CountryDivision
(
	CountryDivisionCode CHAR(3)      NOT NULL,
	CountryCode         CHAR(2)      NOT NULL,
	CountryDivisionName VARCHAR(100) NOT NULL,
	CategoryName        VARCHAR(100) NOT NULL,
	IsEnabled           BIT          NOT NULL,
	CONSTRAINT pkcCountryDivision PRIMARY KEY CLUSTERED (CountryCode , CountryDivisionCode),
	CONSTRAINT fkCountryDivision_Country FOREIGN KEY (CountryCode) REFERENCES dbo.Country(CountryCode)
);
GO

EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'CountryDivision',                                           @value = N'Lookup table representing the world regions as defined by the ISO 3166-2 standard.',                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'CountryDivision', @level2name=N'CountryDivisionCode',       @value = N'Identifier of the country division using the ISO 3166-2 Alpha-2 code.',                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'CountryDivision', @level2name=N'CountryDivisionName',       @value = N'Name of the country using the ISO 3166-2 Subdivision Name.',                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'CountryDivision', @level2name=N'CategoryName',              @value = N'The category name of the country division.',                                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'CountryDivision', @level2name=N'IsEnabled',                 @value = N'Flag indicating whether the country division record is enabled.',                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'CountryDivision', @level2name=N'pkcCountryDivision',        @value = N'Defines the primary key fro the CountryDivision table using the CountryId and CountryDivisionId columns.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0Name=N'dbo', @level1name=N'CountryDivision', @level2name=N'fkCountryDivision_Country', @value = N'Defines the relationship between CountryDivision and Country tables.',                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO