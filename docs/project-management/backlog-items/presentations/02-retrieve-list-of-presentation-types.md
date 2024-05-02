## Retrieve a List of Presentation Types

As a Hermes user, I want to retrieve the list of presentation types. The list can be displayed either in the console or outputted to a JSON file.

#### Command Line Arguments

```bash
$ hermes presentations types [--format {console|JSON}] [--output {presentation-types.json}]
```



#### Acceptance Criteria

- When the user runs the command with no arguments, the CLI should display the list of all presentations types within the console.

- The list should be sorted by the presentation type sort order value.

- If there are no presentation type to display, the CLI should provide a message indicating that there are no configured presentation types.

- If the user include the "--format json" (case insensitive) option, the list of presentations types will be saved to a JSON file.

  - If the `--output` option is not specified, the presentation types will be saved to the `presentation-types.json` file with the current directory.
  - If the `--output` option is specified and the file name is invalid, the CLI shall display the appropriate error message.
  - If the `--output` option is specified and the file already exists, the CLI shall prompt the user if they want to override the file.

  

#### Impact Personas

* [Alex - The Aspiring Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/alex-the-aspiring-speaker.md)
* [Emily - The Social Media Savvy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/emily-the-social-media-savvy-speaker.md)
* [Marcus - The Personal Brand Builder](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/marcus-the-personal-brand-builder.md)
* [Ryan - The Analytics Enthusiast](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/ryan-the-analytics-enthusiast.md)
* [Sarah - The Busy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/sara-the-busy-speaker.md)