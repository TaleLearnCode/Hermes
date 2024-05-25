CREATE TABLE dbo.SubmissionStatus
(
  SubmissionStatusId   INT           NOT NULL,
  SubmissionStatusName NVARCHAR(100) NOT NULL,
  SortOrder            INT           NOT NULL,
  StatusDescription    NVARCHAR(500)     NULL,
  IsDefault            BIT           NOT NULL,
  IndicatesAcceptance  BIT           NOT NULL,
  IsEnabled            BIT           NOT NULL,
  CONSTRAINT pkcSubmissionStatus PRIMARY KEY CLUSTERED (SubmissionStatusId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionStatus',                                      @value=N'Represents a submission status.',                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionStatus', @level2name=N'SubmissionStatusId',   @value=N'The identifier of the submnission status record.',                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionStatus', @level2name=N'SubmissionStatusName', @value=N'The name of the submission status.',                                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionStatus', @level2name=N'SortOrder',            @value=N'The sorting order of the submission status.',                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionStatus', @level2name=N'StatusDescription',    @value=N'A description of the submission status.',                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionStatus', @level2name=N'IsDefault',            @value=N'Flag indicating whether the submission status is the default status.',                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionStatus', @level2name=N'IndicatesAcceptance',  @value=N'Flag indicating whether the submission status indicates acceptance.',                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionStatus', @level2name=N'IsEnabled',            @value=N'Flag indicating whether the submission status is enabled.',                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'SubmissionStatus', @level2name=N'pkcSubmissionStatus',  @value=N'Defines the primary key for the SubmissionStatus table using the SubmissionStatusId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO