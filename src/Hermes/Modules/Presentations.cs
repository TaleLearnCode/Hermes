namespace Hermes.Modules;

internal class Presentations(string databaseConnectionString)
{

	private readonly PresentationServices _presentationServices = new(databaseConnectionString);

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
		Command addCommand = new("add", "Adds a presentation to the database.");
		addCommand.AddOption(inputOption);
		addCommand.AddOption(outputOption);
		presentationCommand.AddCommand(addCommand);
		addCommand.SetHandler(AddPresentationAsync, inputOption, outputOption);
	}

	#endregion

	internal async Task GetTemplateAsync(string output)
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

	internal async Task AddPresentationAsync(string inputPath, string? outputPath)
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
			await AnsiConsole.Status()
				.Spinner(Spinner.Known.Dots8Bit)
				.SpinnerStyle(Style.Parse("green bold"))
				.StartAsync("Adding presentation...", async ctx =>
				{
					status = await _presentationServices.AddPresentationAsync(inputPath, outputPath);
				});
			Console.WriteLine(status);
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