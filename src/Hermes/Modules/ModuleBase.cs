namespace Hermes.Modules;

internal abstract class ModuleBase
{

	protected InputOutputFormat DetermineFileFormat(
		string? fileFormatOption,
		InputOutputFormat defaultFileFormat,
		string? filePath,
		bool fileFormatOptional = false)
	{
		if (string.IsNullOrEmpty(fileFormatOption))
		{
			if (!string.IsNullOrWhiteSpace(filePath))
			{
				return Path.GetExtension(filePath).ToLowerInvariant() switch
				{
					".json" => InputOutputFormat.Json,
					".md" => InputOutputFormat.Markdown,
					_ => throw new ArgumentException("The file format is not supported. Please specify 'markdown' or 'json'."),
				};
			}
			else
			{
				return defaultFileFormat;
			}
		}
		else
		{
			return fileFormatOption.ToLowerInvariant() switch
			{
				"json" => InputOutputFormat.Json,
				"markdown" => InputOutputFormat.Markdown,
				_ => (fileFormatOptional) ? InputOutputFormat.Console : throw new ArgumentException("The file format is not supported. Please specify 'markdown' or 'json'."),
			};
		}
	}

	protected string DetermineFilePath(string? outputPath, InputOutputFormat outputFormat, string defaultOutputPath)
	{
		if (!string.IsNullOrWhiteSpace(outputPath))
			return outputPath;
		else
		{
			return outputFormat switch
			{
				InputOutputFormat.Json => $"{defaultOutputPath}.json",
				InputOutputFormat.Markdown => $"{defaultOutputPath}.md",
				_ => throw new ArgumentException("The file format is not supported. Please specify 'markdown' or 'json'."),
			};
		}
	}

	protected bool VerifyIfOverwrite(string outputPath)
		=> !File.Exists(outputPath)
		|| AnsiConsole.Confirm("The specified output file already exists. Do you want to overwrite it?");

}