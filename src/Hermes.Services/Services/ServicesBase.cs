using Markdig;

namespace Hermes.Services;

public abstract class ServicesBase
{

	protected readonly JsonSerializerOptions _jsonSerializerOptions = new()
	{
		Converters = { new JsonStringEnumConverter() },
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
		WriteIndented = true
	};

	protected readonly string _databaseConnectionString;

	protected ServicesBase(string databaseConnectionString)
	{
		_databaseConnectionString = databaseConnectionString;
	}

	/// <summary>
	/// Saves a template object of type `T` as a JSON file at the specified `path`.
	/// </summary>
	/// <typeparam name="T">The type of the template object. It must be a reference type and have a parameterless constructor.</typeparam>
	/// <param name="path">The file path where the JSON file will be saved.</param>
	/// <returns>A message indicating the success of the operation, including the name of the template object and the path where the JSON file is saved.</returns>
	/// <exception cref="IOException">If an I/O error occurs while writing the JSON file.</exception>"
	/// <exception cref="ArgumentNullException">If the `path` is `null` or empty.</exception>
	protected string SaveTemplateToFile<T>(string path) where T : class, new()
		=> SerializeAndSaveFile<T>(path, new T());

	/// <summary>
	/// Saves an item of type `T` as a JSON file at the specified `path`.
	/// </summary>
	/// <typeparam name="T">The type of the object. It must be a reference type and have a parameterless constructor.</typeparam>
	/// <param name="path">The file path where the JSON file will be saved.</param>
	/// <param name="item">The item to be serialized to JSON and saved to the path.</param>
	/// <returns>A message indicating the success of the operation, including the name of the item and the path where the JSON file is saved.</returns>
	/// <exception cref="IOException">If an I/O error occurs while writing the JSON file.</exception>"
	/// <exception cref="ArgumentNullException">If the `path` is `null` or empty.</exception>
	protected string SerializeAndSaveFile<T>(string path, T item) where T : class, new()
	{
		ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));
		string json = JsonSerializer.Serialize(item, _jsonSerializerOptions);
		return SaveFile<T>(path, json);
	}

	protected static string SaveFile<T>(string path, string contents)
	{
		File.WriteAllText(path, contents);
		return $"'{typeof(T).Name.SplitCamelCase().Capitalize()}' file saved to '{path}'.";
	}

	protected T LoadTemplateFromFile<T>(string inputPath)
	{
		ArgumentException.ThrowIfNullOrEmpty(inputPath, nameof(inputPath));
		string json = File.ReadAllText(inputPath);
		if (string.IsNullOrWhiteSpace(json))
			throw new ArgumentException($"The file '{inputPath}' is empty.", nameof(inputPath));
		return JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions)!;
	}

	protected async Task<T> CreateAsync<T>(T entity) where T : class
	{
		using HermesContext context = new(_databaseConnectionString);
		await context.Set<T>().AddAsync(entity);
		await context.SaveChangesAsync();
		return entity;
	}

	protected async Task<T> UpdateAsync<T>(T entity) where T : class
	{
		using HermesContext context = new(_databaseConnectionString);
		context.Set<T>().Update(entity);
		await context.SaveChangesAsync();
		return entity;
	}

	protected async Task DeleteAsync<T>(T entity) where T : class
	{
		using HermesContext context = new(_databaseConnectionString);
		context.Set<T>().Remove(entity);
		await context.SaveChangesAsync();
	}

	protected async Task DeleteRangeAsync<T>(IEnumerable<T> entities) where T : class
	{
		using HermesContext context = new(_databaseConnectionString);
		context.Set<T>().RemoveRange(entities);
		await context.SaveChangesAsync();
	}

	protected async Task<T?> GetAsync<T>(int id) where T : class
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.Set<T>().FindAsync(id);
	}

	protected async Task<List<T>> GetAllAsync<T>() where T : class
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.Set<T>().ToListAsync();
	}

	protected async Task<List<T>> GetWhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.Set<T>().Where(predicate).ToListAsync();
	}

	protected async Task<T?> GetFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.Set<T>().FirstOrDefaultAsync(predicate);
	}

	protected async Task<int> CountAsync<T>() where T : class
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.Set<T>().CountAsync();
	}

	protected async Task<int> CountWhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.Set<T>().CountAsync(predicate);
	}

	protected async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.Set<T>().AnyAsync(predicate);
	}

	protected static bool IsMarkdownValid(string markdown)
	{
		try
		{
			Markdown.Parse(markdown);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

}