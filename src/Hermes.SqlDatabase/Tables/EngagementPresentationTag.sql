CREATE TABLE dbo.EngagementPresentationTag
(
  EngagementPresentationTagId INT NOT NULL IDENTITY(1,1),
  EngagementPresentationId    INT NOT NULL,
  TagId                       INT NOT NULL,
  CONSTRAINT pkcEngagementPresentationTag PRIMARY KEY (EngagementPresentationTagId),
  CONSTRAINT fkEngagementPresentationTag_EngagementPresentation FOREIGN KEY (EngagementPresentationId) REFERENCES dbo.EngagementPresentation (EngagementPresentationId),
  CONSTRAINT fkEngagementPresentationTag_Tag FOREIGN KEY (TagId) REFERENCES dbo.Tag (TagId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationTag',                                                                    @value=N'Associates a tag with an engagement presentation.',                                                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationTag', @level2name=N'EngagementPresentationTagId',                        @value=N'The identifier of the engagement presentation tag record.',                                                                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationTag', @level2name=N'EngagementPresentationId',                           @value=N'The identifier of the engagement presentation.',                                                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationTag', @level2name=N'TagId',                                              @value=N'The identifier of the associated tag.',                                                                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationTag', @level2name=N'pkcEngagementPresentationTag',                       @value=N'Defines the primary key for the EngagementPresentationTag table using the EngagementPresentationTagId column.',                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationTag', @level2name=N'fkEngagementPresentationTag_EngagementPresentation', @value=N'Defines the relationship between the EngagementPresentationTag and EngagementPresentation tables using the EngagementPresentationId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationTag', @level2name=N'fkEngagementPresentationTag_Tag',                    @value=N'Defines the relationship between the EngagementPresentationTag and Tag tables using the TagId column.',                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO