namespace Hermes.Services;

public class LanguageServices(string databaseConnectionString) : ServicesBase(databaseConnectionString)
{

	/// <summary>
	/// Retrieves a language by its code.
	/// </summary>
	/// <param name="languageCode">The language code.</param>
	/// <returns>The language object if found, otherwise null.</returns>
	public async Task<Language?> GetByCodeAsync(string languageCode)
		=> await GetFirstOrDefaultAsync<Language>(x => x.LanguageCode == languageCode);

	/// <summary>
	/// Retrieves a list of all languages.
	/// </summary>
	/// <returns>A list of all languages.</returns>
	public async Task<List<Language>> GetListAsync() => await GetAllAsync<Language>();

}