
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
	public async Task<string> AddPresentationAsync(string inputPath, string? outputPath)
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
}