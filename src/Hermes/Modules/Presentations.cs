using Hermes.Models;
using Hermes.Requests;

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
		Option<string> outputOption = new(["--output", "-o"], () => "presentations.json", "Name of path of the outputted JSON file.");
		Command templateCommand = new("template", "Returns a template to add a presentation.");
		templateCommand.AddOption(outputOption);
		presentationCommand.AddCommand(templateCommand);
		templateCommand.SetHandler(GetTemplateAsync, outputOption);
	}

	private void InitializeAddCommand(Command presentationCommand)
	{
		Option<string> inputOption = new(["--input", "-i"], () => string.Empty, "Path to the input JSON file.");
		Option<string> outputOption = new(["--output", "-o"], () => StaticValues.NoEntryDefault, "Path to the output JSON file.");
		//Option<string> outputOption = new(["--output", "-o"], "Path to the output JSON file.");
		Option<string> formatOption = new(["--format", "-f"], "The format of the input file. Must be 'markdown' or 'json'.");
		Command addCommand = new("add", "Adds a presentation to the database.");
		addCommand.AddOption(inputOption);
		addCommand.AddOption(outputOption);
		addCommand.AddOption(formatOption);
		presentationCommand.AddCommand(addCommand);
		addCommand.SetHandler(AddPresentationAsync, inputOption, outputOption, formatOption);
	}

	#endregion

	private async Task GetTemplateAsync(string output)
	{
		if (File.Exists(output) && !AnsiConsole.Confirm("The specified output path exists; do you want to overwrite?"))
			return;
		string status = string.Empty;
		await AnsiConsole.Status()
			.Spinner(Spinner.Known.Dots8Bit)
			.SpinnerStyle(Style.Parse("green bold"))
			.StartAsync("Generating template...", async ctx =>
			{
				status = await _presentationServices.GetTemplateAsync(output);
			});
		Console.WriteLine(status);
	}

	//private async Task AddPresentationAsync(string inputPath, string? outputPath)
	//{
	//	try
	//	{
	//		if (!File.Exists(inputPath))
	//		{
	//			AnsiConsole.MarkupLine($"[red]The specified input file [/][bold red]'{inputPath}'[/][red] does not exist.[/]");
	//			return;
	//		}
	//		if (string.IsNullOrWhiteSpace(outputPath)) outputPath = null;
	//		if (!string.IsNullOrWhiteSpace(outputPath)
	//			&& File.Exists(outputPath)
	//			&& !AnsiConsole.Confirm("The specified output path exists; do you want to overwrite?"))
	//			return;
	//		string status = string.Empty;
	//		await AnsiConsole.Status()
	//			.Spinner(Spinner.Known.Dots8Bit)
	//			.SpinnerStyle(Style.Parse("green bold"))
	//			.StartAsync("Adding presentation...", async ctx =>
	//			{
	//				status = await _presentationServices.AddPresentationFromJsonAsync(inputPath, outputPath);
	//			});
	//		Console.WriteLine(status);
	//	}
	//	catch (ArgumentException ex)
	//	{
	//		AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
	//	}
	//	catch (Exception ex)
	//	{
	//		AnsiConsole.WriteException(ex,
	//			ExceptionFormats.ShortenPaths
	//			| ExceptionFormats.ShortenTypes
	//			| ExceptionFormats.ShortenMethods
	//			| ExceptionFormats.ShortenEverything
	//			| ExceptionFormats.NoStackTrace);
	//	}

	//}

	private async Task AddPresentationAsync(string inputPath, string? outputPath, string format)
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
			string status = string.Empty;
			//await AnsiConsole.Status()
			//	.Spinner(Spinner.Known.Dots8Bit)
			//	.SpinnerStyle(Style.Parse("green bold"))
			//	.StartAsync("Adding presentation...", async ctx =>
			//	{
			//		status = await _presentationServices.AddPresentationFromJsonAsync(inputPath, outputPath);
			//	});
			//Console.WriteLine(status);


			status = format switch
			{
				"markdown" => await AddPresentationFromMarkdown(inputPath, outputPath),
				"json" => await AddPresentationFromJson(inputPath, outputPath),
				_ => "[red]Invalid format. Please specify 'markdown' or 'json'.[/]",
			};
			AnsiConsole.Markup(status);





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

	private async Task<string> AddPresentationFromJson(string inputPath, string? outputPath)
	{
		string status = string.Empty;
		await AnsiConsole.Status()
			.Spinner(Spinner.Known.Dots8Bit)
			.SpinnerStyle(Style.Parse("green bold"))
			.StartAsync("Adding presentation...", async ctx =>
			{
				status = await _presentationServices.AddPresentationFromJsonAsync(inputPath, outputPath);
			});
		return status;
	}

	private async Task<string> AddPresentationFromMarkdown(string inputPath, string? outputPath)
	{
		//MarkdownPresentationRequest request = new()
		//{
		//	LanguageCode = AnsiConsole.Ask<string>("Language Code: "),
		//	PublicRepoLink = AnsiConsole.Prompt(new TextPrompt<string>("Public Repo Link: ").AllowEmpty()),
		//	PrivateRepoLink = AnsiConsole.Prompt(new TextPrompt<string>("Private Repo Link: ").AllowEmpty()),
		//	Status = AnsiConsole.Ask<string>("Status: "),
		//	IsArchived = AnsiConsole.Confirm("Not Archived: "),
		//	IncludeInPublicProfile = AnsiConsole.Confirm("Include in Public Profile: ")
		//};
		//string status = string.Empty;
		//await AnsiConsole.Status()
		//	.Spinner(Spinner.Known.Dots8Bit)
		//	.SpinnerStyle(Style.Parse("green bold"))
		//	.StartAsync("Adding presentation...", async ctx =>
		//	{
		//		status = await _presentationServices.AddPresentationFromMarkdownAsync(inputPath, outputPath, request);
		//	});
		//return status;

		MarkdownPresentationRequest presentationRequest = await _presentationServices.BuildPresentationRequestFromMarkdownAsync(inputPath);
		if (presentationRequest is null)
			return "[red]The specified input file is not a valid Markdown presentation file.[/]";

		if (string.IsNullOrEmpty(presentationRequest.Title))
			presentationRequest.Title = AnsiConsole.Ask<string>("Title: ");
		if (string.IsNullOrEmpty(presentationRequest.Permalink))
			presentationRequest.Permalink = _presentationServices.GetPermalink(presentationRequest);

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
				status = await _presentationServices.AddPresentation(presentationRequest, outputPath);
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