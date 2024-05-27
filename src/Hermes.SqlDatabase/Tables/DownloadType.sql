CREATE TABLE dbo.DownloadType
(
  DownloadTypeId   INT           NOT NULL,
  DownloadTypeName NVARCHAR(100) NOT NULL,
  TypeDescription  NVARCHAR(500)     NULL,
  SortOrder        INT           NOT NULL,
  IsEnabled        BIT           NOT NULL
  CONSTRAINT pkcDownloadType PRIMARY KEY CLUSTERED (DownloadTypeId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'DownloadType', @level2name=N'DownloadTypeId',   @value=N'The identifier of the download type record.',                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'DownloadType', @level2name=N'DownloadTypeName', @value=N'The name of the download type.',                                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'DownloadType', @level2name=N'TypeDescription',  @value=N'A description of the download type.',                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'DownloadType', @level2name=N'SortOrder',        @value=N'The sorting order of the download type.',                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'DownloadType', @level2name=N'IsEnabled',        @value=N'Flag indicating whether the download type is enabled.',                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'DownloadType', @level2name=N'pkcDownloadType',  @value=N'Defines the primary key for the DownloadType table using the DownloadTypeId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO