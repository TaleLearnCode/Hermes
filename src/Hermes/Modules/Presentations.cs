namespace Hermes.Modules;

internal class Presentations(string databaseConnectionString)
{

	private readonly PresentationServices _presentationServices = new(databaseConnectionString);
	private readonly PresentationTypeServices _presentationTypeServices = new(databaseConnectionString);
	private readonly PresentationStatusServices _presentationStatusServices = new(databaseConnectionString);
	private readonly LanguageServices _languageServices = new(databaseConnectionString);
	private readonly FileServices _fileServices = new();

	#region Command Line Argument Initialization

	internal void Initialize(RootCommand rootCommand)
	{
		Command presentationCommand = new("presentations", "Work with the 'Presentations' module.");
		rootCommand.AddCommand(presentationCommand);

		InitializeTemplateCommand(presentationCommand);
		InitializeAddCommand(presentationCommand);
		InitializeUpdateCommand(presentationCommand);
		InitializeRemoveCommand(presentationCommand);
		InitializeGetStatusesCommand(presentationCommand);
		InitializeGetTypesCommand(presentationCommand);
	}

	private void InitializeTemplateCommand(Command presentationCommand)
	{
		Option<string> outputOption = new(["--output", "-o"], () => "add-presentation.md", "Name of path of the outputted JSON file.");
		Option<string> outputFormatOption = new(["--outputFormat", "-of"], "The format of the output file. Must be 'markdown' or 'json'.");
		Command templateCommand = new("template", "Returns a template to add a presentation.");
		templateCommand.AddOption(outputOption);
		templateCommand.AddOption(outputFormatOption);
		presentationCommand.AddCommand(templateCommand);
		templateCommand.SetHandler(GetTemplateAsync, outputOption, outputFormatOption);
	}

	private void InitializeAddCommand(Command presentationCommand)
	{
		Option<string> inputOption = new(["--input", "-i"], () => string.Empty, "Path to the input JSON file.");
		Option<string> outputOption = new(["--output", "-o"], () => string.Empty, "Path to the output JSON file.");
		Option<string> inputFormatOption = new(["--inputFormat", "-if"], "The format of the input file. Must be 'markdown' or 'json'.");
		Option<string> outputFormatOption = new(["--outputFormat", "-of"], "The format of the output file. Must be 'markdown' or 'json'.");

		Command addCommand = new("add", "Adds a presentation to the database.");
		addCommand.AddOption(inputOption);
		addCommand.AddOption(outputOption);
		addCommand.AddOption(inputFormatOption);
		addCommand.AddOption(outputFormatOption);
		presentationCommand.AddCommand(addCommand);
		addCommand.SetHandler(AddPresentationAsync, inputOption, inputFormatOption, outputOption, outputFormatOption);
	}

	private void InitializeUpdateCommand(Command presentationCommand)
	{
		Option<string> inputOption = new(["--input", "-i"], () => string.Empty, "Path to the input JSON file.");
		Option<string> outputOption = new(["--output", "-o"], () => string.Empty, "Path to the output JSON file.");
		Option<string> inputFormatOption = new(["--inputFormat", "-if"], "The format of the input file. Must be 'markdown' or 'json'.");
		Option<string> outputFormatOption = new(["--outputFormat", "-of"], "The format of the output file. Must be 'markdown' or 'json'.");

		Command updateCommand = new("update", "Updates a presentation in the database.");
		updateCommand.AddOption(inputOption);
		updateCommand.AddOption(outputOption);
		updateCommand.AddOption(inputFormatOption);
		updateCommand.AddOption(outputFormatOption);
		presentationCommand.AddCommand(updateCommand);
		updateCommand.SetHandler(UpdatePresentationAsync, inputOption, inputFormatOption, outputOption, outputFormatOption);
	}

	private void InitializeRemoveCommand(Command presentationCommand)
	{
		Option<string> permalinkOption = new(["--permalink", "-p"], "The permalink of the presentation to remove.");
		Command removeCommand = new("remove", "Removes a presentation from the database.");
		removeCommand.AddOption(permalinkOption);
		presentationCommand.AddCommand(removeCommand);
		removeCommand.SetHandler(RemovePresentationAsync, permalinkOption);
	}

	private void InitializeGetStatusesCommand(Command presentationCommand)
	{
		Option<string> outputOption = new(["--output", "-o"], () => string.Empty, "Path to the output JSON file.");
		Option<string> outputFormatOption = new(["--outputFormat", "-of"], () => string.Empty, "The format of the output file. Must be 'console' or 'json'.");

		Command updateCommand = new("statuses", "Gets the list of presentation statuses.");
		updateCommand.AddOption(outputOption);
		updateCommand.AddOption(outputFormatOption);
		presentationCommand.AddCommand(updateCommand);
		updateCommand.SetHandler(GetPresentationStatuses, outputFormatOption, outputOption);

	}

	private void InitializeGetTypesCommand(Command presentationCommand)
	{
		Option<string> outputOption = new(["--output", "-o"], () => string.Empty, "Path to the output JSON file.");
		Option<string> outputFormatOption = new(["--outputFormat", "-of"], () => string.Empty, "The format of the output file. Must be 'console' or 'json'.");

		Command updateCommand = new("types", "Gets the list of presentation types.");
		updateCommand.AddOption(outputOption);
		updateCommand.AddOption(outputFormatOption);
		presentationCommand.AddCommand(updateCommand);
		updateCommand.SetHandler(GetPresentationStatuses, outputFormatOption, outputOption);

	}

	#endregion

	private async Task GetPresentationStatuses(string? outputFormatOption, string? outputPath)
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
			outputPath = "presentation-statuses.json";

		List<PresentationStatus> presentationStatuses = await _presentationStatusServices.GetListAsync();

		if (outputFormat == InputOutputFormat.Json)
		{
			AnsiConsole.WriteLine(_fileServices.SerializeAndSaveFile(outputPath!, presentationStatuses));
		}
		else
		{
			var table = new Table();
			table.AddColumn("Name");
			table.AddColumn("Sort Order");
			foreach (PresentationStatus presentationStatus in presentationStatuses)
				table.AddRow(presentationStatus.PresentationStatusName, presentationStatus.SortOrder.ToString());
			AnsiConsole.Write(table);
		}
	}

	private async Task GetPresentationTypes(string? outputFormatOption, string? outputPath)
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
			outputPath = "presentation-types.json";

		List<PresentationType> presentationTypes = await _presentationTypeServices.GetListAsync();

		if (outputFormat == InputOutputFormat.Json)
		{
			AnsiConsole.WriteLine(_fileServices.SerializeAndSaveFile(outputPath!, presentationTypes));
		}
		else
		{
			var table = new Table();
			table.AddColumn("Name");
			table.AddColumn("Sort Order");
			foreach (PresentationType presentationType in presentationTypes)
				table.AddRow(presentationType.PresentationTypeName, presentationType.SortOrder.ToString());
			AnsiConsole.Write(table);
		}
	}

	private async Task RemovePresentationAsync(string? permalink)
	{
		if (string.IsNullOrWhiteSpace(permalink))
			permalink = AnsiConsole.Ask<string>("Permalink: ");
		if (AnsiConsole.Confirm($"Are you sure you want to delete the [bold green]'{permalink}'[/] presentation?"))
			AnsiConsole.WriteLine(await _presentationServices.RemovePresentationAsync(permalink));
	}

	private async Task GetTemplateAsync(string outputPath, string? outputFormatOption)
	{

		if (File.Exists(outputPath) && !AnsiConsole.Confirm("The specified output path exists; do you want to overwrite?"))
			return;

		if (outputFormatOption is not null && !Enum.TryParse<InputOutputFormat>(outputFormatOption, true, out InputOutputFormat outputFormat))
			throw new ArgumentException($"The specified output format '{outputFormatOption}' is invalid. Please specify 'markdown' or 'json'.");
		else if (outputFormatOption is null)
			outputFormat = Path.GetExtension(outputPath) switch
			{
				".md" => InputOutputFormat.Markdown,
				".json" => InputOutputFormat.Json,
				_ => throw new ArgumentException("The output file format is not supported. Please specify 'markdown' or 'json'.")
			};
		else
			outputFormat = InputOutputFormat.Console;

		string status = string.Empty;
		await AnsiConsole.Status()
			.Spinner(Spinner.Known.Dots8Bit)
			.SpinnerStyle(Style.Parse("green bold"))
			.StartAsync("Generating template...", async ctx =>
			{
				status = await _presentationServices.GetTemplateAsync(outputPath, outputFormat);
			});
		Console.WriteLine(status);
	}

	private async Task AddPresentationAsync(
		string inputPath,
		string? inputFormatOption,
		string? outputPath,
		string? outputFormatOption)
	{
		try
		{

			if (!File.Exists(inputPath))
			{
				if (Directory.Exists(inputPath)) await AddPresentationsAsync(inputPath, inputFormatOption, outputPath, outputFormatOption);
				AnsiConsole.MarkupLine($"[red]The specified input file [/][bold red]'{inputPath}'[/][red] does not exist.[/]");
				return;
			}

			if (!string.IsNullOrWhiteSpace(outputPath)
				&& File.Exists(outputPath)
				&& !AnsiConsole.Confirm("The specified output path exists; do you want to overwrite?"))
				return;


			InputOutputFormat inputFormat = GetInputFormat(inputPath, inputFormatOption);
			InputOutputFormat? outputFormat = GetOutputFormat(outputPath, outputFormatOption);

			PresentationRequest? presentationRequest = await GetPresentationRequestAsync(inputPath, inputFormat);
			if (presentationRequest is null)
				AnsiConsole.Write("[red]The specified input file is not a valid JSON presentation file.[/]");
			else
				AnsiConsole.Write(await AddPresentationFromRequestAsync(presentationRequest, inputFormat, outputPath, outputFormat));

		}
		catch (ArgumentException ex)
		{
			AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
		}
		catch (Exception ex)
		{
			AnsiConsole.WriteException(ex,
				ExceptionFormats.ShortenPaths
				| ExceptionFormats.ShortenTypes
				| ExceptionFormats.ShortenMethods
				| ExceptionFormats.ShortenEverything
				| ExceptionFormats.NoStackTrace);
		}

	}

	private async Task UpdatePresentationAsync(
		string inputPath,
		string? inputFormatOption,
		string? outputPath,
		string? outputFormatOption)
	{
		try
		{

			if (!File.Exists(inputPath))
			{
				if (Directory.Exists(inputPath)) await UpdatePresentationsAsync(inputPath, inputFormatOption, outputPath, outputFormatOption);
				AnsiConsole.MarkupLine($"[red]The specified input file [/][bold red]'{inputPath}'[/][red] does not exist.[/]");
				return;
			}

			if (!string.IsNullOrWhiteSpace(outputPath)
				&& File.Exists(outputPath)
				&& !AnsiConsole.Confirm("The specified output path exists; do you want to overwrite?"))
				return;


			InputOutputFormat inputFormat = GetInputFormat(inputPath, inputFormatOption);
			InputOutputFormat? outputFormat = GetOutputFormat(outputPath, outputFormatOption);

			PresentationRequest? presentationRequest = await GetPresentationRequestAsync(inputPath, inputFormat);
			if (presentationRequest is null)
				AnsiConsole.Write("[red]The specified input file is not a valid presentation file.[/]");
			else
				AnsiConsole.Write(await UpdatePresentationFromRequestAsync(presentationRequest, inputFormat, outputPath, outputFormat));

		}
		catch (ArgumentException ex)
		{
			AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
		}
		catch (Exception ex)
		{
			AnsiConsole.WriteException(ex,
				ExceptionFormats.ShortenPaths
				| ExceptionFormats.ShortenTypes
				| ExceptionFormats.ShortenMethods
				| ExceptionFormats.ShortenEverything
				| ExceptionFormats.NoStackTrace);
		}

	}

	private async Task<PresentationRequest?> GetPresentationRequestAsync(
		string inputPath,
		InputOutputFormat inputFormat)
	{
		return inputFormat switch
		{
			InputOutputFormat.Json => _presentationServices.BuildPresentationRequestFromJson(inputPath),
			InputOutputFormat.Markdown => await PresentationServices.BuildPresentationRequestFromMarkdownAsync(inputPath),
			_ => null
		};
	}

	private static InputOutputFormat GetInputFormat(
		string inputPath,
		string? inputFormatOption)
	{
		if (inputFormatOption is not null && !Enum.TryParse<InputOutputFormat>(inputFormatOption, true, out InputOutputFormat inputFormat))
			throw new ArgumentException($"The specified input format '{inputFormatOption}' is invalid. Please specify 'markdown' or 'json'.");
		else if (inputFormatOption is null)
			inputFormat = Path.GetExtension(inputPath) switch
			{
				".md" => InputOutputFormat.Markdown,
				".json" => InputOutputFormat.Json,
				_ => throw new ArgumentException("The input file format is not supported. Please specify 'markdown' or 'json'.")
			};
		else
			inputFormat = InputOutputFormat.Console;
		return inputFormat;
	}

	private static InputOutputFormat GetOutputFormat(
		string? outputPath,
		string? outputFormatOption)
	{
		if (outputFormatOption is not null && !Enum.TryParse<InputOutputFormat>(outputFormatOption, true, out InputOutputFormat outputFormat))
			throw new ArgumentException($"The specified output format '{outputFormatOption}' is invalid. Please specify 'markdown' or 'json'.");
		else if (outputFormatOption is null && !string.IsNullOrWhiteSpace(outputPath))
			outputFormat = Path.GetExtension(outputPath) switch
			{
				".md" => InputOutputFormat.Markdown,
				".json" => InputOutputFormat.Json,
				_ => throw new ArgumentException("The output file format is not supported. Please specify 'markdown' or 'json'.")
			};
		else
			outputFormat = InputOutputFormat.Console;
		return outputFormat;
	}

	private async Task AddPresentationsAsync(
		string inputPath,
		string? inputFormatOption,
		string? outputPath,
		string? outputFormatOption)
	{
		try
		{

			if (!IsInputPathValid(inputPath)) return;
			if (!IsOutputPathValid(outputPath)) return;
			InputOutputFormat inputFormat = GetInputFormat(inputPath, inputFormatOption);
			InputOutputFormat? outputFormat = GetOutputFormat(outputPath, outputFormatOption);
			if (!TryGetInputFiles(inputPath, inputFormat, out string[] inputFilePaths)) return;

			foreach (string inputFilePath in inputFilePaths)
			{
				AnsiConsole.MarkupLine($"[bold]Processing file:[/] {inputFilePath}");
				try
				{
					PresentationRequest? presentationRequest = inputFormat switch
					{
						InputOutputFormat.Json => _presentationServices.BuildPresentationRequestFromJson(inputFilePath),
						InputOutputFormat.Markdown => await PresentationServices.BuildPresentationRequestFromMarkdownAsync(inputFilePath),
						_ => null
					};

					if (presentationRequest is null)
						AnsiConsole.Write("[red]The specified input file is not a valid JSON presentation file.[/]");
					else
						AnsiConsole.WriteLine(await AddPresentationFromRequestAsync(presentationRequest, inputFormat, outputPath, outputFormat));

				}
				catch (ArgumentException ex)
				{
					AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
				}
				catch (Exception ex)
				{
					AnsiConsole.WriteException(ex,
						ExceptionFormats.ShortenPaths
						| ExceptionFormats.ShortenTypes
						| ExceptionFormats.ShortenMethods
						| ExceptionFormats.ShortenEverything
						| ExceptionFormats.NoStackTrace);
				}
			}
		}
		catch (ArgumentException ex)
		{
			AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
		}
		catch (Exception ex)
		{
			AnsiConsole.WriteException(ex,
				ExceptionFormats.ShortenPaths
				| ExceptionFormats.ShortenTypes
				| ExceptionFormats.ShortenMethods
				| ExceptionFormats.ShortenEverything
				| ExceptionFormats.NoStackTrace);
		}

	}

	private async Task UpdatePresentationsAsync(
		string inputPath,
		string? inputFormatOption,
		string? outputPath,
		string? outputFormatOption)
	{
		try
		{

			if (!IsInputPathValid(inputPath)) return;
			if (!IsOutputPathValid(outputPath)) return;
			InputOutputFormat inputFormat = GetInputFormat(inputPath, inputFormatOption);
			InputOutputFormat? outputFormat = GetOutputFormat(outputPath, outputFormatOption);
			if (!TryGetInputFiles(inputPath, inputFormat, out string[] inputFilePaths)) return;

			foreach (string inputFilePath in inputFilePaths)
			{
				AnsiConsole.MarkupLine($"[bold]Processing file:[/] {inputFilePath}");
				try
				{
					PresentationRequest? presentationRequest = inputFormat switch
					{
						InputOutputFormat.Json => _presentationServices.BuildPresentationRequestFromJson(inputFilePath),
						InputOutputFormat.Markdown => await PresentationServices.BuildPresentationRequestFromMarkdownAsync(inputFilePath),
						_ => null
					};

					if (presentationRequest is null)
						AnsiConsole.Write("[red]The specified input file is not a valid presentation file.[/]");
					else
						AnsiConsole.WriteLine(await UpdatePresentationFromRequestAsync(presentationRequest, inputFormat, outputPath, outputFormat));

				}
				catch (ArgumentException ex)
				{
					AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
				}
				catch (Exception ex)
				{
					AnsiConsole.WriteException(ex,
						ExceptionFormats.ShortenPaths
						| ExceptionFormats.ShortenTypes
						| ExceptionFormats.ShortenMethods
						| ExceptionFormats.ShortenEverything
						| ExceptionFormats.NoStackTrace);
				}
			}
		}
		catch (ArgumentException ex)
		{
			AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
		}
		catch (Exception ex)
		{
			AnsiConsole.WriteException(ex,
				ExceptionFormats.ShortenPaths
				| ExceptionFormats.ShortenTypes
				| ExceptionFormats.ShortenMethods
				| ExceptionFormats.ShortenEverything
				| ExceptionFormats.NoStackTrace);
		}

	}

	private static bool TryGetInputFiles(string inputPath, InputOutputFormat inputFormat, out string[] inputFilePaths)
	{
		inputFilePaths = Directory.GetFiles(inputPath, $"*.{(inputFormat == InputOutputFormat.Json ? "json" : "md")}", SearchOption.TopDirectoryOnly);
		if (inputFilePaths.Length == 0)
		{
			if (AnsiConsole.Confirm("Unable to file files matching the specified format in the specified directory. Do you want to process all files in the directory?"))
				inputFilePaths = Directory.GetFiles(inputPath, "*.*", SearchOption.TopDirectoryOnly);
		}
		if (inputFilePaths.Length == 0)
		{
			AnsiConsole.MarkupLine("[red]No files found in the specified directory.[/]");
			return false;
		}
		return true;
	}

	private static bool IsInputPathValid(string inputPath)
	{
		if (!Directory.Exists(inputPath))
		{
			AnsiConsole.MarkupLine($"[red]The specified input path [/][bold red]'{inputPath}'[/][red] does not exist.[/]");
			return false;
		}
		return true;
	}

	private static bool IsOutputPathValid(string? outputPath)
	{
		if (!string.IsNullOrWhiteSpace(outputPath)
			&& File.Exists(outputPath)
			&& !AnsiConsole.Confirm("The specified output path exists; do you want to overwrite?"))
			return false;
		return true;
	}
	private async Task<string> AddPresentationFromRequestAsync(
		PresentationRequest presentationRequest,
		InputOutputFormat inputFormat,
		string? outputPath,
		InputOutputFormat? outputFormat)
	{

		await GetMissingAttributes(presentationRequest);

		string status = string.Empty;
		await AnsiConsole.Status()
			.Spinner(Spinner.Known.Dots8Bit)
			.SpinnerStyle(Style.Parse("green bold"))
			.StartAsync("Adding presentation...", async ctx =>
			{
				status = await _presentationServices.AddPresentation(presentationRequest, inputFormat, outputPath, outputFormat);
			});
		return status;

	}

	private async Task<string> UpdatePresentationFromRequestAsync(
		PresentationRequest presentationRequest,
		InputOutputFormat inputFormat,
		string? outputPath,
		InputOutputFormat? outputFormat)
	{

		await GetMissingAttributes(presentationRequest);

		string status = string.Empty;
		await AnsiConsole.Status()
			.Spinner(Spinner.Known.Dots8Bit)
			.SpinnerStyle(Style.Parse("green bold"))
			.StartAsync("Adding presentation...", async ctx =>
			{
				status = await _presentationServices.UpdatePresentationAsync(presentationRequest, inputFormat, outputPath, outputFormat);
			});
		return status;

	}


	private async Task GetMissingAttributes(PresentationRequest presentationRequest)
	{
		if (string.IsNullOrEmpty(presentationRequest.Title))
			presentationRequest.Title = AnsiConsole.Ask<string>("Title: ");
		if (string.IsNullOrEmpty(presentationRequest.Permalink))
			presentationRequest.Permalink = PresentationServices.GetPermalink(presentationRequest);

		PresentationType? presentationType = null;
		if (!string.IsNullOrWhiteSpace(presentationRequest.PresentationType))
			presentationType = await _presentationTypeServices.GetByNameAsync(presentationRequest.PresentationType);
		presentationType ??= await GetPresentationType();

		PresentationStatus? presentationStatus = null;
		if (!string.IsNullOrWhiteSpace(presentationRequest.PresentationStatus))
			presentationStatus = await _presentationStatusServices.GetByNameAsync(presentationRequest.PresentationStatus);
		presentationStatus ??= await GetPresentationStatus();

		Language? language = null;
		if (!string.IsNullOrWhiteSpace(presentationRequest.DefaultLanguageCode))
			language = await _languageServices.GetByCodeAsync(presentationRequest.DefaultLanguageCode);
		language ??= await GetLanguage();
	}

	private async Task<PresentationType> GetPresentationType()
	{
		List<PresentationType> presentationTypes = await _presentationTypeServices.GetListAsync();
		string presentationTypeName = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.Title("Presentation Type [green](required)[/]:")
				.PageSize(10)
				.MoreChoicesText("More presentation types")
				.AddChoices(presentationTypes.Select(x => x.PresentationTypeName).ToArray()));
		return presentationTypes.First(x => x.PresentationTypeName == presentationTypeName);
	}

	private async Task<PresentationStatus> GetPresentationStatus()
	{
		List<PresentationStatus> presentationStatuses = await _presentationStatusServices.GetListAsync();
		string presentationStatusName = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.Title("Presentation Status [green](required)[/]:")
				.PageSize(10)
				.MoreChoicesText("More presentation statuses")
				.AddChoices(presentationStatuses.Select(x => x.PresentationStatusName).ToArray()));
		return presentationStatuses.First(x => x.PresentationStatusName == presentationStatusName);
	}

	private async Task<Language> GetLanguage()
	{
		List<Language> languages = await _languageServices.GetListAsync();
		string languageCode = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.Title("Language Code [green](required)[/]:")
				.PageSize(10)
				.MoreChoicesText("More languages")
				.AddChoices(languages.Select(x => x.LanguageCode).ToArray()));
		return languages.First(x => x.LanguageCode == languageCode);
	}

}