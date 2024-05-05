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

	public static async Task<MarkdownPresentationRequest> BuildPresentationRequestFromMarkdownAsync(string inputPath)
	{

		List<string> markdownLines = [.. (await File.ReadAllLinesAsync(inputPath))];

		StringBuilder elevatorPitch = new();
		StringBuilder shortAbstract = new();
		StringBuilder abstractText = new();
		StringBuilder resources = new();
		StringBuilder additionalDetails = new();
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
					case MarkdownPresentationHeadings.AdditionalDetails:
						additionalDetails.AppendLine(currentMarkdownLine);
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
			OriginalThumbnailUrl = presentationRequest.Thumbnail,
			IsArchived = presentationRequest.IsArchived,
			IncludeInPublicProfile = presentationRequest.IncludeInPublicProfile,
			DefaultLanguageCode = presentationRequest.DefaultLanguageCode,
			Resources = presentationRequest.Resources,
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

		Presentation? presentation = await GetFirstOrDefaultAsync<Presentation>(x => x.Permalink == presentationRequest.Permalink);
		if (presentation is not null)
			throw new ArgumentException($"The presentation with the permalink '{presentationRequest.Permalink}' already exists in the database.");

		if (presentationRequest.Permalink?.Length > 200)
			throw new ArgumentException($"The permalink '{presentationRequest.Permalink}' exceeds the maximum length of 200 characters.");
		if (presentationRequest.Thumbnail?.Length > 200)
			throw new ArgumentException($"The thumbnail URL '{presentationRequest.Thumbnail}' exceeds the maximum length of 200 characters.");
		if (presentationRequest.Resources?.Length > 3000)
			throw new ArgumentException($"The resources '{presentationRequest.Resources}' exceeds the maximum length of 3000 characters.");
		if (presentationRequest.ShortTitle?.Length > 60)
			throw new ArgumentException($"The short title '{presentationRequest.ShortTitle}' exceeds the maximum length of 60 characters.");
		if (presentationRequest.Title?.Length > 300)
			throw new ArgumentException($"The title '{presentationRequest.Title}' exceeds the maximum length of 300 characters.");
		if (presentationRequest.ElevatorPitch?.Length > 160)
			throw new ArgumentException($"The elevator pitch '{presentationRequest.ElevatorPitch}' exceeds the maximum length of 160 characters.");
		if (presentationRequest.AdditionalDetails?.Length > 3000)
			throw new ArgumentException($"The additional details '{presentationRequest.AdditionalDetails}' exceeds the maximum length of 3000 characters.");
		foreach (string? learningObjective in presentationRequest.LearningObjectives)
			if (learningObjective.Length > 1000)
				throw new ArgumentException($"The learning objective '{learningObjective}' exceeds the maximum length of 1000 characters.");
		foreach (string? tag in presentationRequest.Tags)
			if (tag.Length > 100)
				throw new ArgumentException($"The tag '{tag}' exceeds the maximum length of 100 characters.");

		if (!Uri.TryCreate(presentationRequest.PublicRepoLink, UriKind.Absolute, out Uri? publicRepoLinkUri))
			throw new ArgumentException($"The public repository link '{presentationRequest.PublicRepoLink}' is not a valid URL.");
		if (!Uri.TryCreate(presentationRequest.PrivateRepoLink, UriKind.Absolute, out Uri? privateRepoLinkUri))
			throw new ArgumentException($"The private repository link '{presentationRequest.PrivateRepoLink}' is not a valid URL.");
		if (!Uri.TryCreate(presentationRequest.Thumbnail, UriKind.Absolute, out Uri? thumbnailUri))
			throw new ArgumentException($"The thumbnail URL '{presentationRequest.Thumbnail}' is not a valid URL.");

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