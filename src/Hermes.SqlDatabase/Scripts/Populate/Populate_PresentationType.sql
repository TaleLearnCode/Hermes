SET IDENTITY_INSERT dbo.PresentationType ON
GO

MERGE dbo.PresentationType AS TARGET
USING (VALUES ( 1,  1, 1, 'Session',               'In-depth presentation on a specific subject. Allows for detailed exploration and discussion.'),
              ( 2,  2, 1, 'Workshop',              'Interactive session with hands-on activities. Participants engage in practical exercises. Facilitator guides the session.'),
              ( 3,  3, 1, 'Keynote',               'Usually the main presentation at an event. Sets the ttone for the entire event. Delivereed by a prominent speaker.'),
              ( 4,  4, 1, 'Panel Discussion',      'Involves multiple speakers discussing a speciufic topic. Each panelist shares their perspective. Often includes audience Q&A.'),
              ( 5,  5, 1, 'Training Session',      'Educational presentation to teach specific skills. Focuses on practical knowledge and application.'),
              ( 6,  6, 1, 'Ignite Talk',           'Short, fast-paced presentations (usually 5 minutes). Each slide is automatically advanced after a fixed time.'),
              ( 7,  7, 1, 'Lightening Talk',       'Brief, concise presentation (typically 5-10 minutes). Covers a specific topic or idea.'),
              ( 8,  8, 1, 'TED-style Talk',        'Emphasizes storytelling and engaging delivery. Focuses on sharing innovative ideas.'),
              (09,  9, 1, 'Product Demo',          'Showcases a product or service. Demonstrates features and benefits.'),
              (10, 10, 1, 'Roundtable Discussion', 'Informal discussion among participants. Encourages open dialogue and idea sharing.'),
              (11, 11, 1, 'Poster Presentation',   'Visual presentation displayed on a poster board. Presenter discusses the content with attendees.'),
              (12, 12, 1, 'Symposium',             'Conference or meeting for the discussion of a specific topic. Multiple presentations and discussions.'),
              (13, 13, 1, 'Fireside Chat',         'Informal conversation between a moderator and a speaker. Often takes place in a related setting.'),
              (14, 14, 1, 'Town Hall Meeting',     'Gathering where leaders address and interact with a community. Provides updates and allows for Q&A.'))
AS SOURCE (PresentationTypeId,
           SortOrder,
           IsEnabled,
           PresentationTypeName,
           TypeDescription)
ON TARGET.PresentationTypeId = SOURCE.PresentationTypeId
WHEN MATCHED THEN
    UPDATE SET PresentationTypeName = SOURCE.PresentationTypeName,
               TypeDescription      = SOURCE.TypeDescription,
               SortOrder            = SOURCE.SortOrder,
               IsEnabled            = SOURCE.IsEnabled
WHEN NOT MATCHED BY TARGET THEN INSERT (PresentationTypeId,
                                        PresentationTypeName,
                                        TypeDescription,
                                        SortOrder,
                                        IsEnabled)
                                VALUES (SOURCE.PresentationTypeId,
                                        SOURCE.PresentationTypeName,
                                        SOURCE.TypeDescription,
                                        SOURCE.SortOrder,
                                        SOURCE.IsEnabled)
WHEN NOT MATCHED BY SOURCE THEN DELETE;
GO

SET IDENTITY_INSERT dbo.PresentationType OFF
GO