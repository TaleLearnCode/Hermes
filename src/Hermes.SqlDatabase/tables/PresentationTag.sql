CREATE TABLE dbo.PresentationTag
(
  PresentationTagId INT NOT NULL IDENTITY(1,1),
  PresentationId    INT NOT NULL,
  TagId             INT NOT NULL,
  CONSTRAINT pkcPresentationTag PRIMARY KEY CLUSTERED (PresentationTagId),
  CONSTRAINT fkPresentationTag_Presentation FOREIGN KEY (PresentationId) REFERENCES dbo.Presentation (PresentationId),
  CONSTRAINT fkPresentationTag_Tag FOREIGN KEY (TagId) REFERENCES dbo.Tag (TagId),
  CONSTRAINT unqPresentationTag_PresentationId_TagId UNIQUE (PresentationId, TagId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationTag',                                                @value=N'Represents the association between a presentation and a tag.',                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationTag', @level2name=N'PresentationTagId',              @value=N'The identifier of the presentation/tag record.',                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationTag', @level2name=N'TagId',                          @value=N'Identifier of the associated tag.',                                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationTag', @level2name=N'PresentationId',                 @value=N'Identifier of the associated presentation.',                                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationTag', @level2name=N'pkcPresentationTag',             @value=N'Defines the primary key for the PresentationTag table using the PresentationTagId column.',                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationTag', @level2name=N'fkPresentationTag_Tag',          @value=N'Defines the relationship between the PresentationTag and Tag tables using the TagId column.',                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationTag', @level2name=N'fkPresentationTag_Presentation', @value=N'Defines the relationship between the PresentationTag and Presentation tables using the PresentationId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO