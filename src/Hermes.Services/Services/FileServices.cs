namespace Hermes.Services;

public class FileServices
{

	protected readonly JsonSerializerOptions _jsonSerializerOptions = new()
	{
		Converters = { new JsonStringEnumConverter() },
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
		DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
		WriteIndented = true
	};

	/// <summary>
	/// Saves an item of type `T` as a JSON file at the specified `path`.
	/// </summary>
	/// <typeparam name="T">The type of the object. It must be a reference type and have a parameterless constructor.</typeparam>
	/// <param name="path">The file path where the JSON file will be saved.</param>
	/// <param name="item">The item to be serialized to JSON and saved to the path.</param>
	/// <returns>A message indicating the success of the operation, including the name of the item and the path where the JSON file is saved.</returns>
	/// <exception cref="IOException">If an I/O error occurs while writing the JSON file.</exception>"
	/// <exception cref="ArgumentNullException">If the `path` is `null` or empty.</exception>
	public string SerializeAndSaveFile<T>(string path, T item) where T : class, new()
	{
		ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));
		string json = JsonSerializer.Serialize(item, _jsonSerializerOptions);
		return SaveFile<T>(path, json);
	}

	public static string SaveFile<T>(string path, string contents)
	{
		File.WriteAllText(path, contents);
		return $"'{typeof(T).Name.SplitCamelCase().Capitalize()}' file saved to '{path}'.";
	}


}