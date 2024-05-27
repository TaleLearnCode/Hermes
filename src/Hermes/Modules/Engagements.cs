﻿namespace Hermes.Modules;

internal class Engagements(string databaseConnectionString) : ModuleBase
{

	private readonly EngagementServices _engagementServices = new(databaseConnectionString);

	internal void Initialize(RootCommand rootCommand)
	{

		Command engagementCommand = new("engagements", "Work with the 'Engagements' module.");
		rootCommand.AddCommand(engagementCommand);

		InitializeAdd(engagementCommand);

	}

	#region Add Engagement(s)

	private void InitializeAdd(Command engagementCommand)
	{
		Option<string> inputOption = new(new[] { "--input", "-i" }, "Path to the input file.");
		Option<string> inputFormatOption = new(new[] { "--inputFormat", "-if" }, "The format of the input file. Must be 'markdown' or 'json'.");
		Option<string> outputOption = new(new[] { "--output", "-o" }, "Path to the output file.");
		Option<string> outputFormatOption = new(new[] { "--outputFormat", "-of" }, "The format of the output file. Must be 'markdown' or 'json'.");

		Command addCommand = new("add", "Adds a call for speakers.");
		addCommand.AddOption(inputOption);
		addCommand.AddOption(inputFormatOption);
		addCommand.AddOption(outputOption);
		addCommand.AddOption(outputFormatOption);
		engagementCommand.AddCommand(addCommand);
		addCommand.SetHandler(AddEngagementAsync, inputOption, inputFormatOption, outputOption, outputFormatOption);
	}

	private async Task AddEngagementAsync(string inputPath, string? inputFormatOption, string? outputPath, string? outputFormatOption)
	{
		try
		{
			if (!File.Exists(inputPath))
			{
				if (Directory.Exists(inputPath))
					await AddEngagementsAsync(inputPath, inputFormatOption, outputPath, outputFormatOption);
				else
					AnsiConsole.MarkupLine($"[red]The specified input file or directory does not exist.[/]");
				return;
			}

			InputOutputFormat inputFormat = DetermineFileFormat(inputFormatOption, InputOutputFormat.Console, inputPath);
			if (inputFormat == InputOutputFormat.Console)
				throw new ArgumentException("The input file format is not supported. Please specify 'markdown' or 'json'.");
			inputPath = DetermineFilePath(inputPath, inputFormat, "engagement");
			InputOutputFormat outputFormat = DetermineFileFormat(outputFormatOption, inputFormat, outputPath, true);
			AnsiConsole.WriteLine(await _engagementServices.AddEngagementAsync(inputPath, inputFormat, outputPath, outputFormat));

		}
		catch (ArgumentException ex)
		{
			AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
		}
		catch (Exception ex)
		{
			AnsiConsole.WriteException(ex,
				ExceptionFormats.ShortenPaths
				| ExceptionFormats.ShortenTypes
				| ExceptionFormats.ShortenMethods
				| ExceptionFormats.ShortenEverything
				| ExceptionFormats.NoStackTrace);
		}
	}

	private async Task AddEngagementsAsync(string inputPath, string? inputFormatOption, string outputPath, string? outputFormatOption)
	{
		try
		{
			if (!IsInputPathValid(inputPath)) return;
			if (!IsOutputPathValid(outputPath)) return;
			InputOutputFormat? outputFormat = GetOutputFormat(outputPath, outputFormatOption);
			if (!TryGetInputFiles(inputPath, out string[] inputFilePaths)) return;
			foreach (string inputFilePath in inputFilePaths)
			{
				AnsiConsole.MarkupLine($"[bold]Processing file:[/] {inputFilePath}");
				try
				{
					InputOutputFormat inputFormat = GetInputFormat(inputFilePath, inputFormatOption);
					AnsiConsole.WriteLine(await _engagementServices.AddEngagementAsync(inputFilePath, inputFormat, outputPath, outputFormat));
				}
				catch (ArgumentException ex)
				{
					AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
				}
				catch (Exception ex)
				{
					AnsiConsole.WriteException(ex,
						ExceptionFormats.ShortenPaths
						| ExceptionFormats.ShortenTypes
						| ExceptionFormats.ShortenMethods
						| ExceptionFormats.ShortenEverything
						| ExceptionFormats.NoStackTrace);
				}
			}
		}
		catch (ArgumentException ex)
		{
			AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
		}
		catch (Exception ex)
		{
			AnsiConsole.WriteException(ex,
				ExceptionFormats.ShortenPaths
				| ExceptionFormats.ShortenTypes
				| ExceptionFormats.ShortenMethods
				| ExceptionFormats.ShortenEverything
				| ExceptionFormats.NoStackTrace);
		}
	}

	#endregion

}