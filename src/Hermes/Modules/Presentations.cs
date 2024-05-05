using Hermes.Models;
using Hermes.Requests;
using Hermes.Types;

namespace Hermes.Modules;

internal class Presentations(string databaseConnectionString)
{

	private readonly PresentationServices _presentationServices = new(databaseConnectionString);
	private readonly PresentationTypeServices presentationTypeServices = new(databaseConnectionString);
	private readonly PresentationStatusServices presentationStatusServices = new(databaseConnectionString);
	private readonly LanguageServices _languageServices = new(databaseConnectionString);

	#region Command Line Argument Initialization

	internal void InitializePresentationsCommand(RootCommand rootCommand)
	{
		Command presentationCommand = new("presentations", "Work with the 'Presentations' module.");
		rootCommand.AddCommand(presentationCommand);

		InitializeTemplateCommand(presentationCommand);
		InitializeAddCommand(presentationCommand);
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

	#endregion

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
				AnsiConsole.MarkupLine($"[red]The specified input file [/][bold red]'{inputPath}'[/][red] does not exist.[/]");
				return;
			}
			if (string.IsNullOrWhiteSpace(outputPath)) outputPath = null;
			if (!string.IsNullOrWhiteSpace(outputPath)
				&& File.Exists(outputPath)
				&& !AnsiConsole.Confirm("The specified output path exists; do you want to overwrite?"))
				return;

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


			InputOutputFormat? outputFormat = null;
			if (!string.IsNullOrWhiteSpace(outputPath))
			{
				InputOutputFormat testOutputFormat = InputOutputFormat.Console;
				if (outputFormatOption is not null && !Enum.TryParse<InputOutputFormat>(outputFormatOption, true, out testOutputFormat))
					throw new ArgumentException($"The specified output format '{outputFormatOption}' is invalid. Please specify 'markdown' or 'json'.");
				else if (outputFormatOption is null)
					testOutputFormat = Path.GetExtension(outputPath) switch
					{
						".md" => InputOutputFormat.Markdown,
						".json" => InputOutputFormat.Json,
						_ => throw new ArgumentException("The output file format is not supported. Please specify 'markdown' or 'json'.")
					};
				outputFormat = testOutputFormat;
			}

			MarkdownPresentationRequest? presentationRequest = inputFormat switch
			{
				InputOutputFormat.Json => _presentationServices.BuildPresentationRequestFromJson(inputPath),
				InputOutputFormat.Markdown => await PresentationServices.BuildPresentationRequestFromMarkdownAsync(inputPath),
				_ => null
			};

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

	private async Task<string> AddPresentationFromRequestAsync(
		MarkdownPresentationRequest presentationRequest,
		InputOutputFormat inputFormat,
		string? outputPath,
		InputOutputFormat? outputFormat)
	{

		if (string.IsNullOrEmpty(presentationRequest.Title))
			presentationRequest.Title = AnsiConsole.Ask<string>("Title: ");
		if (string.IsNullOrEmpty(presentationRequest.Permalink))
			presentationRequest.Permalink = PresentationServices.GetPermalink(presentationRequest);

		PresentationType? presentationType = null;
		if (!string.IsNullOrWhiteSpace(presentationRequest.PresentationType))
			presentationType = await presentationTypeServices.GetByNameAsync(presentationRequest.PresentationType);
		presentationType ??= await GetPresentationType();

		PresentationStatus? presentationStatus = null;
		if (!string.IsNullOrWhiteSpace(presentationRequest.PresentationStatus))
			presentationStatus = await presentationStatusServices.GetByNameAsync(presentationRequest.PresentationStatus);
		presentationStatus ??= await GetPresentationStatus();

		Language? language = null;
		if (!string.IsNullOrWhiteSpace(presentationRequest.DefaultLanguageCode))
			language = await _languageServices.GetByCodeAsync(presentationRequest.DefaultLanguageCode);
		language ??= await GetLanguage();

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


	private async Task<PresentationType> GetPresentationType()
	{
		List<PresentationType> presentationTypes = await presentationTypeServices.GetListAsync();
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
		List<PresentationStatus> presentationStatuses = await presentationStatusServices.GetListAsync();
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