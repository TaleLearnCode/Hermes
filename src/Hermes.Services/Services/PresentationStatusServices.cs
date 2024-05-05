namespace Hermes.Services;

public class PresentationStatusServices(string databaseConnectionString) : ServicesBase(databaseConnectionString)
{

	/// <summary>
	/// Retrieves a presentation status by its name.
	/// </summary>
	/// <param name="presentationStatus">The name of the presentation status.</param>
	/// <returns>The presentation status if found, otherwise null.</returns>
	public async Task<PresentationStatus?> GetByNameAsync(string presentationStatus)
			=> await GetFirstOrDefaultAsync<PresentationStatus>(x => x.PresentationStatusName == presentationStatus);

	/// <summary>
	/// Retrieves a list of all presentation statuses.
	/// </summary>
	/// <returns>A list of presentation statuses.</returns>
	public async Task<List<PresentationStatus>> GetListAsync()
		=> [.. (await GetAllAsync<PresentationStatus>()).OrderBy(x => x.SortOrder)];

}