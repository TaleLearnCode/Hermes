namespace Hermes.Services;

public class CallForSpeakerServices(string databaseConnectionString) : ServicesBase(databaseConnectionString)
{

	public async Task<List<CallForSpeakerStatus>> GetStatusesAsync()
		=> [.. (await GetAllAsync<CallForSpeakerStatus>()).OrderBy(x => x.SortOrder)];

}