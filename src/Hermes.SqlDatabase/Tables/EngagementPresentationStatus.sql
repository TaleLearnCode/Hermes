CREATE TABLE dbo.EngagementPresentationStatus
(
  EngagementPresentationStatusId   INT           NOT NULL,
  EngagementPresentationStatusName NVARCHAR(100) NOT NULL,
  StatusDescription                NVARCHAR(500)     NULL,
  SortOrder                        INT           NOT NULL,
  IsEnabled                        BIT           NOT NULL,
  CONSTRAINT pkcEngagementPresentationStatus PRIMARY KEY CLUSTERED (EngagementPresentationStatusId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationStatus',                                                  @value=N'Represents a status of an engagement presentation.',                                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationStatus', @level2name=N'EngagementPresentationStatusId',   @value=N'The identifier of the engagement presentation status record.',                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationStatus', @level2name=N'EngagementPresentationStatusName', @value=N'The name of the engagement presentation status.',                                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationStatus', @level2name=N'SortOrder',                        @value=N'The sorting order of the engagement presentation status.',                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationStatus', @level2name=N'StatusDescription',                @value=N'A description of the engagement presentation status.',                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationStatus', @level2name=N'IsEnabled',                        @value=N'Flag indicating whether the engagement presentation status is enabled.',                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationStatus', @level2name=N'pkcEngagementPresentationStatus',  @value=N'Defines the primary key for the EngagementPresentationStatus table using the EngagementPresentationStatusId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO