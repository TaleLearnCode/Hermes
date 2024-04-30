CREATE TABLE dbo.PresentationText
(
  PresentationTextId     INT            NOT NULL IDENTITY(1,1),
  PresentationId         INT            NOT NULL,
  LanguageCode           CHAR(2)        NOT NULL,
  PresentationTitle      NVARCHAR(300)  NOT NULL,
  PresentationShortTitle NVARCHAR(60)       NULL,
  Abstract               NVARCHAR(3000)     NULL,
  ShortAbstract          NVARCHAR(2000)     NULL,
  ElevatorPitch          NVARCHAR(160)      NULL,
  AdditionalDetails      NVARCHAR(3000)     NULL,
  CONSTRAINT pkcPresentationText PRIMARY KEY CLUSTERED (PresentationTextId),
  CONSTRAINT fkPresentationText_Presentation FOREIGN KEY (PresentationId) REFERENCES dbo.Presentation (PresentationId),
  CONSTRAINT fkPresentationText_Language FOREIGN KEY (LanguageCode) REFERENCES dbo.Language (LanguageCode),
  CONSTRAINT unqPresentationText_PresentationId_LanguageCode UNIQUE (PresentationId, LanguageCode)
)
GO

EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText',                                                                 @value=N'The text for a presentation details in a specific language.',                                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText', @level2name=N'PresentationTextId',                              @value=N'The identifier of the presentation text record.',                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText', @level2name=N'PresentationId',                                  @value=N'The identifier of the associated presentation.',                                                                         @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText', @level2name=N'PresentationTitle',                               @value=N'The full title of the presentation.',                                                                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText', @level2name=N'PresentationShortTitle',                          @value=N'The short title of the presentation.',                                                                               @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText', @level2name=N'Abstract',                                        @value=N'The full abstract for the presentation.',                                                                            @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText', @level2name=N'ShortAbstract',                                   @value=N'The short abstract for the presentation.',                                                                           @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText', @level2name=N'ElevatorPitch',                                   @value=N'The summary for the presentation.',                                                                                  @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'COLUMN';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText', @level2name=N'pkcPresentationText',                             @value=N'Defines the primary key for the PresentationText table using the PresentationTextId column.',                                @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText', @level2name=N'fkPresentationText_Presentation',                 @value=N'Defines the relationship between the PresentationText and Presentation tables using the PresentationId column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText', @level2name=N'fkPresentationText_Language',                     @value=N'Defines the relationship between the PresentationText and Language tables using the LanguageCode column.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO
EXEC sp_addextendedproperty @level0name=N'dbo', @level1name=N'PresentationText', @level2name=N'unqPresentationText_PresentationId_LanguageCode', @value=N'Defines a unique constraint for the table based upon the PresentationId and LanguageCode columns.', @name=N'MS_Description', @level0type=N'SCHEMA', @level1type=N'TABLE', @level2type=N'CONSTRAINT';
GO