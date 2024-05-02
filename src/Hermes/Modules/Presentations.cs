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

	}

	private void InitializeTemplateCommand(Command presentationCommand)
	{
		Option<string> outputOption = new(["--output", "-o"], () => "presentations.json", "Name of path of the outputted JSON file.");
		Command templateCommand = new("template", "Returns a template to add a presentation.");
		templateCommand.AddOption(outputOption);
		presentationCommand.AddCommand(templateCommand);
		//templateCommand.SetHandler((output) => { await GetTemplateAsync(output); }, outputOption);

		templateCommand.SetHandler(GetTemplateAsync, outputOption);

	}

	#endregion

	internal async Task GetTemplateAsync(string output)
	{
		if (File.Exists(output) && !AnsiConsole.Confirm("The specified output path exists; do you want to overwrite?"))
			return;
		Console.WriteLine(await _presentationServices.GetTemplateAsync(output));
	}

}