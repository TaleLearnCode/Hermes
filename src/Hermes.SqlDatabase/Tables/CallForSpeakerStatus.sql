CREATE TABLE dbo.CallForSpeakerStatus
(
  CallForSpeakerStatusId   INT          NOT NULL,
  CallForSpeakerStatusName NVARCHAR(50) NOT NULL,
  SortOrder                INT          NOT NULL,
  IsEnabled                BIT          NOT NULL,
  IsDefault                BIT          NOT NULL,
  CONSTRAINT pkcCallForSpeakerStatus PRIMARY KEY (CallForSpeakerStatusId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeakerStatus',                                          @value=N'Represents a status of a call for speakers.',                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeakerStatus', @level2name=N'CallForSpeakerStatusId',   @value=N'The identifier of the call for speakers status record.',                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeakerStatus', @level2name=N'CallForSpeakerStatusName', @value=N'The name of the call for speakers status.',                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeakerStatus', @level2name=N'IsEnabled',                @value=N'Indicates whether the call for speakers status is enabled.',                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeakerStatus', @level2name=N'SortOrder',                @value=N'The order in which the call for speakers status should be displayed.',                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeakerStatus', @level2name=N'IsDefault',                @value=N'Indicates whether the call for speakers status is the default status.',                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'CallForSpeakerStatus', @level2name=N'pkcCallForSpeakerStatus',  @value=N'Defines the primary key for the CallForSpeakerStatus table using the CallForSpeakerStatusId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO