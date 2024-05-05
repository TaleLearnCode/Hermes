MERGE dbo.CallForSpeakerStatus AS TARGET
USING (VALUES ( 1, 1, 1, 'Pending'),
              ( 2, 1, 2, 'Open'),
              ( 3, 1, 3, 'In Evaluation'),
              ( 4, 1, 4, 'Closed'))
AS SOURCE (CallForSpeakerStatusId,
           IsEnabled,
           SortOrder,
           CallForSpeakerStatusName)
ON TARGET.CallForSpeakerStatusId = SOURCE.CallForSpeakerStatusId
WHEN MATCHED THEN UPDATE SET TARGET.CallForSpeakerStatusName = SOURCE.CallForSpeakerStatusName,
                             TARGET.IsEnabled                = SOURCE.IsEnabled,
                             TARGET.SortOrder                = SOURCE.SortOrder
WHEN NOT MATCHED THEN INSERT (CallForSpeakerStatusId,
                              CallForSpeakerStatusName,
                              IsEnabled,
                              SortOrder)
                      VALUES (SOURCE.CallForSpeakerStatusId,
                              SOURCE.CallForSpeakerStatusName,
                              SOURCE.IsEnabled,
                              SOURCE.SortOrder)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO