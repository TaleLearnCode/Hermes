CREATE TABLE dbo.CallForSpeaker
(
  CallForSpeakerId            INT           NOT NULL IDENTITY(1,1),
  CallForSpeakerStatusId      INT           NOT NULL,
  EventName                   NVARCHAR(200) NOT NULL,
  EventUrl                    NVARCHAR(200)     NULL,
  EventStartDate              DATE          NOT NULL,
  EventEndDate                DATE          NOT NULL,
  EventLocation               NVARCHAR(300) NOT NULL,
  EventCity                   NVARCHAR(100) NOT NULL,
  EventCountryCode            CHAR(2)       NOT NULL,
  EventCountryDivisionCode    CHAR(3)           NULL,
  EventTimeZoneId             VARCHAR(100)      NULL,
  CallForSpeakerUrl           NVARCHAR(200) NOT NULL,
  CallForSpeakerStartDate     DATE          NOT NULL,
  CallForSpeakerEndDate       DATE          NOT NULL,
  SpeakerHonorarium           BIT           NOT NULL CONSTRAINT dfCallForSpeaker_SpeakerHonorarium DEFAULT 0,
  SpeakerHonorariumAmount     DECIMAL(10,2)     NULL,
  SpeakerHonorariumCurrency   CHAR(3)           NULL,
  SpeakerHonorariumNotes      NVARCHAR(200)     NULL,
  TravelExpensesCovered       BIT           NOT NULL CONSTRAINT dfCallForSpeaker_TravelExpensesCovered DEFAULT 0,
  TravelNotes                 NVARCHAR(200)     NULL,
  AccomodationExpensesCovered BIT           NOT NULL CONSTRAINT dfCallForSpeakers_AccomodationExpensesCovered DEFAULT 0,
  AccomodationNotes           NVARCHAR(200)     NULL,
  EventFeeCovered             BIT           NOT NULL CONSTRAINT dfCallForSpeakers_EventFeeCovered DEFAULT 1,
  EventFeeNotes               NVARCHAR(200)     NULL,
  SubmissionLimit             INT               NULL,
  CONSTRAINT pkcCallForSpeaker PRIMARY KEY CLUSTERED (CallForSpeakerId),
  CONSTRAINT fkCallForSpeaker_CallForSpeakerStatus FOREIGN KEY (CallForSpeakerStatusId) REFERENCES dbo.CallForSpeakerStatus(CallForSpeakerStatusId),
  CONSTRAINT fkCallForSpeaker_TimeZone FOREIGN KEY (EventTimeZoneId) REFERENCES dbo.TimeZone(TimeZoneId),
  CONSTRAINT fkCallForSpeaker_Country FOREIGN KEY (EventCountryCode) REFERENCES dbo.Country(CountryCode),
  CONSTRAINT fkCallForSpeaker_CountryDivision FOREIGN KEY (EventCountryCode, EventCountryDivisionCode) REFERENCES dbo.CountryDivision(CountryCode, CountryDivisionCode)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'CallForSpeakerId',                              @value=N'The identifier of the call for speaker.',                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'CallForSpeakerStatusId',                        @value=N'The identifier of the call for speaker status.',                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'EventName',                                     @value=N'The name of the event.',                                                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'EventUrl',                                      @value=N'The URL of the event.',                                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'EventStartDate',                                @value=N'The start date of the event.',                                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'EventEndDate',                                  @value=N'The end date of the event.',                                                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'EventLocation',                                 @value=N'The location of the event.',                                                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'EventCity',                                     @value=N'The city where the event is located.',                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'EventCountryCode',                              @value=N'The ISO 3166-1 alpha-2 country code where the event is located.',                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'EventCountryDivisionCode',                      @value=N'The ISO 3166-2 alpha-3 country division code where the event is located.',                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'EventTimeZoneId',                               @value=N'The identifier of the time zone where the event is located.',                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'CallForSpeakerUrl',                             @value=N'The URL of the call for speaker.',                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'CallForSpeakerStartDate',                       @value=N'The start date of the call for speaker.',                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'CallForSpeakerEndDate',                         @value=N'The end date of the call for speaker.',                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'SpeakerHonorarium',                             @value=N'Indicates if the speaker will receive a honorarium.',                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'SpeakerHonorariumAmount',                       @value=N'The amount of the honorarium.',                                                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'SpeakerHonorariumCurrency',                     @value=N'The currency of the honorarium.',                                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'SpeakerHonorariumNotes',                        @value=N'Additional notes about the honorarium.',                                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'TravelExpensesCovered',                         @value=N'Indicates if the event will cover the travel expenses of the speaker.',                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'TravelNotes',                                   @value=N'Additional notes about the travel expenses.',                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'AccomodationExpensesCovered',                   @value=N'Indicates if the event will cover the accomodation expenses of the speaker.',                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'AccomodationNotes',                             @value=N'Additional notes about the accomodation expenses.',                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'EventFeeCovered',                               @value=N'Indicates if the event will cover the fee of the speaker.',                                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'EventFeeNotes',                                 @value=N'Additional notes about the event fee.',                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'SubmissionLimit',                               @value=N'The maximum number of submissions allowed.',                                                      @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'pkcCallForSpeaker',                             @value=N'Defines the primary key for the CallForSpeaker table using the CallForSpeakerId column.',         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'fkCallForSpeaker_CallForSpeakerStatus',           @value=N'Defines the foreign key for the CallForSpeaker table using the CallForSpeakerStatusId column.',    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'fkCallForSpeaker_TimeZone',                     @value=N'Defines the foreign key for the CallForSpeaker table using the EventTimeZoneId column.',          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'fkCallForSpeaker_Country',                      @value=N'Defines the foreign key for the CallForSpeaker table using the EventCountryCode column.',         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'fkCallForSpeaker_CountryDivision',              @value=N'Defines the foreign key for the CallForSpeaker table using the EventCountryDivisionCode column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'dfCallForSpeaker_SpeakerHonorarium',            @value=N'Defines the default value for the SpeakerHonorarium column as 0 (false).',                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'dfCallForSpeaker_TravelExpensesCovered',        @value=N'Defines the default value for the TravelExpensesCovered column as 0 (false).',                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'dfCallForSpeakers_AccomodationExpensesCovered', @value=N'Defines the default value for the AccomodationExpensesCovered column as 0 (false).',              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeaker', @level2name=N'dfCallForSpeakers_EventFeeCovered',             @value=N'Defines the default value for the EventFeeCovered column as 1 (true).',                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO