string databaseConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hermes;Integrated Security=True;Encrypt=True";

Presentations _presentations = new(databaseConnectionString);
//CallForSpeakers _callForSpeakers = new(databaseConnectionString);
Engagements _engagements = new(databaseConnectionString);
Website _website = new(databaseConnectionString);

RootCommand rootCommand = new("Hermes: Speaking Engagement Management and Analytics Platform");
_presentations.Initialize(rootCommand);
//_callForSpeakers.Initialize(rootCommand);
_engagements.Initialize(rootCommand);
_website.Initialize(rootCommand);

return await rootCommand.InvokeAsync(args);





//string markdownContent = System.IO.File.ReadAllText(@"C:\Repos\Hermes\src\Hermes\bin\Debug\net8.0\serverless-architecture-conference-berlin-2024.md");
//EngagementRequest engagementRequest = EngagementServices.ParseMarkdownToEngagementRequest(markdownContent);
//Console.WriteLine(engagementRequest.Name);

//EngagementRequest engagementRequest = EngagementServices.ParseMarkdownFileToEngagementRequest(@"C:\Repos\Hermes\src\Hermes\bin\Debug\net8.0\serverless-architecture-conference-berlin-2024.md");
//Console.WriteLine(engagementRequest.Name);