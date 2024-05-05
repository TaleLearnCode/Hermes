namespace Hermes.Modules;

internal class CallForSpeakers(string databaseConnectionString) : ModuleBase
{

	private readonly CallForSpeakerServices _callForSpeakerServices = new(databaseConnectionString);
	private readonly FileServices _fileServices = new();

	internal void Initialize(RootCommand rootCommand)
	{
		Command presentationCommand = new("callforspeakers", "Work with the 'Call for Speakers' module.");
		rootCommand.AddCommand(presentationCommand);

		InitializeStatuses(presentationCommand);
		InitializeTemplate(presentationCommand);
	}

	#region Get Statuses

	private void InitializeStatuses(Command presentationCommand)
	{
		Option<string> outputOption = new(["--output", "-o"], () => string.Empty, "Path to the output JSON file.");
		Option<string> outputFormatOption = new(["--outputFormat", "-of"], () => string.Empty, "The format of the output file. Must be 'console' or 'json'.");

		Command updateCommand = new("statuses", "Gets the list of call for speaker statuses.");
		updateCommand.AddOption(outputOption);
		updateCommand.AddOption(outputFormatOption);
		presentationCommand.AddCommand(updateCommand);
		updateCommand.SetHandler(GetStatuses, outputFormatOption, outputOption);

	}

	private async Task GetStatuses(string? outputFormatOption, string? outputPath)
	{

		InputOutputFormat outputFormat = InputOutputFormat.Console;
		if (string.IsNullOrWhiteSpace(outputFormatOption) && !string.IsNullOrWhiteSpace(outputPath))
			if (Path.GetExtension(outputPath) == ".json")
				outputFormat = InputOutputFormat.Json;
			else
				throw new ArgumentException("The output file format is not supported. Please specify 'console' or 'json'.");
		else if (outputFormatOption?.ToLowerInvariant() == "json")
			outputFormat = InputOutputFormat.Json;

		if (outputFormat == InputOutputFormat.Json && string.IsNullOrWhiteSpace(outputPath))
			outputPath = "callforspeakers-statuses.json";

		List<CallForSpeakerStatus> statuses = await _callForSpeakerServices.GetStatusesAsync();

		if (outputFormat == InputOutputFormat.Json)
		{
			AnsiConsole.WriteLine(_fileServices.SerializeAndSaveFile(outputPath!, statuses));
		}
		else
		{
			var table = new Table();
			table.AddColumn("Name");
			table.AddColumn("Sort Order");
			foreach (CallForSpeakerStatus status in statuses)
				table.AddRow(status.CallForSpeakerStatusName, status.SortOrder.ToString());
			AnsiConsole.Write(table);
		}
	}

	#endregion

	#region Get Template

	private void InitializeTemplate(Command presentationCommand)
	{
		Option<string> outputOption = new(["--output", "-o"], () => string.Empty, "Path to the output file.");
		Option<string> outputFormatOption = new(["--outputFormat", "-of"], () => string.Empty, "The format of the output file. Must be 'markdown' or 'json'.");

		Command updateCommand = new("template", "Gets the template for a call for speakers.");
		updateCommand.AddOption(outputOption);
		updateCommand.AddOption(outputFormatOption);
		presentationCommand.AddCommand(updateCommand);
		updateCommand.SetHandler(GetTemplate, outputFormatOption, outputOption);
	}

	private void GetTemplate(string outputPath, string? outputFormatOption)
	{
		InputOutputFormat outputFormat = DetermineOutputFormat(outputFormatOption, InputOutputFormat.Markdown, outputPath);
		outputPath = DetermineOutputPath(outputPath, outputFormat, "callforspeakers-template");
		if (!VerifyIfOverwrite(outputPath)) return;
		AnsiConsole.WriteLine(_callForSpeakerServices.GetTemplate(outputFormat, outputPath));
	}

	#endregion

}