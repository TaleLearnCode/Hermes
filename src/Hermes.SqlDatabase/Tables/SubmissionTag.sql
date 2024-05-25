CREATE TABLE dbo.SubmissionTag
(
  SubmissionTagId INT NOT NULL IDENTITY(1,1),
  SubmissionId    INT NOT NULL,
  TagId           INT NOT NULL,
  CONSTRAINT pkcSubmissionTag PRIMARY KEY (SubmissionTagId),
  CONSTRAINT fkSubmissionTag_Submission FOREIGN KEY (SubmissionId) REFERENCES dbo.Submission (SubmissionId),
  CONSTRAINT fkSubmissionTag_Tag        FOREIGN KEY (TagId)        REFERENCES dbo.Tag (TagId)
)
GO
 
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionTag',                                            @value=N'Associated a tag with a call for speaker submission.',                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionTag', @level2name=N'SubmissionTagId',            @value=N'Identifier of the SubmissionTag record.',                                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionTag', @level2name=N'SubmissionId',               @value=N'Identifier of the associated submission.',                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionTag', @level2name=N'TagId',                      @value=N'Identifier of the associated tag.',                                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionTag', @level2name=N'pkcSubmissionTag',           @value=N'Defines the primary key for the SubmissionTag table using the SubmissionId column.',                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionTag', @level2name=N'fkSubmissionTag_Submission', @value=N'Defines the relationship between the SubmissionTag and Submission tables using the SubmissionId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionTag', @level2name=N'fkSubmissionTag_Tag',        @value=N'Defines the relationship between the SubmissionTag and Tag tables using the TagId column.',               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
