namespace Hermes.Services;

public class PresentationTypeServices(string databaseConnectionString) : ServicesBase(databaseConnectionString)
{

	/// <summary>
	/// Retrieves a presentation type by its name.
	/// </summary>
	/// <param name="presentationType">The name of the presentation type.</param>
	/// <returns>The presentation type if found, otherwise null.</returns>
	public async Task<PresentationType?> GetByNameAsync(string presentationType)
					=> await GetFirstOrDefaultAsync<PresentationType>(x => x.PresentationTypeName == presentationType);

	/// <summary>
	/// Retrieves a list of all presentation types.
	/// </summary>
	/// <returns>A list of presentation types.</returns>
	public async Task<List<PresentationType>> GetListAsync() => await GetAllAsync<PresentationType>();

}
