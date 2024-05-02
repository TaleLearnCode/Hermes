## Retrieve a List of Call for Speaker Submission Statuses

As a Hermes user, I want to retrieve the list of call for speaker submission statuses. The list can be displayed either in the console or outputted to a JSON file.

#### Command Line Arguments

```bash
$ hermes submissions statuses [--format {console|JSON}] [--output {submissions-status.json}]
```



#### Acceptance Criteria

- When the user runs the command with no arguments, the CLI should display the list of all call for speaker submission statuses within the console.

- The list should be sorted by the call for speaker submission status sort order value.

- If there are no call for speaker submission status to display, the CLI should provide a message indicating that there are no configured call for speaker submission statuses.

- If the user include the "--format json" (case insensitive) option, the list of call for speaker submissionstatus will be saved to a JSON file.

  - If the `--output` option is not specified, the call for speaker statuses will be saved to the `submissions-statuses.json` file with the current directory.
  - If the `--output` option is specified and the file name is invalid, the CLI shall display the appropriate error message.
  - If the `--output` option is specified and the file already exists, the CLI shall prompt the user if they want to override the file.

  

#### Impact Personas

* [Alex - The Aspiring Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/alex-the-aspiring-speaker.md)
* [Emily - The Social Media Savvy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/emily-the-social-media-savvy-speaker.md)
* [Marcus - The Personal Brand Builder](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/marcus-the-personal-brand-builder.md)
* [Ryan - The Analytics Enthusiast](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/ryan-the-analytics-enthusiast.md)
* [Sarah - The Busy Speaker](https://github.com/TaleLearnCode/Hermes/blob/develop/docs/project-management/personas/sara-the-busy-speaker.md)