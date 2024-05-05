using Hermes.Responses;
using Hermes.Types;
using System.Text;

namespace Hermes.Services;

public class PresentationServices(string databaseConnectionString) : ServicesBase(databaseConnectionString)
{

	/// <summary>
	/// Saves a template object of type `Presentation` as a JSON file at the specified `path`.
	/// </summary>
	/// <param name="path">The file path where the JSON file will be saved.</param>
	/// <returns>A message indicating the success of the operation, including the name of the template object and the path where the JSON file is saved.</returns>
	/// <exception cref="IOException">If an I/O error occurs while writing the JSON file.</exception>
	public async Task<string> GetTemplateAsync(string path)
	{

		List<PresentationType> presentationTypes = await GetWhereAsync<PresentationType>(x => x.IsEnabled == true);
		List<PresentationStatus> presentationStatuses = await GetWhereAsync<PresentationStatus>(x => x.IsEnabled == true);

		PresentationRequest template = new()
		{
			Type = $"Required -  The type of presentation [{string.Join(", ", presentationTypes.Select(x => x.PresentationTypeName))}]",
			Status = $"Required - The status of the presentation [{string.Join(", ", presentationStatuses.Select(x => x.PresentationStatusName))}]",
			PublicRepoLink = "Optional - The public repository link of the presentation",
			PrivateRepoLink = "Optional - The private repository link of the presentation",
			Permalink = "Required - The permalink of the presentation; used as the identifier",
			DefaultLanguageCode = "Optional - The default language code of the presentation; default is en.",
			Texts =
			[
				new() {
					LanguageCode = "Required - The default language code of the presentation",
					Title = "Required - The title of the presentation",
					ShortTitle = "Optional - The short title of the presentation",
					Abstract = "Optional - The abstract of the presentation",
					ShortAbstract = "Optional - The short abstract of the presentation",
					ElevatorPitch = "Optional - The elevator pitch of the presentation",
					AdditionalDetails = "Optional - Additional details of the presentation",
					LearningObjectives =
					[
						new() {
							Text = "Required - The text of the learning objective",
						}
					]
				}
			],
			Tags = ["Optional - one or more tags associated with the presentation."]
		};

		return SerializeAndSaveFile(path, template);
	}

	/// <summary>
	/// Adds a presentation to the database based on the provided input file.
	/// </summary>
	/// <param name="inputPath">The path of the input file containing the presentation details.</param>
	/// <param name="outputPath">The optional path where the JSON file will be saved. If not provided, the file will be saved with the permalink as the file name.</param>
	/// <returns>A message indicating the success of the operation, including the permalink of the added presentation.</returns>
	/// <exception cref="ArgumentException">Thrown when the presentation type, status, default language code, or any required presentation text is invalid or missing.</exception>
	public async Task<string> AddPresentationFromJsonAsync(string inputPath, string? outputPath)
	{

		PresentationRequest presentationRequest = LoadTemplateFromFile<PresentationRequest>(inputPath);

		List<string> languageCodes = (await GetWhereAsync<Language>(x => x.IsEnabled == true)).Select(x => x.LanguageCode).ToList();
		PresentationType presentationType = await GetFirstOrDefaultAsync<PresentationType>(x => x.PresentationTypeName == presentationRequest.Type) ?? throw new ArgumentException($"Invalid presentation type; '{presentationRequest.Type}' was not found in the database.");
		PresentationStatus presentationStatus = await GetFirstOrDefaultAsync<PresentationStatus>(x => x.PresentationStatusName == presentationRequest.Status) ?? throw new ArgumentException($"Invalid presentation status; '{presentationRequest.Status}' was not found in the database.");
		string permalink = GetNewPermalink(presentationRequest);

		await ValidateRequest(presentationRequest, languageCodes, permalink);

		Presentation presentation = await CreateAsync<Presentation>(new()
		{
			PresentationTypeId = presentationType.PresentationTypeId,
			PresentationStatusId = presentationStatus.PresentationStatusId,
			PublicRepoLink = presentationRequest.PublicRepoLink,
			PrivateRepoLink = presentationRequest.PrivateRepoLink,
			Permalink = permalink,
			IsArchived = presentationRequest.IsArchived,
			IncludeInPublicProfile = presentationRequest.IncludeInPublicProfile,
			DefaultLanguageCode = presentationRequest.DefaultLanguageCode,
			PresentationTexts = GetNewPresentationTexts(presentationRequest),
			PresentationTags = await GetNewPresentationTagsAsync(presentationRequest)
		});

		if (outputPath != StaticValues.NoEntryDefault)
			SerializeAndSaveFile(outputPath ?? $"{permalink}.json", presentationRequest);

		return $"The presentation '{presentation.Permalink}' was successfully added to the database.";

	}

	//public async Task<string> AddPresentationFromMarkdownAsync(string inputPath, string? outputPath, MarkdownPresentationRequest markdownPresentationRequest)
	//{
	//	string markdown = File.ReadAllText(inputPath);
	//	if (string.IsNullOrWhiteSpace(markdown))
	//		throw new ArgumentException($"The file '{inputPath}' is empty.", nameof(inputPath));
	//	else if (IsMarkdownValid(markdown))
	//		throw new ArgumentException($"The markdown file '{inputPath}' is invalid.", nameof(inputPath));

	//	List<string> markdownLines = [.. File.ReadAllLines(@"C:\Presentations\ArchitectLikeABoss\README.md")];

	//	string? currentOperation = null;
	//	int presentationsHeaderLinesSkipped = 0;

	//	List<Tag> tags = await GetAllAsync<Tag>();

	//	Presentation presentation = new()
	//	{
	//		PresentationStatus = await GetPresentationStatusAsync(markdownPresentationRequest.Status),
	//		DefaultLanguageCode = markdownPresentationRequest.LanguageCode,
	//		PublicRepoLink = markdownPresentationRequest.PublicRepoLink,
	//		PrivateRepoLink = markdownPresentationRequest.PrivateRepoLink,
	//		IncludeInPublicProfile = markdownPresentationRequest.IncludeInPublicProfile,
	//		IsArchived = markdownPresentationRequest.IsArchived
	//	};
	//	PresentationText presentationText = new() { LanguageCode = markdownPresentationRequest.LanguageCode };

	//	Dictionary<string, Action<string>> operationHandlers = new()
	//	{
	//		{ "## Elevator Pitch", line => presentationText.ElevatorPitch += line.TrimEnd('\n') },
	//		{ "## Short Abstract", line => presentationText.ShortAbstract += line.TrimEnd('\n') },
	//		{ "## Abstract", line => presentationText.Abstract += line.TrimEnd('\n') },
	//		{ "## Type", async line => presentation.PresentationType = await GetPresentationTypeAsync(line.TrimEnd('\n')) },
	//		{ "## Tags", async line => presentation.PresentationTags.Add(await GetNewPresentationTagAsync(line.TrimEnd('\n'))) },
	//		{ "## Learning Objectives", line => presentationText.LearningObjectives.Add(new()
	//		{
	//			LearningObjectiveText = line.TrimEnd('\n'),
	//			SortOrder = presentationText.LearningObjectives.Count + 1
	//		}) },
	//		{ "## Presentations", line => ParseEngagement(line.TrimEnd('\n'), presentation) }
	//	};


	//	foreach (string line in markdownLines)
	//	{
	//		if (line.StartsWith("# "))
	//		{
	//			presentationText.PresentationTitle = line[2..];
	//			if (presentationText.PresentationTitle.Contains(':'))
	//				presentationText.PresentationShortTitle = presentationText.PresentationTitle[..presentationText.PresentationTitle.IndexOf(':')];
	//		}
	//		else if (line.StartsWith('!') && line.Contains("Thumbnail.jpg"))
	//		{
	//			presentation.ThumbnailUrl = line.Substring(line.IndexOf("(") + 1, line.IndexOf(")") - line.IndexOf("(") - 1);
	//		}
	//		else if (!line.StartsWith("##"))
	//		{
	//			if (currentOperation != null && operationHandlers.TryGetValue(currentOperation, out Action<string>? value))
	//				value(line);
	//		}
	//		else if (operationHandlers.ContainsKey(line))
	//		{
	//			currentOperation = line;
	//		}
	//		else
	//		{
	//			currentOperation = null;
	//		}
	//	}

	//	presentation.PresentationTexts.Add(presentationText);
	//	presentation.Permalink = presentationText.PresentationShortTitle.ToKebabCase();

	//	presentation = await CreateAsync(presentation);

	//	if (outputPath != StaticValues.NoEntryDefault)
	//		SerializeAndSaveFile(outputPath ?? $"{presentation.Permalink}.json", presentation.ToPresentationRequest());

	//	return $"The presentation '{presentation.Permalink}' was successfully added to the database.";

	//}

	public async Task<MarkdownPresentationRequest> BuildPresentationRequestFromMarkdownAsync(string inputPath)
	{

		List<string> markdownLines = [.. (await File.ReadAllLinesAsync(inputPath))];

		StringBuilder elevatorPitch = new();
		StringBuilder shortAbstract = new();
		StringBuilder abstractText = new();
		StringBuilder resources = new();
		MarkdownPresentationRequest presentationRequest = new();

		string? currentField = null;
		foreach (string markdownLine in markdownLines)
		{
			string currentMarkdownLine = markdownLine.Trim();
			if (currentMarkdownLine.StartsWith("## "))
			{
				currentField = currentMarkdownLine[3..];
			}
			else
			{
				switch (currentField)
				{
					case MarkdownPresentationHeadings.ElevatorPitch:
						elevatorPitch.AppendLine(currentMarkdownLine);
						break;
					case MarkdownPresentationHeadings.ShortAbstract:
						shortAbstract.AppendLine(currentMarkdownLine);
						break;
					case MarkdownPresentationHeadings.Abstract:
						abstractText.AppendLine(currentMarkdownLine);
						break;
					case MarkdownPresentationHeadings.Resources:
						resources.AppendLine(currentMarkdownLine);
						break;
					case MarkdownPresentationHeadings.Tags:
						if (!string.IsNullOrWhiteSpace(currentMarkdownLine))
							presentationRequest.Tags.Add(GetListItemValue(currentMarkdownLine));
						break;
					case MarkdownPresentationHeadings.LearningObjectives:
						if (!string.IsNullOrWhiteSpace(currentMarkdownLine))
							presentationRequest.LearningObjectives.Add(GetListItemValue(currentMarkdownLine));
						break;
					case MarkdownPresentationHeadings.PresentationAttributes:
						ParsePresentationAttributes(currentMarkdownLine, presentationRequest);
						break;
				}
			}
		}

		if (elevatorPitch.ToString().TrimEnd('r', 'n').Length > 0)
			presentationRequest.ElevatorPitch = elevatorPitch.ToString().TrimEnd('\r', '\n');
		if (shortAbstract.ToString().TrimEnd('r', 'n').Length > 0)
			presentationRequest.ShortAbstract = shortAbstract.ToString().TrimEnd('\r', '\n');
		if (abstractText.ToString().TrimEnd('r', 'n').Length > 0)
			presentationRequest.Abstract = abstractText.ToString().TrimEnd('\r', '\n');
		if (resources.ToString().TrimEnd('r', 'n').Length > 0)
			presentationRequest.Resources = resources.ToString().TrimEnd('\r', '\n');

		return presentationRequest;
	}

	public MarkdownPresentationRequest? BuildPresentationRequestFromJson(string inputPath)
		=> JsonSerializer.Deserialize<MarkdownPresentationRequest>(File.ReadAllText(inputPath), _jsonSerializerOptions);

	public static string GetPermalink(MarkdownPresentationRequest markdownPresentationRequest)
	{
		if (!string.IsNullOrWhiteSpace(markdownPresentationRequest.Permalink))
		{
			return markdownPresentationRequest.Permalink;
		}
		else
		{
			return (!string.IsNullOrWhiteSpace(markdownPresentationRequest.ShortTitle)
			? markdownPresentationRequest.ShortTitle
			: markdownPresentationRequest.Title ?? string.Empty).ToKebabCase();
		}
	}

	public async Task<string> AddPresentation(
		MarkdownPresentationRequest presentationRequest,
		InputOutputFormat inputFormat,
		string? outputPath,
		InputOutputFormat? outputFormat)
	{

		await ValidateRequestAsync(presentationRequest);

		Presentation presentation = await CreateAsync<Presentation>(new()
		{
			PresentationTypeId = (await GetFirstOrDefaultAsync<PresentationType>(x => x.PresentationTypeName == presentationRequest.PresentationType))!.PresentationTypeId,
			PresentationStatusId = (await GetFirstOrDefaultAsync<PresentationStatus>(x => x.PresentationStatusName == presentationRequest.PresentationStatus))!.PresentationStatusId,
			PublicRepoLink = presentationRequest.PublicRepoLink,
			PrivateRepoLink = presentationRequest.PrivateRepoLink,
			Permalink = presentationRequest.Permalink,
			IsArchived = presentationRequest.IsArchived,
			IncludeInPublicProfile = presentationRequest.IncludeInPublicProfile,
			DefaultLanguageCode = presentationRequest.DefaultLanguageCode,
			PresentationTexts = GetNewPresentationTexts(presentationRequest),
			PresentationTags = await GetNewPresentationTagsAsync(presentationRequest)
		});

		outputFormat ??= inputFormat;
		if (!string.IsNullOrWhiteSpace(outputPath) && outputFormat != InputOutputFormat.Console)
		{
			using HermesContext context = new(_databaseConnectionString);
			presentation = await context.Presentations
				.Include(x => x.PresentationType)
				.Include(x => x.PresentationStatus)
				.Include(x => x.PresentationTexts)
					.ThenInclude(x => x.LearningObjectives)
				.Include(x => x.PresentationTags)
					.ThenInclude(x => x.Tag)
				.FirstAsync(x => x.PresentationId == presentation.PresentationId);
			if (outputFormat == InputOutputFormat.Json)
				SerializeAndSaveFile(outputPath ?? $"{presentation.Permalink}.json", presentation.ToPresentationRequest());
			else if (outputFormat == InputOutputFormat.Markdown)
				SaveFile<Presentation>(outputPath ?? $"{presentation.Permalink}.md", presentation.ToMarkdown());

		}

		return $"The presentation '{presentation.Permalink}' was successfully added to the database.";
	}

	/// <summary>
	/// Retrieves a list of presentations based on the specified filters.
	/// </summary>
	/// <param name="outputFormat">The format in which the presentation list should be returned.</param>
	/// <param name="outputPath">The optional path where the presentation list should be saved. If not provided, the list will not be saved.</param>
	/// <param name="presentationType">The optional presentation type filter.</param>
	/// <param name="presentationStatus">The optional presentation status filter.</param>
	/// <returns>A list of PresentationItemResponse objects representing the presentations.</returns>
	public async Task<List<PresentationItemResponse>> GetPresentationListAsync(
		InputOutputFormat outputFormat,
		string? outputPath,
		string? presentationType = null,
		string? presentationStatus = null)
	{

		Expression<Func<Presentation, bool>> predicate;
		if (presentationType is not null && presentationStatus is not null)
			predicate = x => x.PresentationType.PresentationTypeName == presentationType && x.PresentationStatus.PresentationStatusName == presentationStatus;
		else if (presentationType is not null)
			predicate = x => x.PresentationType.PresentationTypeName == presentationType;
		else if (presentationStatus is not null)
			predicate = x => x.PresentationStatus.PresentationStatusName == presentationStatus;
		else
			predicate = x => true;


		using HermesContext context = new(_databaseConnectionString);
		//return await context.Set<T>().Where(predicate).ToListAsync();
		//List<Presentation> presentations = await GetWhereAsync(predicate);
		List<Presentation> presentations = await context.Presentations
			.Include(x => x.PresentationType)
			.Include(x => x.PresentationStatus)
			.Include(x => x.PresentationTexts)
			.Where(predicate)
			.ToListAsync();


		List<PresentationItemResponse> presentationItems = presentations.Select(x => new PresentationItemResponse
		{
			Permalink = x.Permalink,
			Title = x.PresentationTexts.FirstOrDefault(y => y.LanguageCode == x.DefaultLanguageCode)?.PresentationTitle ?? string.Empty,
			Type = x.PresentationType.PresentationTypeName,
			Status = x.PresentationStatus.PresentationStatusName,
			PublicRepoLink = x.PublicRepoLink,
			PrivateRepoLink = x.PrivateRepoLink,
			IncludeInPublicProfile = x.IncludeInPublicProfile,
			DefaultLanguageCode = x.DefaultLanguageCode
		}).ToList();

		if (outputFormat == InputOutputFormat.Json && outputPath is not null)
			SerializeAndSaveFile(outputPath, presentationItems);
		if (outputPath != StaticValues.NoEntryDefault)
			SerializeAndSaveFile(outputPath ?? "presentations.json", presentationItems);

		return presentationItems;

	}

	private async Task<List<PresentationTag>> GetNewPresentationTagsAsync(PresentationRequest presentationRequest)
	{
		List<PresentationTag> presentationTags = [];
		List<Tag> tags = await GetAllAsync<Tag>();
		if (presentationRequest.Tags is not null && presentationRequest.Tags.Count > 0)
		{
			foreach (string tag in presentationRequest.Tags)
			{
				Tag? tagRecord = tags.FirstOrDefault(x => x.TagName == tag);
				int tagId = tagRecord?.TagId ?? (await CreateAsync(new Tag { TagName = tag })).TagId;
				presentationTags.Add(new() { TagId = tagId });
			}
		}
		return presentationTags;
	}

	private async Task<List<PresentationTag>> GetNewPresentationTagsAsync(MarkdownPresentationRequest presentationRequest)
	{
		List<PresentationTag> presentationTags = [];
		List<Tag> tags = await GetAllAsync<Tag>();
		if (presentationRequest.Tags is not null && presentationRequest.Tags.Count > 0)
		{
			foreach (string tag in presentationRequest.Tags)
			{
				Tag? tagRecord = tags.FirstOrDefault(x => x.TagName == tag);
				int tagId = tagRecord?.TagId ?? (await CreateAsync(new Tag { TagName = tag })).TagId;
				presentationTags.Add(new() { TagId = tagId });
			}
		}
		return presentationTags;
	}

	private static List<PresentationText> GetNewPresentationTexts(PresentationRequest presentationRequest)
	{
		List<PresentationText> presentationTexts = [];
		foreach (PresentationTextRequest text in presentationRequest.Texts)
		{
			PresentationText presentationText = new()
			{
				LanguageCode = text.LanguageCode,
				PresentationTitle = text.Title,
				PresentationShortTitle = text.ShortTitle,
				Abstract = text.Abstract,
				ShortAbstract = text.ShortAbstract,
				ElevatorPitch = text.ElevatorPitch,
				AdditionalDetails = text.AdditionalDetails
			};
			foreach (LearningObjectiveRequest learningObjective in text.LearningObjectives)
			{
				presentationText.LearningObjectives.Add(new()
				{
					LearningObjectiveText = learningObjective.Text,
					SortOrder = learningObjective.SortOrder
				});
			}
			presentationTexts.Add(presentationText);
		}
		return presentationTexts;
	}

	private static List<PresentationText> GetNewPresentationTexts(MarkdownPresentationRequest presentationRequest)
	{
		PresentationText presentationText = new()
		{
			LanguageCode = presentationRequest.DefaultLanguageCode,
			PresentationTitle = presentationRequest.Title,
			PresentationShortTitle = presentationRequest.ShortTitle,
			Abstract = (!string.IsNullOrWhiteSpace(presentationRequest.Abstract)) ? presentationRequest.Abstract.ToString() : null,
			ShortAbstract = (!string.IsNullOrWhiteSpace(presentationRequest.ShortAbstract)) ? presentationRequest.ShortAbstract.ToString() : null,
			ElevatorPitch = (!string.IsNullOrWhiteSpace(presentationRequest.ElevatorPitch)) ? presentationRequest.ElevatorPitch.ToString() : null,
			AdditionalDetails = (!string.IsNullOrWhiteSpace(presentationRequest.AdditionalDetails)) ? presentationRequest.AdditionalDetails.ToString() : null
		};
		foreach (string learningObjective in presentationRequest.LearningObjectives)
		{
			presentationText.LearningObjectives.Add(new()
			{
				LearningObjectiveText = learningObjective,
				SortOrder = presentationText.LearningObjectives.Count + 1
			});
		}
		return [presentationText];
	}

	private async Task ValidateRequest(
		PresentationRequest presentationRequest,
		List<string> languageCodes,
		string permalink)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(presentationRequest.Type, nameof(presentationRequest.Type));
		ArgumentException.ThrowIfNullOrWhiteSpace(presentationRequest.Status, nameof(presentationRequest.Status));
		ArgumentException.ThrowIfNullOrWhiteSpace(presentationRequest.DefaultLanguageCode, nameof(presentationRequest.DefaultLanguageCode));
		if (!languageCodes.Contains(presentationRequest.DefaultLanguageCode))
			throw new ArgumentException($"Invalid language code; '{presentationRequest.DefaultLanguageCode}' was not found in the database.");
		if (presentationRequest.Texts is null || presentationRequest.Texts.Count == 0)
			throw new ArgumentException($"At least one presentation text is required.");
		if (presentationRequest.Texts.Any(x => string.IsNullOrWhiteSpace(x.LanguageCode) || string.IsNullOrWhiteSpace(x.Title)))
			throw new ArgumentException($"The language code and title are required for each presentation text.");
		if (presentationRequest.Texts.Any(x => x.LearningObjectives is not null && x.LearningObjectives.Count > 0 && x.LearningObjectives.Any(y => string.IsNullOrWhiteSpace(y.Text))))
			throw new ArgumentException($"The text of the learning objective is required for each learning objective.");

		Presentation presentation = await GetFirstOrDefaultAsync<Presentation>(x => x.Permalink == permalink);
		if (presentation is not null)
			throw new ArgumentException($"The presentation with the permalink '{permalink}' already exists in the database.");

		foreach (PresentationTextRequest text in presentationRequest.Texts)
		{
			if (!languageCodes.Contains(text.LanguageCode))
				throw new ArgumentException($"Invalid language code; '{text.LanguageCode}' was not found in the database.");
			ArgumentException.ThrowIfNullOrWhiteSpace(text.Title, nameof(text.Title));
			foreach (LearningObjectiveRequest objective in text.LearningObjectives)
				ArgumentException.ThrowIfNullOrWhiteSpace(objective.Text, nameof(objective.Text));
		}
	}

	private async Task ValidateRequestAsync(MarkdownPresentationRequest presentationRequest)
	{

		ArgumentException.ThrowIfNullOrWhiteSpace(presentationRequest.PresentationType, nameof(presentationRequest.PresentationType));
		ArgumentException.ThrowIfNullOrWhiteSpace(presentationRequest.PresentationStatus, nameof(presentationRequest.PresentationStatus));
		ArgumentException.ThrowIfNullOrWhiteSpace(presentationRequest.DefaultLanguageCode, nameof(presentationRequest.DefaultLanguageCode));

		if ((await GetFirstOrDefaultAsync<PresentationType>(x => x.PresentationTypeName == presentationRequest.PresentationType)) is null)
			throw new ArgumentException($"Invalid presentation type; '{presentationRequest.PresentationType}' was not found in the database.");
		if ((await GetFirstOrDefaultAsync<PresentationStatus>(x => x.PresentationStatusName == presentationRequest.PresentationStatus)) is null)
			throw new ArgumentException($"Invalid presentation status; '{presentationRequest.PresentationStatus}' was not found in the database.");
		if ((await GetFirstOrDefaultAsync<Language>(x => x.LanguageCode == presentationRequest.DefaultLanguageCode)) is null)
			throw new ArgumentException($"Invalid language code; '{presentationRequest.DefaultLanguageCode}' was not found in the database.");

		Presentation presentation = await GetFirstOrDefaultAsync<Presentation>(x => x.Permalink == presentationRequest.Permalink);
		if (presentation is not null)
			throw new ArgumentException($"The presentation with the permalink '{presentationRequest.Permalink}' already exists in the database.");

	}

	private static string GetNewPermalink(PresentationRequest presentationRequest)
	{
		if ((!string.IsNullOrWhiteSpace(presentationRequest.Permalink)))
		{
			return presentationRequest.Permalink;
		}
		else
		{
			return (!string.IsNullOrWhiteSpace(presentationRequest.Texts.FirstOrDefault(x => x.LanguageCode == presentationRequest.DefaultLanguageCode)?.ShortTitle)
			? presentationRequest.Texts.First().ShortTitle
			: presentationRequest.Texts.First().Title).ToKebabCase();
		}
	}

	private async Task<PresentationTag> GetNewPresentationTagAsync(string tag)
	{
		Tag? tagRecord = await GetFirstOrDefaultAsync<Tag>(x => x.TagName == tag);
		tagRecord ??= await CreateAsync(new Tag { TagName = tag });
		return new PresentationTag { TagId = tagRecord.TagId };
	}

	private async Task<PresentationType> GetPresentationTypeAsync(string presentationType)
	{
		PresentationType? presentationTypeRecord = await GetFirstOrDefaultAsync<PresentationType>(x => x.PresentationTypeName == presentationType)
			?? throw new ArgumentException($"Invalid presentation type; '{presentationType}' was not found in the database.");
		return presentationTypeRecord;
	}

	private async Task<PresentationStatus> GetPresentationStatusAsync(string presentationStatus)
	{
		PresentationStatus? presentationStatusRecord = await GetFirstOrDefaultAsync<PresentationStatus>(x => x.PresentationStatusName == presentationStatus)
			?? throw new ArgumentException($"Invalid presentation status; '{presentationStatus}' was not found in the database.");
		return presentationStatusRecord;
	}

	private static string GetListItemValue(string markdownLine)
	=> markdownLine.StartsWith("- ") ? markdownLine[2..] : markdownLine;

	private static void ParsePresentationAttributes(string markdownLine, MarkdownPresentationRequest presentation)
	{
		string[] tableRow = markdownLine.Split("|");

		if (markdownLine.StartsWith(MarkdownPresentationAttributes.Permalink, StringComparison.OrdinalIgnoreCase))
			presentation.Permalink = GetAttributeValue(tableRow);
		else if (markdownLine.StartsWith(MarkdownPresentationAttributes.Title, StringComparison.OrdinalIgnoreCase))
			presentation.Title = GetAttributeValue(tableRow);
		else if (markdownLine.StartsWith(MarkdownPresentationAttributes.ShortTitle, StringComparison.OrdinalIgnoreCase))
			presentation.ShortTitle = GetAttributeValue(tableRow);
		else if (markdownLine.StartsWith(MarkdownPresentationAttributes.PresentationType, StringComparison.OrdinalIgnoreCase))
			presentation.PresentationType = GetAttributeValue(tableRow);
		else if (markdownLine.StartsWith(MarkdownPresentationAttributes.PresentationStatus, StringComparison.OrdinalIgnoreCase))
			presentation.PresentationStatus = GetAttributeValue(tableRow);
		else if (markdownLine.StartsWith(MarkdownPresentationAttributes.PublicRepoLink, StringComparison.OrdinalIgnoreCase))
			presentation.PublicRepoLink = GetAttributeValue(tableRow);
		else if (markdownLine.StartsWith(MarkdownPresentationAttributes.PrivateRepoLink, StringComparison.OrdinalIgnoreCase))
			presentation.PrivateRepoLink = GetAttributeValue(tableRow);
		else if (markdownLine.StartsWith(MarkdownPresentationAttributes.Thumbnail, StringComparison.OrdinalIgnoreCase))
			presentation.Thumbnail = GetAttributeValue(tableRow);
		else if (markdownLine.StartsWith(MarkdownPresentationAttributes.IncludeInPublicProfile, StringComparison.OrdinalIgnoreCase))
			presentation.IncludeInPublicProfile = GetAttributeBoolValue(tableRow);
		else if (markdownLine.StartsWith(MarkdownPresentationAttributes.DefaultLanguageCode, StringComparison.OrdinalIgnoreCase))
			presentation.DefaultLanguageCode = GetAttributeValue(tableRow);
		else if (markdownLine.StartsWith(MarkdownPresentationAttributes.IsArchived, StringComparison.OrdinalIgnoreCase))
			presentation.IsArchived = GetAttributeBoolValue(tableRow);
	}

	private static string GetAttributeValue(string[] tableRow) => tableRow.Length > 2 ? tableRow[2].Trim() : string.Empty;

	private static bool GetAttributeBoolValue(string[] tableRow)
	{
		if (tableRow.Length > 2 && bool.TryParse(tableRow[2], out bool attributeValue))
			return attributeValue;

		return false;
	}

}