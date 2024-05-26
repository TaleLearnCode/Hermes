MERGE dbo.EngagementStatus AS TARGET
USING (VALUES
              ( 1,  1, 1, 'Submitted', 'The speaker has submitted to the session and is awaiting approval.'),
              ( 2,  2, 1, 'Accepted',  'The engagmeent has accepted one or more of the speaker''s proposals,'),
              ( 3,  3, 1, 'Confirmed', 'The speaker has confirmed their participation in the engagement.'),
              ( 4,  4, 1, 'Scheduled', 'The engagement has been scheduled.'),
              ( 5,  5, 1, 'Completed', 'The engagement has been completed.'),
              ( 6,  6, 1, 'Not Selected', 'The speaker was not selected to participate in the engagement.'),
              ( 7,  7, 1, 'Declined',  'The speaker has declined the invitation to participate in the engagement.'),
              ( 8,  8, 1, 'Waitlisted','The speaker has been placed on a waitlist for the engagement.'),
              ( 9,  9, 1, 'Withdrawn', 'The speaker has withdrawn from the engagement.'),
              (10, 10, 1, 'Cancelled', 'The engagement has been cancelled by the organizer.'),
              (11, 11, 1, 'Postponed', 'The engagement has been postponed.'))
AS SOURCE (EngagementStatusId,
           SortOrder,
           IsEnabled,
           EngagementStatusName,
           StatusDescription)
ON TARGET.EngagementStatusId = SOURCE.EngagementStatusId
WHEN MATCHED THEN UPDATE SET TARGET.EngagementStatusName = SOURCE.EngagementStatusName,
                             TARGET.SortOrder              = SOURCE.SortOrder,
                             TARGET.IsEnabled              = SOURCE.IsEnabled,
                             TARGET.StatusDescription      = SOURCE.StatusDescription
WHEN NOT MATCHED THEN INSERT (EngagementStatusId,
                              EngagementStatusName,
                              StatusDescription,
                              SortOrder,
                              IsEnabled)
                      VALUES (SOURCE.EngagementStatusId,
                              SOURCE.EngagementStatusName,
                              SOURCE.StatusDescription,
                              SOURCE.SortOrder,
                              SOURCE.IsEnabled)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO