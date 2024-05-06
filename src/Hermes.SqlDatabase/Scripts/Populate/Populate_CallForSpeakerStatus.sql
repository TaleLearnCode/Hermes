MERGE dbo.CallForSpeakerStatus AS TARGET
USING (VALUES ( 1, 1, 1, 0, 'Pending'),
              ( 2, 1, 2, 1, 'Open'),
              ( 3, 1, 3, 0, 'In Evaluation'),
              ( 4, 1, 4, 0, 'Dormant'),
              ( 5, 1, 5, 0, 'Closed'))
AS SOURCE (CallForSpeakerStatusId,
           IsEnabled,
           SortOrder,
           IsDefault,
           CallForSpeakerStatusName)
ON TARGET.CallForSpeakerStatusId = SOURCE.CallForSpeakerStatusId
WHEN MATCHED THEN UPDATE SET TARGET.CallForSpeakerStatusName = SOURCE.CallForSpeakerStatusName,
                             TARGET.IsEnabled                = SOURCE.IsEnabled,
                             TARGET.SortOrder                = SOURCE.SortOrder,
                             TARGET.IsDefault                = SOURCE.IsDefault
WHEN NOT MATCHED THEN INSERT (CallForSpeakerStatusId,
                              CallForSpeakerStatusName,
                              IsEnabled,
                              SortOrder,
                              IsDefault)
                      VALUES (SOURCE.CallForSpeakerStatusId,
                              SOURCE.CallForSpeakerStatusName,
                              SOURCE.IsEnabled,
                              SOURCE.SortOrder,
                              SOURCE.IsDefault)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO