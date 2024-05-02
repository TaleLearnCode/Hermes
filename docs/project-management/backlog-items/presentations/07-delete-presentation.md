## Remove Presentation

As a Hermes user, I want to be able to delete a presentation from the system either by specifying its permalink or by allowing the system to prompt me to delete it. Before removing the presentation, the system should confirm with the user the details to be deleted to prevent accidental deletions.



#### Command Line Arguments

```bash
$ hermes presentations remove [--permalink {presentation-permalink}]
```



#### Acceptance Criteria

- If the user includes the `-- permalink` flag followed by a presentation permalink, the CLI will display the appropriate error message if that permalink cannot be found.

- If the user does not include the `--permalink` flag, the CLI shall prompt the user which presentation they want to delete.
- The CLI will prompt the user to confirm the deletion by displaying the presentation details to be deleted.
- If the user confirms the deletion, the CLI should remove the presentation from the system and provide a success message.
- If the user cancels the deletion confirmation, the CLI should abort the removal process and provide a message indicating the cancellation.



#### Impact Personas

* [Alex - The Aspiring Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/alex-the-aspiring-speaker.md)
* [Emily - The Social Media Savvy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/emily-the-social-media-savvy-speaker.md)
* [Marcus - The Personal Brand Builder](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/marcus-the-personal-brand-builder.md)
* [Ryan - The Analytics Enthusiast](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/ryan-the-analytics-enthusiast.md)
* [Sarah - The Busy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/sara-the-busy-speaker.md)