CREATE TABLE dbo.Tag
(
  TagId     INT           NOT NULL IDENTITY(1,1),
  TagName   NVARCHAR(100) NOT NULL,
  IsEnabled BIT           NOT NULL CONSTRAINT dfTag_IsEnabled DEFAULT(1),
  CONSTRAINT pkcTag PRIMARY KEY CLUSTERED (TagId)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Tag',                           @value=N'Represents a label attached to a presentation.',                    @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Tag', @level2name=N'TagId',     @value=N'The identifier of the tag record.',                                 @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Tag', @level2name=N'TagName',   @value=N'The name of the tag.',                                              @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Tag', @level2name=N'IsEnabled', @value=N'Flag indicating whether the tag is enabled.',                       @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'Tag', @level2name=N'pkcTag',    @value=N'Defines the primary key for the Tag table using the TagId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO