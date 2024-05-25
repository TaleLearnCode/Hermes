CREATE TABLE dbo.SubmissionLearningObjective
(
  SubmissionLearningObjectiveId INT            NOT NULL IDENTITY(1,1),
  SubmissionId                  INT            NOT NULL,
  LearningObjectiveText         NVARCHAR(1000) NOT NULL,
  CONSTRAINT pkcSubmissionLearningObjective PRIMARY KEY CLUSTERED (SubmissionLearningObjectiveId),
  CONSTRAINT fkSubmissionLearningObjective_Submission FOREIGN KEY (SubmissionId) REFERENCES dbo.Submission (SubmissionId)
)
GO
 
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionLearningObjective',                                                          @value=N'Represents a learning objective that was a part of a submission to a call for speakers..',                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionLearningObjective', @level2name=N'SubmissionLearningObjectiveId',            @value=N'Identifier of the SubmissionLearningObjective record.',                                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionLearningObjective', @level2name=N'SubmissionId',                             @value=N'Identifier of the associated submission record.',                                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionLearningObjective', @level2name=N'LearningObjectiveText',                    @value=N'The text of the submitted learning objective.',                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionLearningObjective', @level2name=N'pkcSubmissionLearningObjective',           @value=N'Defines the primary key for the SubmissionLearningObjective table using the SubmissionLearningObjectiveId column.',     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionLearningObjective', @level2name=N'fkSubmissionLearningObjective_Submission', @value=N'Defines the relationship between the SubmissionLearningObjective and Submission tables using the SubmissionId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
