CREATE TABLE dbo.EngagementPresentation
(
  EngagementPresentationId       INT            NOT NULL IDENTITY(1,1),
  EngagementId                   VARCHAR(200)   NOT NULL,
  PresentationId                 VARCHAR(200)   NOT NULL,
  EngagementPresentationStatusId INT            NOT NULL CONSTRAINT dfEngagementPresentation_EngagementPresentationStatusId DEFAULT 1,
  StartDateTime                  DATETIME2          NULL,
  PresentationLength             INT            NOT NULL,
  Room                           NVARCHAR(100)      NULL,
  EngagementPresentationUrl      VARCHAR(200)       NULL,
  LangaugeCode                   CHAR(2)        NOT NULL CONSTRAINT dfEngagementPresentation_LangaugeCode DEFAULT('en'),
  PresentationTitle              NVARCHAR(300)  NOT NULL,
  PresentationShortTitle         NVARCHAR(60)       NULL,
  Abstract                       NVARCHAR(3000)     NULL,
  ShortAbstract                  NVARCHAR(2000)     NULL,
  ElevatorPitch                  NVARCHAR(160)      NULL,
  AdditionalDetails              NVARCHAR(3000)     NULL,
  IsEnabled                      BIT            NOT NULL CONSTRAINT dfEngagementPresentation_IsEnabled DEFAULT 1,
  CONSTRAINT pkcEngagementPresentation PRIMARY KEY CLUSTERED (EngagementPresentationId),
  CONSTRAINT fkEngagementPresentation_Engagement FOREIGN KEY (EngagementId) REFERENCES dbo.Engagement (Permalink),
  CONSTRAINT fkEngagementPresentation_Presentation FOREIGN KEY (PresentationId) REFERENCES dbo.Presentation (Permalink),
  CONSTRAINT fkEngagementPresentation_EngagementPresentationStatus FOREIGN KEY (EngagementPresentationStatusId) REFERENCES dbo.EngagementPresentationStatus (EngagementPresentationStatusId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'EngagementPresentationId',                              @value=N'The identifier of the engagement presentation record.',                                                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'EngagementId',                                          @value=N'The identifier of the associated engagement.',                                                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'PresentationId',                                        @value=N'The identifier of the associated presentation.',                                                                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'EngagementPresentationStatusId',                        @value=N'The identifier of the status of the engagement presentation.',                                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'StartDateTime',                                         @value=N'The date and time the engagement presentation starts.',                                                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'PresentationLength',                                    @value=N'The length of the presentation in minutes.',                                                                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'Room',                                                  @value=N'The room where the presentation will be held.',                                                                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'EngagementPresentationUrl',                             @value=N'The URL for the engagement presentation.',                                                                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'LangaugeCode',                                           @value=N'The language code for the presentation.',                                                                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'PresentationTitle',                                     @value=N'The full title of the presentation.',                                                                                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'PresentationShortTitle',                                @value=N'The short title of the presentation.',                                                                                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'Abstract',                                              @value=N'The full abstract for the presentation.',                                                                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'ShortAbstract',                                         @value=N'The short abstract for the presentation.',                                                                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'ElevatorPitch',                                         @value=N'The summary for the presentation.',                                                                                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'AdditionalDetails',                                     @value=N'Additional details for the presentation.',                                                                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'IsEnabled',                                             @value=N'Flag indicating whether the engagement presentation is enabled.',                                                                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'pkcEngagementPresentation',                             @value=N'Defines the primary key for the EngagementPresentation table using the EngagementPresentationId column.',                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'fkEngagementPresentation_Engagement',                   @value=N'Defines the relationship between the EngagementPresentation and Engagement tables using the EngagementId column.',                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'fkEngagementPresentation_Presentation',                 @value=N'Defines the relationship between the EngagementPresentation and Presentation tables using the PresentationId column.',                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentation', @level2name=N'fkEngagementPresentation_EngagementPresentationStatus', @value=N'Defines the relationship between the EngagementPresentation and EngagementPresentationStatus tables using the EngagementPresentationStatusId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO