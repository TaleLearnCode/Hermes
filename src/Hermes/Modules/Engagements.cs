namespace Hermes.Modules;

internal class Engagements(string databaseConnectionString) : ModuleBase
{

	private readonly EngagementServices _engagementServices = new(databaseConnectionString);

	internal void Initialize(RootCommand rootCommand)
	{

		Command engagementCommand = new("engagements", "Work with the 'Engagements' module.");
		rootCommand.AddCommand(engagementCommand);

		InitializeAdd(engagementCommand);
		InitializeAddPresentationDownload(engagementCommand);

	}

	#region Add Engagement(s)

	private void InitializeAdd(Command engagementCommand)
	{
		Option<string> inputOption = new(new[] { "--input", "-i" }, "Path to the input file.");
		Option<string> inputFormatOption = new(new[] { "--inputFormat", "-if" }, "The format of the input file. Must be 'markdown' or 'json'.");
		Option<string> outputOption = new(new[] { "--output", "-o" }, "Path to the output file.");
		Option<string> outputFormatOption = new(new[] { "--outputFormat", "-of" }, "The format of the output file. Must be 'markdown' or 'json'.");

		Command addCommand = new("add", "Adds a call for speakers.");
		addCommand.AddOption(inputOption);
		addCommand.AddOption(inputFormatOption);
		addCommand.AddOption(outputOption);
		addCommand.AddOption(outputFormatOption);
		engagementCommand.AddCommand(addCommand);
		addCommand.SetHandler(AddEngagementAsync, inputOption, inputFormatOption, outputOption, outputFormatOption);
	}

	private async Task AddEngagementAsync(
		string inputPath,
		string? inputFormatOption,
		string? outputPath,
		string? outputFormatOption)
	{
		try
		{
			if (!File.Exists(inputPath))
			{
				if (Directory.Exists(inputPath))
					await AddEngagementsAsync(inputPath, inputFormatOption, outputPath, outputFormatOption);
				else
					AnsiConsole.MarkupLine($"[red]The specified input file or directory does not exist.[/]");
				return;
			}

			InputOutputFormat inputFormat = DetermineFileFormat(inputFormatOption, InputOutputFormat.Console, inputPath);
			if (inputFormat == InputOutputFormat.Console)
				throw new ArgumentException("The input file format is not supported. Please specify 'markdown' or 'json'.");
			inputPath = DetermineFilePath(inputPath, inputFormat, "engagement");
			InputOutputFormat outputFormat = DetermineFileFormat(outputFormatOption, inputFormat, outputPath, true);
			AnsiConsole.WriteLine(await _engagementServices.AddEngagementAsync(inputPath, inputFormat, outputPath, outputFormat));

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

	private async Task AddEngagementsAsync(string inputPath, string? inputFormatOption, string outputPath, string? outputFormatOption)
	{
		try
		{
			if (!IsInputPathValid(inputPath)) return;
			if (!IsOutputPathValid(outputPath)) return;
			InputOutputFormat? outputFormat = GetOutputFormat(outputPath, outputFormatOption);
			if (!TryGetInputFiles(inputPath, out string[] inputFilePaths)) return;
			foreach (string inputFilePath in inputFilePaths)
			{
				AnsiConsole.MarkupLine($"[bold]Processing file:[/] {inputFilePath}");
				try
				{
					InputOutputFormat inputFormat = GetInputFormat(inputFilePath, inputFormatOption);
					AnsiConsole.WriteLine(await _engagementServices.AddEngagementAsync(inputFilePath, inputFormat, outputPath, outputFormat));
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

	#endregion

	#region Add Presentation Download 

	private void InitializeAddPresentationDownload(Command engagementCommand)
	{

		Option<string> engagementPermalinkOption = new(["--engagement", "-e"], "The permalink of the engagement.");
		Option<string> presentationPermalinkOption = new(["--presentation", "-p"], "The permalink of the presentation.");
		Option<string> downloadTypeOption = new(["--type", "-t"], "The type of download.");
		Option<string> downloadNameOption = new(["--name", "-n"], "The name of the download.");
		Option<string> downloadPathOption = new(["--path", "-pa"], "The path to the download.");

		Command addPresentationDownloadCommand = new("addPresentationDownload", "Adds a download for a presentation.");
		addPresentationDownloadCommand.AddOption(engagementPermalinkOption);
		addPresentationDownloadCommand.AddOption(presentationPermalinkOption);
		addPresentationDownloadCommand.AddOption(downloadTypeOption);
		addPresentationDownloadCommand.AddOption(downloadNameOption);
		addPresentationDownloadCommand.AddOption(downloadPathOption);
		engagementCommand.AddCommand(addPresentationDownloadCommand);
		addPresentationDownloadCommand.SetHandler(
			AddPresentationDownloadAsync,
					engagementPermalinkOption,
					presentationPermalinkOption,
					downloadTypeOption,
					downloadNameOption,
					downloadPathOption);
	}

	private async Task AddPresentationDownloadAsync(
		string? engagementPermalink,
		string? presentationPermalink,
		string? downloadType,
		string? downloadName,
		string? downloadPath)
	{

		try
		{

			if (string.IsNullOrWhiteSpace(engagementPermalink))
				engagementPermalink = await PromptForEngagementPermalinkAsync();
			if (string.IsNullOrWhiteSpace(presentationPermalink))
				presentationPermalink = await PromptForEngagementPresentationAsync(engagementPermalink);
			if (string.IsNullOrWhiteSpace(downloadType))
				downloadType = AnsiConsole.Prompt(
					new SelectionPrompt<string>()
						.Title("Download Type")
						.AddChoices((await _engagementServices.GetDownloadTypesAsync()).Select(x => x.DownloadTypeName)));
			if (string.IsNullOrWhiteSpace(downloadName))
				downloadName = AnsiConsole.Ask<string>("Download Name:", downloadType);
			if (string.IsNullOrWhiteSpace(downloadPath))
				downloadPath = AnsiConsole.Prompt(
					new TextPrompt<string>("[grey][[Optional]][/] Download Path")
						.AllowEmpty());

			AnsiConsole.WriteLine(await _engagementServices.AddEngagementPresentationDownloadAsync(engagementPermalink, presentationPermalink, downloadType, downloadName, downloadPath));

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

	private async Task<string> PromptForEngagementPermalinkAsync()
	{
		int year = AnsiConsole.Prompt(
			new SelectionPrompt<int>()
					.Title("Year of engagement?")
					.PageSize(10)
					.MoreChoicesText("[grey](Move up and adown to reveal more years)[/]")
					.AddChoices(await _engagementServices.GetEngagementYearsAsync()));
		Dictionary<string, string> engagementsForYear = await _engagementServices.GetEngagementsForYearAsync(year);
		string engagementName = AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.Title("Select the engagement")
				.PageSize(10)
				.MoreChoicesText("[grey](Move up and adown to reveal more engagements)[/]")
				.AddChoices(engagementsForYear.Values));
		return engagementsForYear.First(x => x.Value == engagementName).Key;
	}

	private async Task<string> PromptForEngagementPresentationAsync(string engagementPermalink)
	{
		Dictionary<string, string> engagementPresentations = await _engagementServices.GetEngagementPresentationsAsync(engagementPermalink);
		string presentationName = AnsiConsole.Prompt(
					new SelectionPrompt<string>()
						.Title("Select the presentation")
						.PageSize(10)
						.MoreChoicesText("[grey](Move up and adown to reveal more presentations)[/]")
						.AddChoices(engagementPresentations.Values));
		return engagementPresentations.First(x => x.Value == presentationName).Key;
	}

	#endregion

}