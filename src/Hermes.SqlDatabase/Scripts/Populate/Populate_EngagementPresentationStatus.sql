MERGE dbo.EngagementPresentationStatus AS TARGET
USING (VALUES
              ( 1, 0,  1, 1, 'Submitted',          'The presentation has been submitted to the engagement and is awaiting acceptance.'),
              ( 2, 0,  2, 1, 'Accepted',           'The presentation propsoal has been accepted by the engagement.'),
              ( 3, 1,  3, 1, 'Confirmed',          'The speaker has confirmed that they will present the presentation at the engagment.'),
              ( 4, 1,  4, 1, 'Scheduled',          'The presentation has been scheduled for engagement.'),
              ( 5, 1,  5, 1, 'Completed',          'The engagement presentation has been completed.'),
              ( 6, 0,  6, 1, 'Not Selected',       'The presentation proposal was not selected for the engagement.'),
              ( 7, 0,  7, 1, 'Declined',           'The speaker has declined the invitation to present the presetnation at the engagement.'),
              ( 8, 0,  8, 1, 'Waitlisted',         'The presentation proposal has been placed on a waitlist for the engagement.'),
              ( 9, 0,  9, 1, 'Proposal Withdrawn', 'The speaker has withdrawn the presentation proposal at the engagement.'),
              (10, 0, 10, 1, 'Withdrawn',          'The speaker has withdrawn the presentation at the engagement.'),
              (11, 1, 11, 1, 'Cancelled',          'The presentation at the engagement has been cancelled by the organizer.'))
AS SOURCE (EngagementPresentationStatusId,
           IncludeOnWebsite,
           SortOrder,
           IsEnabled,
           EngagementPresentationStatusName,
           StatusDescription)
ON TARGET.EngagementPresentationStatusId = SOURCE.EngagementPresentationStatusId
WHEN MATCHED THEN UPDATE SET TARGET.EngagementPresentationStatusName = SOURCE.EngagementPresentationStatusName,
                             TARGET.StatusDescription    = SOURCE.StatusDescription,
                             TARGET.IncludeOnWebsite     = SOURCE.IncludeOnWebsite,
                             TARGET.SortOrder            = SOURCE.SortOrder,
                             TARGET.IsEnabled            = SOURCE.IsEnabled
WHEN NOT MATCHED THEN INSERT (EngagementPresentationStatusId,
                              EngagementPresentationStatusName,
                              StatusDescription,
                              IncludeOnWebsite,
                              SortOrder,
                              IsEnabled)
                      VALUES (SOURCE.EngagementPresentationStatusId,
                              SOURCE.EngagementPresentationStatusName,
                              SOURCE.StatusDescription,
                              SOURCE.IncludeOnWebsite,
                              SOURCE.SortOrder,
                              SOURCE.IsEnabled)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO