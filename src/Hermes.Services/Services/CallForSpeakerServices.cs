namespace Hermes.Services;

public class CallForSpeakerServices(string databaseConnectionString) : ServicesBase(databaseConnectionString)
{

	public async Task<List<CallForSpeakerStatus>> GetStatusesAsync()
		=> [.. (await GetAllAsync<CallForSpeakerStatus>()).OrderBy(x => x.SortOrder)];

	public string GetTemplate(InputOutputFormat outputFormat, string outputPath)
	{
		CallForSpeakerRequest callForSpeakerRequest = new();
		if (outputFormat == InputOutputFormat.Json)
		{
			callForSpeakerRequest.AddInstructions();
			return SerializeAndSaveFile<CallForSpeakerRequest>(outputPath, callForSpeakerRequest);
		}
		else if (outputFormat == InputOutputFormat.Markdown)
		{
			return SaveFile<CallForSpeakerRequest>(outputPath, callForSpeakerRequest.ToMarkdown(true));
		}
		else
		{
			return "[red]Unsupported output format[/]";
		}
	}

}