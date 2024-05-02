CREATE TABLE dbo.LearningObjective
(
  LearningObjectiveId   INT            NOT NULL IDENTITY(1,1),
  PresentationTextId    INT            NOT NULL,
  LearningObjectiveText NVARCHAR(1000) NOT NULL,
  SortOrder             INT            NOT NULL,
  CONSTRAINT pkcLearningObjective PRIMARY KEY CLUSTERED (LearningObjectiveId),
  CONSTRAINT fkLearningObjective_PresentationText FOREIGN KEY (PresentationTextId) REFERENCES dbo.PresentationText (PresentationTextId),
  CONSTRAINT ucLearningObjective UNIQUE (PresentationTextId, SortOrder)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'LearningObjective',                                                      @value=N'Represents a learning objective of a presentation.',                                                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'LearningObjective', @level2name=N'LearningObjectiveId',                  @value=N'The identifier of the learning objective record.',                                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'LearningObjective', @level2name=N'PresentationTextId',                   @value=N'The identifier of the associated presentation (text) record.',                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'LearningObjective', @level2name=N'LearningObjectiveText',                @value=N'The text of the learning objective.',                                                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'LearningObjective', @level2name=N'SortOrder',                            @value=N'The sorting order of the learning objective.',                                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'LearningObjective', @level2name=N'pkcLearningObjective',                 @value=N'Defines the primary key for the LearningObjective table using the LearningObjectiveId column.',                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'LearningObjective', @level2name=N'fkLearningObjective_PresentationText', @value=N'Defines the relationship between the LearningObjective and PresentationText tables using the PresentationTextId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO