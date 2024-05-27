CREATE TABLE dbo.Engagement
(
  Permalink               VARCHAR(200)   NOT NULL,
  EngagementTypeId        INT            NOT NULL CONSTRAINT dfEngagement_EngagementTypeId DEFAULT 1,
  EngagementStatusId      INT            NOT NULL CONSTRAINT dfEngagement_EngagementStatusId DEFAULT 1,
  EngagementName          NVARCHAR(200)  NOT NULL,
  CountryCode             CHAR(2)        NOT NULL,
  CountryDivisionCode     CHAR(3)            NULL,
  City                    NVARCHAR(100)  NOT NULL,
  Venue                   NVARCHAR(200)      NULL,
  OverviewLocation        NVARCHAR(300)      NULL,
  ListingLocation         NVARCHAR(100)      NULL,
  StartDate               DATE           NOT NULL,
  EndDate                 DATE           NOT NULL,
  TimeZoneId              VARCHAR(100)   NOT NULL,
  LanguageCode            CHAR(2)        NOT NULL,
  StartingCost            NVARCHAR(20)       NULL,
  EndingCost              NVARCHAR(20)       NULL,
  EngagementDescription   NVARCHAR(2000)     NULL,
  EngagementSummary       NVARCHAR(160)      NULL,
  EngagementUrl           VARCHAR(200)       NULL,
  IncludeInPublicProfile  BIT            NOT NULL CONSTRAINT dfEngagement_IncludeInPublicProfile DEFAULT 1,
  IsVirtual               BIT            NOT NULL CONSTRAINT dfEngagemnet_IsVirtual DEFAULT 0,
  IsHybrid                BIT            NOT NULL CONSTRAINT dfEngagement_IsHybrid DEFAULT 0,
  IsPublic                BIT            NOT NULL CONSTRAINT dfEngagement_IsPublic DEFAULT 1,
  IsEnabled               BIT            NOT NULL CONSTRAINT dfEngagement_IsEnabled DEFAULT 1,
  CONSTRAINT pkcEngagement PRIMARY KEY CLUSTERED (Permalink),
  CONSTRAINT fkEngagement_EngagementType FOREIGN KEY (EngagementTypeId) REFERENCES dbo.EngagementType (EngagementTypeId),
  CONSTRAINT fkEngagement_EngagementStatus FOREIGN KEY (EngagementStatusId) REFERENCES dbo.EngagementStatus (EngagementStatusId),
  CONSTRAINT fkEngagement_TimeZone FOREIGN KEY (TimeZoneId) REFERENCES dbo.TimeZone (TimeZoneId),
  CONSTRAINT fkEngagement_Language FOREIGN KEY (LanguageCode) REFERENCES dbo.[Language] (LanguageCode),
  CONSTRAINT fkEngagement_Country FOREIGN KEY (CountryCode) REFERENCES dbo.Country (CountryCode),
  CONSTRAINT fkEngagement_CountryDivision FOREIGN KEY (CountryCode, CountryDivisionCode) REFERENCES dbo.CountryDivision (CountryCode, CountryDivisionCode),
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement',                                          @value=N'Represents an engagement.',                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'Permalink',                @value=N'The unique identifier for the engagement.',                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementTypeId',         @value=N'The identifier of the engagement type.',                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementStatusId',       @value=N'The identifier of the engagement status.',                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementName',           @value=N'The name of the engagement.',                                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'CountryCode',              @value=N'The country code for the engagement.',                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'CountryDivisionCode',      @value=N'The country division code for the engagement.',                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'City',                     @value=N'The city where the engagement is located.',                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'Venue',                    @value=N'The venue where the engagement is located.',                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'OverviewLocation',         @value=N'The location of the engagement.',                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'ListingLocation',          @value=N'The location of the engagement as it should be listed.',                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'StartDate',                @value=N'The start date of the engagement.',                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EndDate',                  @value=N'The end date of the engagement.',                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'TimeZoneId',               @value=N'The time zone of the engagement.',                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'StartingCost',             @value=N'The starting cost of the engagement.',                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EndingCost',               @value=N'The ending cost of the engagement.',                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementDescription',    @value=N'The description of the engagement.',                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementSummary',        @value=N'The summary of the engagement.',                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'EngagementUrl',            @value=N'The URL of the engagement.',                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'IncludeInPublicProfile',   @value=N'Flag indicating whether the engagement should be included in the public profile.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'IsVirtual',                @value=N'Flag indicating whether the engagement is virtual.',                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'IsPublic',                 @value=N'Flag indicating whether the engagement is public.',                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'IsEnabled',                @value=N'Flag indicating whether the engagement is enabled.',                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Engagement', @level2name=N'pkcEngagement',            @value=N'Defines the primary key for the Engagement table using the Permalink column.',  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO