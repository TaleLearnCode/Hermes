MERGE dbo.SubmissionStatus AS TARGET
USING (VALUES ( 1, 1,  1, 0, 0, 'Draft',        'Submissions that speakers are still working on and have not yet finalized for submission.'),
              ( 2, 1,  2, 0, 0, 'Submitted',    'Submissions that have been completed and formally submitted by the speaker for consideration.'),
              ( 3, 1,  3, 1, 0, 'Under Review', 'Submissions that are currently being evaluated by the selection committee.'),
              ( 4, 1,  4, 0, 1, 'Selected',     'Submissions that have been approved for inclusion in the event program.'),
              ( 5, 1,  5, 0, 0, 'Waitlisted',   'Submissions that have potential for inclusion but are on hold pending further review or availability.'),
              ( 6, 1,  6, 0, 1, 'Not Selected', 'Submissions that were not chosen for inclusion in the event program.'),
              ( 7, 1,  7, 0, 0, 'Confirmed',    'Submissions where the speaker has confirmed their participation and details.'),
              ( 8, 1,  8, 0, 1, 'Scheduled',    'Submissions that have been scheduled for presentation at specific times during the event.'),
              ( 9, 1,  9, 0, 1, 'Withdrawn',    'Submissions that the speaker has withdrawn from consideration.'),
              (10, 1, 10, 0, 1, 'Cancelled',    'Submissions that were previously accepted but have been cancelled by either the speaker or the event organizers.'))
AS SOURCE (SubmissionStatusId,
           IsEnabled,
           SortOrder,
           IsDefault,
           IndicatesAcceptance,
           SubmissionStatusName,
           StatusDescription)
ON TARGET.SubmissionStatusId = SOURCE.SubmissionStatusId
WHEN MATCHED THEN UPDATE SET TARGET.SubmissionStatusName = SOURCE.SubmissionStatusName,
                             TARGET.SortOrder            = SOURCE.SortOrder,
                             TARGET.StatusDescription    = SOURCE.StatusDescription,
                             TARGET.IsDefault            = SOURCE.IsDefault,
                             TARGET.IndicatesAcceptance  = SOURCE.IndicatesAcceptance,
                             TARGET.IsEnabled            = SOURCE.IsEnabled
WHEN NOT MATCHED THEN INSERT (SubmissionStatusId,
                              SubmissionStatusName,
                              SortOrder,
                              StatusDescription,
                              IsDefault,
                              IndicatesAcceptance,
                              IsEnabled)
                      VALUES (SOURCE.SubmissionStatusId,
                              SOURCE.SubmissionStatusName,
                              SOURCE.SortOrder,
                              SOURCE.StatusDescription,
                              SOURCE.IsDefault,
                              SOURCE.IndicatesAcceptance,
                              SOURCE.IsEnabled)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO