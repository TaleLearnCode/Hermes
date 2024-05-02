using Hermes.Modules;
using System.CommandLine;


Presentations _presentations = new("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hermes;Integrated Security=True;Encrypt=True");

RootCommand rootCommand = new("Hermes: Speaking Engagement Management and Analytics Platform");
_presentations.InitializePresentationsCommand(rootCommand);

return await rootCommand.InvokeAsync(args);

