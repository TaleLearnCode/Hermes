CREATE TABLE dbo.EngagementCallForSpekers
(
  EngagementPermalink              VARCHAR(200)   NOT NULL,
  EngagementCallForSpeakerStatusId INT            NOT NULL,
  CallForSpeakersUrl               VARCHAR(200)   NOT NULL,
  StartDate                        DATE           NOT NULL,
  EndDate                          DATE           NOT NULL,
  ExpectedDecisionDate             DATE               NULL,
  ActualDecisionDate               DATE               NULL,
  SpeakerHonorarium                BIT            NOT NULL CONSTRAINT dfEngagementCallForSpekers_SpeakerHonorariumi DEFAULT 0,
  SpeakerHonorariumAmount          DECIMAL(10,2)      NULL,
  SpeakerHonorariumCurrency        CHAR(3)            NULL,
  SpeakerHonorariumNotes           NVARCHAR(200)      NULL,
  TravelExpensesCovered            BIT            NOT NULL CONSTRAINT dfEngagementCallForSpekers_TravelExpensesCovered DEFAULT 0,
  TravelExpensesNotes              NVARCHAR(200)      NULL,
  AccommodationCovered             BIT            NOT NULL CONSTRAINT dfEngagementCallForSpekers_AccommodationCovered DEFAULT 0,
  AccommodationNotes               NVARCHAR(200)      NULL,
  EventFeeCovered                  BIT            NOT NULL CONSTRAINT dfEngagementCallForSpekers_EventFeeCovered DEFAULT 0,
  EventFeeNotes                    NVARCHAR(200)      NULL,
  SubmissionLimit                  INT            NOT NULL CONSTRAINT dfEngagementCallForSpeakers_SubmissionLimit DEFAULT -1,
  CONSTRAINT pkcEngagementCallForSpekers PRIMARY KEY CLUSTERED (EngagementPermalink),
  CONSTRAINT fkEngagementCallForSpekers_Engagement FOREIGN KEY (EngagementPermalink) REFERENCES dbo.Engagement (Permalink)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'EngagementPermalink', @value=N'The permalink of the engagement.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'CallForSpeakersUrl', @value=N'The URL of the call for speakers.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'StartDate', @value=N'The start date of the call for speakers.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'EndDate', @value=N'The end date of the call for speakers.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'ExpectedDecisionDate', @value=N'The expected decision date for the call for speakers.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'ActualDecisionDate', @value=N'The actual decision date for the call for speakers.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'SpeakerHonorarium', @value=N'Indicates if a speaker honorarium is offered.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'SpeakerHonorariumAmount', @value=N'The amount of the speaker honorarium.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'SpeakerHonorariumCurrency', @value=N'The currency of the speaker honorarium.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'SpeakerHonorariumNotes', @value=N'Additional notes about the speaker honorarium.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'TravelExpensesCovered', @value=N'Indicates if travel expenses are covered for speakers.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'TravelExpensesNotes', @value=N'Additional notes about travel expenses for speakers.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'AccommodationCovered', @value=N'Indicates if accommodation is covered for speakers.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'AccommodationNotes', @value=N'Additional notes about accommodation for speakers.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'EventFeeCovered', @value=N'Indicates if event fees are covered for speakers.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'EventFeeNotes', @value=N'Additional notes about event fees for speakers.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpekers', @level2name=N'SubmissionLimit', @value=N'The maximum number of submissions allowed per speaker.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
