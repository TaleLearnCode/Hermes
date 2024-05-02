## Get Presentation Template

As a Hermes users, I want to get a template JSON document for adding a presentation, which will serve as a guideline for providing necessary information about a new presentation. This will users ensure they include all of the necessary details when adding a new presentation to the system.

#### Command Line Arguments

```bash
$ hermes presentations template [--output {add-presentation.json}]
```



#### Acceptance Criteria

- The template JSON document should include placeholders for the details about the presentation.


- The template should provide clear instructions or descriptions for each field to guide users in providing the correct information.
- If the `--output` option is not specified, the CLI will create a file named `add-presentation.json`.
- If the `--output` option is specified and the file name is invalid, the CLI will display the appropriate error message.
- If the `--output` option is specified and the file already exists, the CLI shall prompt the user if they want to override the file.



#### Impact Personas

* [Alex - The Aspiring Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/alex-the-aspiring-speaker.md)
* [Emily - The Social Media Savvy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/emily-the-social-media-savvy-speaker.md)
* [Marcus - The Personal Brand Builder](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/marcus-the-personal-brand-builder.md)
* [Ryan - The Analytics Enthusiast](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/ryan-the-analytics-enthusiast.md)
* [Sarah - The Busy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/sara-the-busy-speaker.md)