CREATE TABLE dbo.EngagementType
(
  EngagementTypeId     INT           NOT NULL,
  EngagementTypeName   NVARCHAR(100) NOT NULL,
  TypeDescription      NVARCHAR(500)     NULL,
  SortOrder            INT           NOT NULL,
  IsEnabled            BIT           NOT NULL,
  CONSTRAINT pkcEngagementType PRIMARY KEY CLUSTERED (EngagementTypeId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementType',                                      @value=N'Represents a type of an engagement.',                                                        @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementType', @level2name=N'EngagementTypeId',     @value=N'The identifier of the engagement type record.',                                             @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementType', @level2name=N'EngagementTypeName',   @value=N'The name of the engagement type.',                                                          @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementType', @level2name=N'SortOrder',            @value=N'The sorting order of the engagement type.',                                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementType', @level2name=N'TypeDescription',      @value=N'A description of the engagement type.',                                                     @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementType', @level2name=N'IsEnabled',            @value=N'Flag indicating whether the engagement type is enabled.',                                   @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'EngagementType', @level2name=N'pkcEngagementType',    @value=N'Defines the primary key for the EngagementType table using the EngagementTypeId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO