## Get a List of Presentations

As a Hermes user, I want to get the list of presentations. The list can be displayed either in the console or outputted to a JSON file. Additionally, I like the ability to filter the list of presentations by presentation status and/or presentation type. This will allow users to view and manage presentation data according to their preferences and specific needs.

#### Command Line Arguments

```bash
$ hermes presentations list [--format {console|JSON}] [--output {presentation-types.json}] [--status {draft|active|retired|abandoned}] [--type {presentation-type-name}]
```



#### Acceptance Criteria

- If the user does not include the `--format JSON` command, the list of presentations will be displayed within the console


- If the user includes the `--format JSON` command, the list of presentations will be saved as a JSON document.
  - If the user includes the `--output` flag, the JSON document with the list of presentations will be saved using the specified file name.
  - If the user includes the `--output` flag and the JSON document already exists, the CLI will prompt the user to overwrite the file.
- The list should include relevant details for each presentation, such as title, type, status, whether the presentation has been archived, and whether the presentation is included in the public profile.
- The list should be in a logic order, such as name.
- If there are no presentations to display, the CLI should provide a message indicating that no presentations are currently managed.
- The user should be able to optionally filter the list of presentations by presentation status using the `--status` flag followed by a valid status (e.g. "draft", "active").

#### Impact Personas

* [Alex - The Aspiring Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/alex-the-aspiring-speaker.md)
* [Emily - The Social Media Savvy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/emily-the-social-media-savvy-speaker.md)
* [Marcus - The Personal Brand Builder](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/marcus-the-personal-brand-builder.md)
* [Ryan - The Analytics Enthusiast](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/ryan-the-analytics-enthusiast.md)
* [Sarah - The Busy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/sara-the-busy-speaker.md)