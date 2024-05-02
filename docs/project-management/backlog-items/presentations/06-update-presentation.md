## Update Presentation

* As a Hermes user, I want to update a presentation in the system by uploading a JSON document containing the updated details or by sending the data field-by-field through interactive prompts. When uploading a JSON document, the system should confirm the fields being changed with the user to ensure accuracy and prevent unintended modifications.


#### Command Line Arguments

```bash
$ hermes presentations update [--permalink {presentation-permalink}] [--input {add-presentation.json}] [--output {saved-presentation.json}]
```


#### Acceptance Criteria

- If the user specifies the `--input` flag followed by a file name, the CLI will attempt to update the presentation based on the specified JSON document.

- If the user specifies the `--permalink` flag followed by a presentation permalink, the CLI will prompt the user for the specified presentation field by field.
- If the user specifies the `--permalink` flag followed by a presentation permalink and the permalink does not match any of the presentations, the CLI will display the appropriate error.
- If the user specifies both the `--input` flag followed by a file name and the `---permalink` flag followed by a presentation permalink, the system will display the appropriate message if the permalink does not match the data within the JSON document.
- After collecting the updated presentation information, the CLI should display the changes to be made and prompt the user to confirm before proceeding with the update.
- If the user confirms the changes, the CLI should update the presentation record in the system and provide a success message.
- If the provided data is invalid or incomplete, the CLI should display appropriate error messages and allow the user to correct the input.
- If the user specifies the `--output` flag followed by a file name, the CLI should output the saved presentation to the specified JSON document.
- If the user specifies the `--output` flag followed by a file name and the specified file already exists, the CLI will confirm with the user whether they want to overwrite it.

#### Impact Personas

* [Alex - The Aspiring Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/alex-the-aspiring-speaker.md)
* [Emily - The Social Media Savvy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/emily-the-social-media-savvy-speaker.md)
* [Marcus - The Personal Brand Builder](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/marcus-the-personal-brand-builder.md)
* [Ryan - The Analytics Enthusiast](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/ryan-the-analytics-enthusiast.md)
* [Sarah - The Busy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/sara-the-busy-speaker.md)