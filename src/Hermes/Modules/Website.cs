namespace Hermes.Modules;

internal class Website(string databaseConnectionString)
{

	private readonly WebsiteServices _websiteServices = new(databaseConnectionString);

	internal void Initialize(RootCommand rootCommand)
	{

		Command websiteCommand = new("website", "Work with the 'Website' module.");
		rootCommand.AddCommand(websiteCommand);

		InitializeGetEngagements(websiteCommand);
		InitializeGetPresentations(websiteCommand);

	}

	#region Get Engagements

	private void InitializeGetEngagements(Command websiteCommand)
	{
		Command engagementsCommand = new("engagements", "Gets engagements from the database.");
		Option<string> outputOption = new(["--output", "-o"], "Path to the output file.");
		engagementsCommand.AddOption(outputOption);
		websiteCommand.AddCommand(engagementsCommand);
		engagementsCommand.SetHandler(GetEngagementsAsync, outputOption);
	}

	private async Task GetEngagementsAsync(string? outputPath)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(outputPath))
				outputPath = Path.Combine(Directory.GetCurrentDirectory(), "engagements.json");
			AnsiConsole.WriteLine(await _websiteServices.GetEngagementsAsync(outputPath));
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

	#region Get Presentations

	private void InitializeGetPresentations(Command websiteCommand)
	{
		Command presentationsCommand = new("presentations", "Gets presentations from the database.");
		Option<string> outputOption = new(["--output", "-o"], "Path to the output file.");
		presentationsCommand.AddOption(outputOption);
		websiteCommand.AddCommand(presentationsCommand);
		presentationsCommand.SetHandler(GetPresentationsAsync, outputOption);
	}

	private async Task GetPresentationsAsync(string? outputPath)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(outputPath))
				outputPath = Path.Combine(Directory.GetCurrentDirectory(), "presentations.json");
			AnsiConsole.WriteLine(await _websiteServices.GetPresentationsAsync(outputPath));
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

}
