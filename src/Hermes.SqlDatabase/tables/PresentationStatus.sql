CREATE TABLE dbo.PresentationStatus
(
  PresentationStatusId   INT         NOT NULL,
  PresentationStatusName VARCHAR(50) NOT NULL,
  PresentationIsArchived BIT         NOT NULL,
  SortOrder              INT         NOT NULL,
  IsEnabled              BIT         NOT NULL,
  CONSTRAINT pkcPresentationStatus PRIMARY KEY CLUSTERED (PresentationStatusId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationStatus',                                        @value=N'Represents the status of a speaker''s presentation.',                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationStatus', @level2name=N'PresentationStatusId',   @value=N'The identifier of the presentation status record.',                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationStatus', @level2name=N'PresentationStatusName', @value=N'The name of the presentation status.',                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationStatus', @level2name=N'PresentationIsArchived', @value=N'Flag indicating whether the presentation status has been archived.',                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationStatus', @level2name=N'SortOrder',              @value=N'The order in which the presentation status should be displayed.',                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationStatus', @level2name=N'IsEnabled',              @value=N'Flag indicating whether the presentation status is enabled.',                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationStatus', @level2name=N'pkcPresentationStatus',  @value=N'Defines the primary key for the PresentationStatus table using the PresentationStatusId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO