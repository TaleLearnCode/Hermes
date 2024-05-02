MERGE INTO dbo.PresentationStatus AS TARGET
USING (VALUES (1, 0, 1, 1, 'Draft'),
              (2, 0, 1, 2, 'Active'),
              (3, 1, 1, 3, 'Retired'),
              (4, 1, 1, 4, 'Abandoned'))
AS SOURCE (PresentationStatusId,
           PresentationIsArchived,
           IsEnabled,
           SortOrder,
           PresentationStatusName)
ON TARGET.PresentationStatusId = SOURCE.PresentationStatusId
WHEN MATCHED THEN UPDATE SET TARGET.PresentationStatusName = SOURCE.PresentationStatusName,
                             TARGET.PresentationIsArchived = SOURCE.PresentationIsArchived,
                             TARGET.SortOrder              = SOURCE.SortOrder,
                             TARGET.IsEnabled              = SOURCE.IsEnabled
WHEN NOT MATCHED THEN INSERT (PresentationStatusId,
                              PresentationStatusName,
                              PresentationIsArchived,
                              SortOrder,
                              IsEnabled)
                      VALUES (SOURCE.PresentationStatusId,
                              SOURCE.PresentationStatusName,
                              SOURCE.PresentationIsArchived,
                              SOURCE.SortOrder,
                              SOURCE.IsEnabled)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO

SET IDENTITY_INSERT dbo.Presentation OFF
GO