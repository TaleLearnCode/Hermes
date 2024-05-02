using System.CommandLine;



var rootCommand = new RootCommand("Hermes CLI");




var presentationCommand = new Command("presentations", "Work with the 'Presentations' module.");
rootCommand.AddCommand(presentationCommand);

var presentationTemplateOutputOption = new Option<string>(name: "--output", description: "Name of the outputted JSON file.", getDefaultValue: () => "presentation.json");
var templateCommand = new Command("template", "Returns a template to add the target object.");
templateCommand.AddOption(presentationTemplateOutputOption);
presentationCommand.AddCommand(templateCommand);



templateCommand.SetHandler((output) => { PresentationsTemplate(output); }, presentationTemplateOutputOption);

return await rootCommand.InvokeAsync(args);

void PresentationsTemplate(string output)
{
	Console.WriteLine($"Presentations -- Template -- {output}");
}