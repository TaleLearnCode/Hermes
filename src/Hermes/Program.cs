string databaseConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hermes;Integrated Security=True;Encrypt=True";

Presentations _presentations = new(databaseConnectionString);
CallForSpeakers _callForSpeakers = new(databaseConnectionString);

RootCommand rootCommand = new("Hermes: Speaking Engagement Management and Analytics Platform");
_presentations.Initialize(rootCommand);
_callForSpeakers.Initialize(rootCommand);


return await rootCommand.InvokeAsync(args);