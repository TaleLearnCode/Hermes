CREATE TABLE dbo.EngagementPresentationDownload
(
  EngagementPresentationDownloadId INT         NOT NULL IDENTITY(1,1),
  EngagementPresentationId         INT          NOT NULL,
  DownloadTypeId                   INT          NOT NULL,
  DownloadName                     NVARCHAR(50) NOT NULL,
  DownloadPath                     VARCHAR(200)     NULL,
  IsEnabled                        BIT          NOT NULL CONSTRAINT dfEngagementPresentationDownload_IsEnabled DEFAULT(1),
  CONSTRAINT pkcEngagementPresentationDownload PRIMARY KEY CLUSTERED (EngagementPresentationDownloadId),
  CONSTRAINT fk_EngagementPresentationId FOREIGN KEY (EngagementPresentationId) REFERENCES dbo.EngagementPresentation (EngagementPresentationId),
  CONSTRAINT fk_DownloadTypeId FOREIGN KEY (DownloadTypeId) REFERENCES dbo.DownloadType (DownloadTypeId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'EngagementPresentationDownloadId',   @value=N'The identifier of the engagement presentation download record.',                                                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'EngagementPresentationId',           @value=N'The identifier of the engagement presentation record.',                                                                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'DownloadTypeId',                     @value=N'The identifier of the download type.',                                                                                                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'DownloadName',                       @value=N'The name of the download.',                                                                                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'DownloadPath',                       @value=N'The path to the download.',                                                                                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'IsEnabled',                          @value=N'Flag indicating whether the download is enabled.',                                                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'pkcEngagementPresentationDownload',  @value=N'Defines the primary key for the EngagementPresentationDownload table using the EngagementPresentationDownloadId column.',                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'fk_EngagementPresentationId',        @value=N'Foreign key constraint ensuring that the EngagementPresentationId column references a valid record in the EngagementPresentation table.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementPresentationDownload', @level2name=N'fk_DownloadTypeId',                  @value=N'Foreign key constraint ensuring that the DownloadTypeId column references a valid record in the DownloadType table.',                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO