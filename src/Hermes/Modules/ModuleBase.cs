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

	protected static bool IsInputPathValid(string inputPath)
	{
		if (!Directory.Exists(inputPath))
		{
			AnsiConsole.MarkupLine($"[red]The specified input path [/][bold red]'{inputPath}'[/][red] does not exist.[/]");
			return false;
		}
		return true;
	}

	protected static bool IsOutputPathValid(string? outputPath)
	{
		if (!string.IsNullOrWhiteSpace(outputPath)
			&& File.Exists(outputPath)
			&& !AnsiConsole.Confirm("The specified output path exists; do you want to overwrite?"))
			return false;
		return true;
	}

	protected static InputOutputFormat GetOutputFormat(
		string? outputPath,
		string? outputFormatOption)
	{
		if (outputFormatOption is not null && !Enum.TryParse<InputOutputFormat>(outputFormatOption, true, out InputOutputFormat outputFormat))
			throw new ArgumentException($"The specified output format '{outputFormatOption}' is invalid. Please specify 'markdown' or 'json'.");
		else if (outputFormatOption is null && !string.IsNullOrWhiteSpace(outputPath))
			outputFormat = Path.GetExtension(outputPath) switch
			{
				".md" => InputOutputFormat.Markdown,
				".json" => InputOutputFormat.Json,
				_ => throw new ArgumentException("The output file format is not supported. Please specify 'markdown' or 'json'.")
			};
		else
			outputFormat = InputOutputFormat.Console;
		return outputFormat;
	}

	protected static bool TryGetInputFiles(string inputPath, out string[] inputFilePaths)
	{
		List<string> response = new();
		string[] directoryFiles = Directory.GetFiles(inputPath, "*.*", SearchOption.TopDirectoryOnly);
		if (directoryFiles.Length == 0)
		{
			inputFilePaths = [];
		}
		else
		{
			foreach (string file in directoryFiles)
				if (file.EndsWith(".json") || file.EndsWith(".md"))
					response.Add(file);
			inputFilePaths = [.. response];
		}
		return inputFilePaths.Length > 0;
	}

	protected static InputOutputFormat GetInputFormat(
		string inputPath,
		string? inputFormatOption)
	{
		if (inputFormatOption is not null && !Enum.TryParse<InputOutputFormat>(inputFormatOption, true, out InputOutputFormat inputFormat))
			throw new ArgumentException($"The specified input format '{inputFormatOption}' is invalid. Please specify 'markdown' or 'json'.");
		else if (inputFormatOption is null)
			inputFormat = Path.GetExtension(inputPath) switch
			{
				".md" => InputOutputFormat.Markdown,
				".json" => InputOutputFormat.Json,
				_ => throw new ArgumentException("The input file format is not supported. Please specify 'markdown' or 'json'.")
			};
		else
			inputFormat = InputOutputFormat.Console;
		return inputFormat;
	}

}