CREATE TABLE dbo.EngagementPresentationLearningObjective
(
  EngagementPresentationLearningObjectiveId INT            NOT NULL IDENTITY(1,1),
  EngagementPresentationId                  INT            NOT NULL,
  LearningObjectiveText                     NVARCHAR(1000) NOT NULL,
  SortOrder                                 INT            NOT NULL,
  CONSTRAINT pkcEngagementPresentationLearningObjective PRIMARY KEY (EngagementPresentationLearningObjectiveId),
  CONSTRAINT fkEngagementPresentationLearningObjective_EngagementPresentation FOREIGN KEY (EngagementPresentationId) REFERENCES dbo.EngagementPresentation (EngagementPresentationId),
  CONSTRAINT ucEngagementPresentationLearningObjective UNIQUE (EngagementPresentationId, SortOrder)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationLearningObjective',                                                                                  @value=N'Represents a learning objective of an engagement presentation.',                                                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationLearningObjective', @level2name=N'EngagementPresentationLearningObjectiveId',                        @value=N'The identifier of the engagement presentation learning objective record.',                                                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationLearningObjective', @level2name=N'EngagementPresentationId',                                         @value=N'The identifier of the engagement presentation.',                                                                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationLearningObjective', @level2name=N'LearningObjectiveText',                                            @value=N'The text of the learning objective.',                                                                                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationLearningObjective', @level2name=N'SortOrder',                                                        @value=N'The sorting order of the learning objective.',                                                                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationLearningObjective', @level2name=N'pkcEngagementPresentationLearningObjective',                       @value=N'Defines the primary key for the EngagementPresentationLearningObjective table using the EngagementPresentationLearningObjectiveId column.',                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationLearningObjective', @level2name=N'fkEngagementPresentationLearningObjective_EngagementPresentation', @value=N'Defines the relationship between the EngagementPresentationLearningObjective and EngagementPresentation tables using the EngagementPresentationId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO