MERGE dbo.DownloadType AS TARGET
USING (VALUES ( 1, 1, 1, 'Slides',    'The slides used during the presentation.'),
              ( 2, 2, 1, 'Code Demos','Sample code and scripts that were demonstrated or referenced during the presentation.'),
              ( 3, 3, 1, 'Video',     'Video recording of the presentation.'),
              ( 4, 4, 1, 'Audio',     'Audio recording of the presentation..'),
              ( 5, 5, 1, 'Templates', 'Pre-formatted documents or tools that attendees can use to apply concepts from the presentation.'),
              ( 6, 6, 1, 'Handout',   'Additional documents that complement the presentation.'),
              ( 7, 7, 1, 'Other',     'Any other type of download that does not fit into the predefined categories.'))
AS SOURCE (DownloadTypeId,
           SortOrder,
           IsEnabled,
           DownloadTypeName,
           TypeDescription)
ON TARGET.DownloadTypeId = SOURCE.DownloadTypeId
WHEN MATCHED THEN UPDATE SET TARGET.DownloadTypeName = SOURCE.DownloadTypeName,
                             TARGET.TypeDescription    = SOURCE.TypeDescription,
                             TARGET.SortOrder          = SOURCE.SortOrder,
                              TARGET.IsEnabled         = SOURCE.IsEnabled
WHEN NOT MATCHED THEN INSERT (DownloadTypeId,
                              DownloadTypeName,
                              TypeDescription,
                              SortOrder,
                              IsEnabled)
                      VALUES (SOURCE.DownloadTypeId,
                              SOURCE.DownloadTypeName,
                              SOURCE.TypeDescription,
                              SOURCE.SortOrder,
                              SOURCE.IsEnabled)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO