CREATE TABLE dbo.EngagementCallForSpeakerStatus
(
  EngagementCallForSpeakerStatusId   INT           NOT NULL,
  EngagementCallForSpeakerStatusName NVARCHAR(50)  NOT NULL,
  StatusDescription                  NVARCHAR(500)     NULL,
  SortOrder                          INT           NOT NULL,
  IsEnabled                          BIT           NOT NULL,
  IsDefault                          BIT           NOT NULL,
  CONSTRAINT pkcEngagementCallForSpeakerStatus PRIMARY KEY CLUSTERED (EngagementCallForSpeakerStatusId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpeakerStatus',                                          @value=N'Represents a status of an engagement call for speakers.',                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpeakerStatus', @level2name=N'EngagementCallForSpeakerStatusId',   @value=N'The identifier of the engagement call for speakers status record.',                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpeakerStatus', @level2name=N'EngagementCallForSpeakerStatusName', @value=N'The name of the engagement call for speakers status.',                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpeakerStatus', @level2name=N'IsEnabled',                @value=N'Indicates whether the engagement call for speakers status is enabled.',                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpeakerStatus', @level2name=N'SortOrder',                @value=N'The order in which the engagement call for speakers status should be displayed.',                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpeakerStatus', @level2name=N'IsDefault',                @value=N'Indicates whether the engagement call for speakers status is the default status.',                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementCallForSpeakerStatus', @level2name=N'pkcEngagementCallForSpeakerStatus',  @value=N'Defines the primary key for the EngagementCallForSpeakerStatus table using the EngagementCallForSpeakerStatusId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO