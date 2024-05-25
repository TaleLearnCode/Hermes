CREATE TABLE dbo.Submission
(
  SubmissionId               INT            NOT NULL IDENTITY(1,1),
  CallForSpeakerId           INT            NOT NULL,
  SubmissionStatusId         INT            NOT NULL,
  SubmissionDate             DATE           NOT NULL,
  DecisionDate               DATE               NULL,
  PresentationId             INT            NOT NULL,
  SubmissionLanguageCode     CHAR(2)        NOT NULL,
  SessionTitle               NVARCHAR(300)  NOT NULL,
  SessionDescription         NVARCHAR(3000) NOT NULL,
  SessionLength              INT            NOT NULL,
  SessionTrack               NVARCHAR(100)      NULL,
  SessionLevel               NVARCHAR(100)      NULL,
  ElevatorPitch              NVARCHAR(300)      NULL,
  AdditionalDetails          NVARCHAR(3000)     NULL,
  CONSTRAINT pkcSubmission PRIMARY KEY CLUSTERED (SubmissionId),
  CONSTRAINT fkSubmission_SubmissionStatus FOREIGN KEY (SubmissionStatusId) REFERENCES dbo.SubmissionStatus (SubmissionStatusId),
  CONSTRAINT fkSubmission_CallForSpeaker FOREIGN KEY (CallForSpeakerId) REFERENCES dbo.CallForSpeaker (CallForSpeakerId),
  CONSTRAINT fkSubmission_Presentation   FOREIGN KEY (PresentationId)   REFERENCES dbo.Presentation (PresentationId),
)
GO
 
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission',                                               @value=N'Represents a session submission to a call for speakers.',                                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'SubmissionId',                  @value=N'Identifier of the Submission record.',                                                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'CallForSpeakerId',              @value=N'Identifier of the associated call for speakers.',                                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'SubmissionStatusId',            @value=N'Identifier of the status for the submission.',                                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'SubmissionDate',                @value=N'The date of the submission.',                                                                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'PresentationId',                @value=N'Identifier of the associated presentation.',                                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'SessionTitle',                  @value=N'The title of the submitted session.',                                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'SessionDescription',            @value=N'The description of the submitted session.',                                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'SessionLength',                 @value=N'The length of the submitted session (in minutes).',                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'SessionTrack',                  @value=N'The track the session was submitted under.',                                                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'SessionLevel',                  @value=N'The level of the submitted session.',                                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'ElevatorPitch',                 @value=N'The elevator pitch for the session.',                                                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'AdditionalDetails',             @value=N'Any additional details about the session submission to the event organizers.',                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'DecisionDate',                  @value=N'The date the event provided a decision about the submission.',                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'pkcSubmission',                 @value=N'Defines the primary key for the Submission table using the SubmissionId column.',                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'fkSubmission_SubmissionStatus', @value=N'Defines the relationship between the Submission and SubmissionStatus tables using the SubmissionStatusId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'fkSubmission_CallForSpeaker',   @value=N'Defines the relationship between the Submission and CallForSpeaker tables using the CallForSpeakerId column.',     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Submission', @level2name=N'fkSubmission_Presentation',     @value=N'Defines the relationship between the Submission and Presentation tables using the PresentationId column.',         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO