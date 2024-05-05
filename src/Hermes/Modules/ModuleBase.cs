namespace Hermes.Modules;

internal abstract class ModuleBase
{

	protected InputOutputFormat DetermineOutputFormat(string? outputFormatOption, InputOutputFormat defaultOutputFormat, string? outputPath)
	{
		if (string.IsNullOrEmpty(outputFormatOption))
		{
			if (!string.IsNullOrWhiteSpace(outputPath))
			{
				return Path.GetExtension(outputPath).ToLowerInvariant() switch
				{
					".json" => InputOutputFormat.Json,
					".md" => InputOutputFormat.Markdown,
					_ => throw new ArgumentException("The output file format is not supported. Please specify 'markdown' or 'json'."),
				};
			}
			else
			{
				return defaultOutputFormat;
			}
		}
		else
		{
			return outputFormatOption.ToLowerInvariant() switch
			{
				"json" => InputOutputFormat.Json,
				"markdown" => InputOutputFormat.Markdown,
				_ => throw new ArgumentException("The output file format is not supported. Please specify 'markdown' or 'json'."),
			};
		}
	}

	protected string DetermineOutputPath(string? outputPath, InputOutputFormat outputFormat, string defaultOutputPath)
	{
		if (!string.IsNullOrWhiteSpace(outputPath))
			return outputPath;
		else
		{
			return outputFormat switch
			{
				InputOutputFormat.Json => $"{defaultOutputPath}.json",
				InputOutputFormat.Markdown => $"{defaultOutputPath}.md",
				_ => throw new ArgumentException("The output file format is not supported. Please specify 'markdown' or 'json'."),
			};
		}
	}

	protected bool VerifyIfOverwrite(string outputPath)
		=> !File.Exists(outputPath)
		|| AnsiConsole.Confirm("The specified output file already exists. Do you want to overwrite it?");

}