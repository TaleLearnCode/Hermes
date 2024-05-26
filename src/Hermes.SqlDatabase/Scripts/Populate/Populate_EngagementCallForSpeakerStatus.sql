MERGE dbo.EngagementCallForSpeakerStatus AS TARGET
USING (VALUES ( 1, 1, 1, 0, 'Pending'),
              ( 2, 1, 2, 1, 'Open'),
              ( 3, 1, 3, 0, 'Under Review'),
              ( 4, 1, 4, 0, 'Dormant'),
              ( 5, 1, 5, 0, 'Closed'))
AS SOURCE (EngagementCallForSpeakerStatusId,
           IsEnabled,
           SortOrder,
           IsDefault,
           EngagementCallForSpeakerStatusName)
ON TARGET.EngagementCallForSpeakerStatusId = SOURCE.EngagementCallForSpeakerStatusId
WHEN MATCHED THEN UPDATE SET TARGET.EngagementCallForSpeakerStatusName = SOURCE.EngagementCallForSpeakerStatusName,
                             TARGET.IsEnabled                = SOURCE.IsEnabled,
                             TARGET.SortOrder                = SOURCE.SortOrder,
                             TARGET.IsDefault                = SOURCE.IsDefault
WHEN NOT MATCHED THEN INSERT (EngagementCallForSpeakerStatusId,
                              EngagementCallForSpeakerStatusName,
                              IsEnabled,
                              SortOrder,
                              IsDefault)
                      VALUES (SOURCE.EngagementCallForSpeakerStatusId,
                              SOURCE.EngagementCallForSpeakerStatusName,
                              SOURCE.IsEnabled,
                              SOURCE.SortOrder,
                              SOURCE.IsDefault)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO