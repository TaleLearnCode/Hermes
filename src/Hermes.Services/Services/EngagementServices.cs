﻿namespace Hermes.Services;

public class EngagementServices(string databaseConnectionString) : ServicesBase(databaseConnectionString)
{

	public async Task<string> AddEngagementAsync(
		EngagementRequest engagementRequest,
		InputOutputFormat inputFormat,
		string? outputPath,
		InputOutputFormat? outputFormat)
	{
		await ValidateRequestAsync(engagementRequest, true);
		Engagement engagement = await CreateAsync(await ConstructNewEngagement(engagementRequest));
		await SaveOutputAsync(engagement, engagementRequest.Permalink, outputPath, inputFormat, outputFormat);
		return $"The engagement with the permalink '{engagement.Permalink}' has been added.";
	}

	public async Task<string> AddEngagementAsync(
		string inputFilePath,
		InputOutputFormat inputFormat,
		string? outputPath,
		InputOutputFormat? outputFormat)
		=> await AddEngagementAsync(
			ParseFileToEngagementRequest(inputFilePath, inputFormat)!, inputFormat, outputPath, outputFormat);

	public async Task<string> AddEngagementPresentationDownloadAsync(
		string engagementPermalink,
		string presentationPermalink,
		string downloadType,
		string downloadName,
		string? downloadPath)
	{
		Engagement engagement = await GetEngagementAsync(engagementPermalink);
		EngagementPresentation engagementPresentation = engagement.EngagementPresentations.FirstOrDefault(p => p.PresentationId == presentationPermalink)
			?? throw new ArgumentException($"The presentation with the permalink '{presentationPermalink}' does not exist.");
		DownloadType downloadTypeObject = await GetFirstOrDefaultAsync<DownloadType>(dt => dt.DownloadTypeName == downloadType)
			?? throw new ArgumentException($"The download type '{downloadType}' does not exist.");
		engagementPresentation.EngagementPresentationDownloads.Add(new()
		{
			DownloadTypeId = downloadTypeObject.DownloadTypeId,
			DownloadName = downloadName,
			DownloadPath = (!string.IsNullOrWhiteSpace(downloadPath)) ? downloadPath : null
		});
		await UpdateAsync(engagement);
		return $"The download '{downloadName}' has been added to the presentation with the permalink '{presentationPermalink}' for the engagement with the permalink '{engagementPermalink}'.";
	}

	public async Task<List<DownloadType>> GetDownloadTypesAsync() => await GetAllAsync<DownloadType>();

	public async Task<Dictionary<string, string>> GetEngagementsForYearAsync(int year)
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.Engagements
			.Where(e => e.StartDate.Year == year)
			.OrderBy(e => e.EngagementName)
			.ToDictionaryAsync(e => e.Permalink, e => e.EngagementName);
	}

	public async Task<List<int>> GetEngagementYearsAsync()
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.Engagements
			.Select(e => e.StartDate.Year)
			.Distinct()
			.OrderBy(y => y)
			.ToListAsync();
	}

	public async Task<Dictionary<string, string>> GetEngagementPresentationsAsync(string engagementPermalink)
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.EngagementPresentations
			.Where(x => x.EngagementId == engagementPermalink)
			.ToDictionaryAsync(x => x.PresentationId, x => x.PresentationTitle);
	}

	private async Task<Engagement> GetEngagementAsync(string permalink)
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.Engagements
			.Include(e => e.EngagementType)
			.Include(e => e.EngagementStatus)
			.Include(e => e.EngagementCallForSpeaker)
				.ThenInclude(c => c.EngagementCallForSpeakerStatus)
			.Include(e => e.EngagementPresentations)
				.ThenInclude(p => p.EngagementPresentationStatus)
			.Include(e => e.EngagementPresentations)
				.ThenInclude(p => p.EngagementPresentationLearningObjectives)
			.Include(e => e.EngagementPresentations)
				.ThenInclude(p => p.EngagementPresentationTags)
					.ThenInclude(t => t.Tag)
			.Include(e => e.EngagementPresentations)
				.ThenInclude(p => p.EngagementPresentationDownloads)
					.ThenInclude(d => d.DownloadType)
			.Include(e => e.CountryCodeNavigation)
			.Include(e => e.CountryDivision)
			.Include(e => e.TimeZone)
			.FirstOrDefaultAsync(e => e.Permalink == permalink)
			?? throw new ArgumentException($"The engagement with the permalink '{permalink}' does not exist.");
	}

	private async Task SaveOutputAsync(
	Engagement? engagement,
	string permalink,
	string? outputPath,
	InputOutputFormat inputFormat,
	InputOutputFormat? outputFormat)
	{
		outputFormat ??= inputFormat;
		if (!string.IsNullOrWhiteSpace(outputPath) && outputFormat != InputOutputFormat.Console)
		{
			engagement ??= await GetEngagementAsync(permalink);
			if (engagement is not null)
			{
				if (outputFormat == InputOutputFormat.Json)
					SerializeAndSaveFile(outputPath ?? $"{permalink}.json", engagement.ToResponse());
				else if (outputFormat == InputOutputFormat.Markdown)
					SaveFile<Engagement>(outputPath ?? $"{permalink}.md", engagement.ToMarkdown());
			}
		}
	}

	private async Task<Engagement> ConstructNewEngagement(EngagementRequest engagementRequest)
	{
		Engagement engagementConstruction = new()
		{
			Permalink = engagementRequest.Permalink,
			EngagementTypeId = (await GetFirstOrDefaultAsync<EngagementType>(et => et.EngagementTypeName == engagementRequest.EngagementType))!.EngagementTypeId,
			EngagementStatusId = (await GetFirstOrDefaultAsync<EngagementStatus>(es => es.EngagementStatusName == engagementRequest.Status))!.EngagementStatusId,
			EngagementName = engagementRequest.Name,
			CountryCode = engagementRequest.CountryCode,
			CountryDivisionCode = (!string.IsNullOrWhiteSpace(engagementRequest.CountryDivisionCode)) ? engagementRequest.CountryDivisionCode : null,
			City = engagementRequest.City,
			Venue = (!string.IsNullOrWhiteSpace(engagementRequest.Venue)) ? engagementRequest.Venue : null,
			OverviewLocation = (!string.IsNullOrWhiteSpace(engagementRequest.OverviewLocation)) ? engagementRequest.OverviewLocation : null,
			ListingLocation = (!string.IsNullOrWhiteSpace(engagementRequest.ListingLocation)) ? engagementRequest.ListingLocation : null,
			StartDate = engagementRequest.StartDate!.Value,
			EndDate = engagementRequest.EndDate!.Value,
			TimeZoneId = engagementRequest.TimeZone,
			LanguageCode = engagementRequest.LanguageCode,
			StartingCost = (!string.IsNullOrWhiteSpace(engagementRequest.StartingCost)) ? engagementRequest.StartingCost : null,
			EndingCost = (!string.IsNullOrWhiteSpace(engagementRequest.EndingCost)) ? engagementRequest.EndingCost : null,
			EngagementDescription = (!string.IsNullOrWhiteSpace(engagementRequest.Description)) ? engagementRequest.Description : null,
			EngagementSummary = (!string.IsNullOrWhiteSpace(engagementRequest.Summary)) ? engagementRequest.Summary : null,
			EngagementUrl = (!string.IsNullOrWhiteSpace(engagementRequest.Url)) ? engagementRequest.Url : null,
			IncludeInPublicProfile = engagementRequest.IncludeInPublicProfile,
			IsVirtual = engagementRequest.IsVirtual,
			IsHybrid = engagementRequest.IsHybrid,
			IsPublic = engagementRequest.IsPublic,
			IsEnabled = true
		};

		if (engagementRequest.CallForSpeakerDetails is not null)
		{
			engagementConstruction.EngagementCallForSpeaker = new()
			{
				EngagementPermalink = engagementRequest.Permalink,
				EngagementCallForSpeakerStatusId = (await GetFirstOrDefaultAsync<EngagementCallForSpeakerStatus>(x => x.EngagementCallForSpeakerStatusName == engagementRequest.CallForSpeakerDetails.Status))!.EngagementCallForSpeakerStatusId,
				CallForSpeakersUrl = (!string.IsNullOrWhiteSpace(engagementRequest.CallForSpeakerDetails.Url)) ? engagementRequest.CallForSpeakerDetails.Url : null,
				StartDate = engagementRequest.CallForSpeakerDetails.StartDate!.Value,
				EndDate = engagementRequest.CallForSpeakerDetails.EndDate!.Value,
				ExpectedDecisionDate = (engagementRequest.CallForSpeakerDetails.ExpectedDecisionDate is not null) ? engagementRequest.CallForSpeakerDetails.ExpectedDecisionDate : engagementRequest.CallForSpeakerDetails.EndDate!.Value.AddDays(42),
				ActualDecisionDate = engagementRequest.CallForSpeakerDetails.ActualDecisionDate,
				SpeakerHonorarium = engagementRequest.CallForSpeakerDetails.IncludesSpeakerHonorarium,
				SpeakerHonorariumAmount = engagementRequest.CallForSpeakerDetails.SpeakerHonorariumAmount,
				SpeakerHonorariumCurrency = (!string.IsNullOrWhiteSpace(engagementRequest.CallForSpeakerDetails.SpeakerHonorariumCurrencyCode)) ? engagementRequest.CallForSpeakerDetails.SpeakerHonorariumCurrencyCode : null,
				SpeakerHonorariumNotes = (!string.IsNullOrWhiteSpace(engagementRequest.CallForSpeakerDetails.SpeakerHonorariumNotes)) ? engagementRequest.CallForSpeakerDetails.SpeakerHonorariumNotes : null,
				TravelExpensesCovered = engagementRequest.CallForSpeakerDetails.IncludesTravelExpenses,
				TravelExpensesNotes = (!string.IsNullOrWhiteSpace(engagementRequest.CallForSpeakerDetails.TravelExpensesNotes)) ? engagementRequest.CallForSpeakerDetails.TravelExpensesNotes : null,
				AccommodationCovered = engagementRequest.CallForSpeakerDetails.IncludesAccommodation,
				AccommodationNotes = (!string.IsNullOrWhiteSpace(engagementRequest.CallForSpeakerDetails.AccommodationNotes)) ? engagementRequest.CallForSpeakerDetails.AccommodationNotes : null,
				EventFeeCovered = engagementRequest.CallForSpeakerDetails.EventFeeCovered,
				EventFeeNotes = (!string.IsNullOrWhiteSpace(engagementRequest.CallForSpeakerDetails.EventFeeNotes)) ? engagementRequest.CallForSpeakerDetails.EventFeeNotes : null,
				SubmissionLimit = engagementRequest.CallForSpeakerDetails.SubmissionLimit
			};
		}

		foreach (EngagementPresentationRequest presentationRequest in engagementRequest.Presentations)
		{

			string presentationTitle = presentationRequest.Title;
			string? presentationShortTitle = (!string.IsNullOrWhiteSpace(presentationRequest.PresentationShortTitle)) ? presentationRequest.PresentationShortTitle : null;
			string? abstractDescription = presentationRequest.Abstract;
			string? shortAbstract = (!string.IsNullOrWhiteSpace(presentationRequest.ShortAbstract)) ? presentationRequest.ShortAbstract : null;
			string? elevatorPitch = (!string.IsNullOrWhiteSpace(presentationRequest.ElevatorPitch)) ? presentationRequest.ElevatorPitch : null;
			string? additionalDetails = (!string.IsNullOrWhiteSpace(presentationRequest.AdditionalDetails)) ? presentationRequest.AdditionalDetails : null;

			EngagementPresentation engagementPresentation = new()
			{
				EngagementId = engagementConstruction.Permalink,
				PresentationId = presentationRequest.PresentationPermalink,
				EngagementPresentationStatusId = (await GetFirstOrDefaultAsync<EngagementPresentationStatus>(x => x.EngagementPresentationStatusName == presentationRequest.Status))!.EngagementPresentationStatusId,
				StartDateTime = presentationRequest.StartDateTime,
				PresentationLength = presentationRequest.PresentationLength,
				Room = (!string.IsNullOrWhiteSpace(presentationRequest.Room)) ? presentationRequest.Room : null,
				EngagementPresentationUrl = (!string.IsNullOrWhiteSpace(presentationRequest.EngagementPresentationUrl)) ? presentationRequest.EngagementPresentationUrl : null,
				LangaugeCode = presentationRequest.LanguageCode,
				PresentationTitle = presentationTitle,
				PresentationShortTitle = (!string.IsNullOrWhiteSpace(presentationShortTitle)) ? presentationShortTitle : null,
				Abstract = (!string.IsNullOrWhiteSpace(abstractDescription)) ? abstractDescription : null,
				ShortAbstract = (!string.IsNullOrWhiteSpace(shortAbstract)) ? shortAbstract : null,
				ElevatorPitch = (!string.IsNullOrWhiteSpace(elevatorPitch)) ? elevatorPitch : null,
				AdditionalDetails = (!string.IsNullOrWhiteSpace(additionalDetails)) ? additionalDetails : null,
				IsVirtual = presentationRequest.IsVirtual,
				IsEnabled = true
			};

			if (presentationRequest.Downloads.Count > 0)
			{
				foreach (EngagementPresentationDownloadRequest download in presentationRequest.Downloads)
				{
					engagementPresentation.EngagementPresentationDownloads.Add(new()
					{
						DownloadTypeId = (await GetFirstOrDefaultAsync<DownloadType>(x => x.DownloadTypeName == download.Type))!.DownloadTypeId,
						DownloadName = download.Name,
						DownloadPath = download.Path
					});
				}
			}

			engagementConstruction.EngagementPresentations.Add(engagementPresentation);

		}

		return engagementConstruction;
	}

	private async Task ValidateRequestAsync(EngagementRequest engagementRequest, bool checkPermalinkForUniqueness)
	{
		ValidateEngagementValues(engagementRequest);
		ValidateCallForSpeakersValues(engagementRequest);
		ValidateEngagementPresentationValues(engagementRequest);
		if (checkPermalinkForUniqueness) await CheckEngagementPermalinkForUniqueness(engagementRequest);
		await ValidateEngagementReferencedDataAsync(engagementRequest);
		await ValidateCallForSpeakersRefencedDataAsync(engagementRequest);
		await ValidateEngagementPresentationsReferencedDataAsync(engagementRequest);
	}

	private static void ValidateEngagementValues(EngagementRequest engagementRequest)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(engagementRequest.EngagementType, nameof(engagementRequest.EngagementType));
		ArgumentException.ThrowIfNullOrWhiteSpace(engagementRequest.Status, nameof(engagementRequest.Status));
		ArgumentException.ThrowIfNullOrWhiteSpace(engagementRequest.Name, nameof(engagementRequest.Name));
		ArgumentException.ThrowIfNullOrWhiteSpace(engagementRequest.CountryCode, nameof(engagementRequest.CountryCode));
		ArgumentException.ThrowIfNullOrWhiteSpace(engagementRequest.City, nameof(engagementRequest.City));
		if (engagementRequest.StartDate is null)
			throw new ArgumentException("Start date is required.", nameof(engagementRequest.StartDate));
		if (engagementRequest.EndDate is null)
			throw new ArgumentException("End date is required.", nameof(engagementRequest.EndDate));
		if (engagementRequest.Permalink.Length > 200)
			throw new ArgumentException("Permalink must be 200 characters or less.", "Engagement Permalink");
		if (engagementRequest.City.Length > 100)
			throw new ArgumentException("City must be 100 characters or less.", "City");
		if (engagementRequest.Venue?.Length > 200)
			throw new ArgumentException("Venue must be 200 characters or less.", "Venue");
		if (engagementRequest.OverviewLocation?.Length > 300)
			throw new ArgumentException("Overview location must be 300 characters or less.", "Overview Location");
		if (engagementRequest.ListingLocation?.Length > 100)
			throw new ArgumentException("Listing location must be 100 characters or less.", "Listing Location");
		if (engagementRequest.Description?.Length > 2000)
			throw new ArgumentException("Description must be 2000 characters or less.", "Description");
		if (engagementRequest.Summary?.Length > 160)
			throw new ArgumentException("Summary must be 160 characters or less.", "Summary");
		if (engagementRequest.Url?.Length > 200)
			throw new ArgumentException("URL must be 200 characters or less.", "URL");
		if (engagementRequest.StartingCost?.Length > 20)
			throw new ArgumentException("Starting cost must be 20 characters or less.", "Starting Cost");
		if (engagementRequest.EndingCost?.Length > 20)
			throw new ArgumentException("Ending cost must be 20 characters or less.", "Ending Cost");
	}

	private static void ValidateCallForSpeakersValues(EngagementRequest engagementRequest)
	{
		if (engagementRequest.CallForSpeakerDetails is not null)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(engagementRequest.CallForSpeakerDetails.Status, nameof(engagementRequest.CallForSpeakerDetails.Status));
			ArgumentException.ThrowIfNullOrWhiteSpace(engagementRequest.CallForSpeakerDetails.Url, nameof(engagementRequest.CallForSpeakerDetails.Url));
			if (engagementRequest.CallForSpeakerDetails.StartDate is null)
				throw new ArgumentException("Call for speaker start date is required.", nameof(engagementRequest.CallForSpeakerDetails.StartDate));
			if (engagementRequest.CallForSpeakerDetails.EndDate is null)
				throw new ArgumentException("Call for speaker end date is required.", nameof(engagementRequest.CallForSpeakerDetails.EndDate));
			if (engagementRequest.CallForSpeakerDetails.Url.Length > 200)
				throw new ArgumentException("Call for speaker URL must be 200 characters or less.", "Call for speaker URL");
			if (engagementRequest.CallForSpeakerDetails.SpeakerHonorariumNotes?.Length > 200)
				throw new ArgumentException("Speaker honorarium notes must be 200 characters or less.", "Speaker honorarium notes");
			if (engagementRequest.CallForSpeakerDetails.TravelExpensesNotes?.Length > 200)
				throw new ArgumentException("Travel expenses notes must be 200 characters or less.", "Travel expenses notes");
			if (engagementRequest.CallForSpeakerDetails.AccommodationNotes?.Length > 200)
				throw new ArgumentException("Accommodation notes must be 200 characters or less.", "Accommodation notes");
			if (engagementRequest.CallForSpeakerDetails.EventFeeNotes?.Length > 200)
				throw new ArgumentException("Event fee notes must be 200 characters or less.", "Event fee notes");
			if (!Uri.TryCreate(engagementRequest.CallForSpeakerDetails.Url, UriKind.Absolute, out Uri? uri))
				throw new ArgumentException("Call for speaker URL must be a valid URL.", "Call for speaker URL");
		}
	}

	private static void ValidateEngagementPresentationValues(EngagementRequest engagementRequest)
	{
		foreach (EngagementPresentationRequest presentation in engagementRequest.Presentations)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(presentation.Status, nameof(presentation.Status));
			if (presentation.PresentationLength <= 0)
				throw new ArgumentException("Presentation length must be greater than zero.", nameof(presentation.PresentationLength));
			if (presentation.Room?.Length > 100)
				throw new ArgumentException("Room must be 100 characters or less.", "Presentation Room");
			if (presentation.EngagementPresentationUrl?.Length > 200)
				throw new ArgumentException("Engagement presentation URL must be 200 characters or less.", "Engagement Presentation URL");
			if (presentation.Title.Length > 300)
				throw new ArgumentException("Title must be 300 characters or less.", "Presentation Title");
			if (presentation.PresentationShortTitle?.Length > 60)
				throw new ArgumentException("Presentation short title must be 60 characters or less.", "Presentation Short Title");
			if (presentation.Abstract?.Length > 3000)
				throw new ArgumentException("Abstract must be 3000 characters or less.", "Abstract");
			if (presentation.ShortAbstract?.Length > 2000)
				throw new ArgumentException("Short abstract must be 2000 characters or less.", "Short Abstract");
			if (presentation.ElevatorPitch?.Length > 300)
				throw new ArgumentException("Elevator pitch must be 300 characters or less.", "Elevator Pitch");
			if (presentation.AdditionalDetails?.Length > 3000)
				throw new ArgumentException("Additional details must be 3000 characters or less.", "Additional Details");
			foreach (LearningObjectiveRequest learningObjective in presentation.LearningObjectiveRequests)
				if (learningObjective.Text.Length > 1000)
					throw new ArgumentException("Learning objective text must be 1000 characters or less.", "Learning Objective");
			foreach (string tag in presentation.Tags)
				if (tag.Length > 100)
					throw new ArgumentException("Tag must be 100 characters or less.", "Tag");
			if (!string.IsNullOrWhiteSpace(presentation.EngagementPresentationUrl) && !Uri.TryCreate(presentation.EngagementPresentationUrl, UriKind.Absolute, out Uri? uri))
				throw new ArgumentException("Engagement presentation URL must be a valid URL.", "Engagement Presentation URL");
			foreach (EngagementPresentationDownloadRequest download in presentation.Downloads)
			{
				if (download.Name.Length > 50)
					throw new ArgumentException("Download name must be 50 characters or less.", "Download Name");
				if (download.Path.Length > 200)
					throw new ArgumentException("Download Path must be 200 characters or less.", "Download URL");
			}
		}
	}

	private async Task CheckEngagementPermalinkForUniqueness(EngagementRequest engagementRequest)
	{
		Engagement? engagement = await GetFirstOrDefaultAsync<Engagement>(e => e.Permalink == engagementRequest.Permalink);
		if (engagement is not null)
			throw new ArgumentException($"The engagement with the permalink '{engagementRequest.Permalink}' already exists.");
	}

	private async Task ValidateEngagementReferencedDataAsync(EngagementRequest engagementRequest)
	{
		EngagementType? engagementType = await GetFirstOrDefaultAsync<EngagementType>(et => et.EngagementTypeName == engagementRequest.EngagementType) ?? throw new ArgumentException($"The engagement type '{engagementRequest.EngagementType}' does not exist.");
		EngagementStatus? engagementStatus = await GetFirstOrDefaultAsync<EngagementStatus>(es => es.EngagementStatusName == engagementRequest.Status) ?? throw new ArgumentException($"The engagement status '{engagementRequest.Status}' does not exist.");
		Country? country = await GetFirstOrDefaultAsync<Country>(c => c.CountryCode == engagementRequest.CountryCode) ?? throw new ArgumentException($"The country code '{engagementRequest.CountryCode}' does not exist.");
		if (!string.IsNullOrWhiteSpace(engagementRequest.CountryDivisionCode))
		{
			CountryDivision? countryDivision = await GetFirstOrDefaultAsync<CountryDivision>(cd => cd.CountryDivisionCode == engagementRequest.CountryDivisionCode && cd.CountryCode == engagementRequest.CountryCode) ?? throw new ArgumentException($"The country division code '{engagementRequest.CountryDivisionCode}' does not exist.");
		}
		if (!string.IsNullOrWhiteSpace(engagementRequest.LanguageCode))
		{
			Language? language = await GetFirstOrDefaultAsync<Language>(l => l.LanguageCode == engagementRequest.LanguageCode) ?? throw new ArgumentException($"The language code '{engagementRequest.LanguageCode}' does not exist.");
		}
	}

	private async Task ValidateCallForSpeakersRefencedDataAsync(EngagementRequest engagementRequest)
	{
		if (engagementRequest.CallForSpeakerDetails is not null)
		{
			EngagementCallForSpeakerStatus? callForSpeakerStatus = await GetFirstOrDefaultAsync<EngagementCallForSpeakerStatus>(x => x.EngagementCallForSpeakerStatusName == engagementRequest.CallForSpeakerDetails.Status)
				?? throw new ArgumentException($"The call for speaker status '{engagementRequest.CallForSpeakerDetails.Status}' does not exist.");
		}
	}

	private async Task ValidateEngagementPresentationsReferencedDataAsync(EngagementRequest engagementRequest)
	{
		foreach (EngagementPresentationRequest presentationRequest in engagementRequest.Presentations)
		{
			Presentation? presentation = await GetFirstOrDefaultAsync<Presentation>(p => p.Permalink == presentationRequest.PresentationPermalink)
				?? throw new ArgumentException($"The presentation with the permalink '{presentationRequest.PresentationPermalink}' does not exist.");
			EngagementPresentationStatus? presentationStatus = await GetFirstOrDefaultAsync<EngagementPresentationStatus>(x => x.EngagementPresentationStatusName == presentationRequest.Status)
				?? throw new ArgumentException($"The presentation status '{presentationRequest.Status}' does not exist.");
			if (!string.IsNullOrWhiteSpace(presentationRequest.LanguageCode))
			{
				Language? language = await GetFirstOrDefaultAsync<Language>(l => l.LanguageCode == presentationRequest.LanguageCode) ?? throw new ArgumentException($"The language code '{presentationRequest.LanguageCode}' does not exist.");
			}
			if (presentationRequest.Downloads.Count > 0)
			{
				IEnumerable<string> downloadTypes = (await GetAllAsync<DownloadType>()).Select(x => x.DownloadTypeName);
				foreach (EngagementPresentationDownloadRequest download in presentationRequest.Downloads)
					if (!downloadTypes.Contains(download.Type))
						throw new ArgumentException($"The download type '{download.Type}' does not exist.");
			}
		}
	}

	private EngagementRequest? ParseFileToEngagementRequest(string inputPath, InputOutputFormat inputFormat)
		=> inputFormat switch
		{
			InputOutputFormat.Json => ParseJsonToEngagementRequest(File.ReadAllText(inputPath)),
			InputOutputFormat.Markdown => ParseMarkdownFileToEngagementRequest(inputPath),
			_ => null
		};

	private EngagementRequest? ParseJsonToEngagementRequest(string jsonContent)
		=> JsonSerializer.Deserialize<EngagementRequest>(jsonContent, _jsonSerializerOptions);

	private static EngagementRequest ParseMarkdownFileToEngagementRequest(string inputPath)
	{

		List<string> markdownLines = [.. File.ReadAllLines(inputPath)];

		EngagementRequest engagementRequest = new();

		StringBuilder engagementDescription = new();
		StringBuilder engagementSummary = new();
		StringBuilder abstractDescription = new();
		StringBuilder shortAbstract = new();
		StringBuilder elevatorPitch = new();
		StringBuilder additionalDetails = new();

		EngagementPresentationRequest engagementPresentation = new();
		bool buildingEngagementPresentationRequest = false;
		bool firstPresentationFound = false;

		string? currentH2 = null;
		string? currentH3 = null;
		string? currentH4 = null;

		foreach (string markdownLine in markdownLines)
		{
			string currentMarkdownLine = markdownLine.Trim();
			if (currentMarkdownLine.StartsWith("## "))
			{
				currentH2 = currentMarkdownLine[3..];
				currentH3 = null;
				currentH4 = null;
			}
			else if (currentMarkdownLine.StartsWith("### "))
			{
				currentH3 = currentMarkdownLine[4..];
				currentH4 = null;
				if (buildingEngagementPresentationRequest)
				{
					if (firstPresentationFound == true)
					{
						engagementRequest.Presentations.Add(
							FinalizePresentationParsing(
								engagementPresentation,
								abstractDescription,
								shortAbstract,
								elevatorPitch,
								additionalDetails));
						engagementPresentation = new();
						abstractDescription = new();
						shortAbstract = new();
						elevatorPitch = new();
						additionalDetails = new();
					}
					else
						firstPresentationFound = true;
				}
			}
			else if (currentMarkdownLine.StartsWith("#### "))
			{
				currentH4 = currentMarkdownLine[5..];
			}
			else
			{
				switch (currentH2)
				{
					case MarkdownEngagementHeadings.EngagementAttributes:
						ParseEngagementAttribute(engagementRequest, markdownLine);
						break;
					case MarkdownEngagementHeadings.EngagementDetails:
						ParseEngagementDetails(markdownLine, currentH3, engagementDescription, engagementSummary);
						break;
					case MarkdownEngagementHeadings.CallForSpeakerAttributes:
						ParseCallForSpeakersAttribute(markdownLine, engagementRequest);
						break;
					case MarkdownEngagementHeadings.Presentations:
						buildingEngagementPresentationRequest = true;
						ParsePresentation(markdownLine, currentH4, engagementPresentation, abstractDescription, shortAbstract, elevatorPitch, additionalDetails);
						break;
				}
			}
		}

		if (engagementDescription.ToString().Trim('\r', '\n').Length > 0)
			engagementRequest.Description = engagementDescription.ToString().Trim('\r', '\n');
		if (engagementSummary.ToString().Trim('\r', '\n').Length > 0)
			engagementRequest.Summary = engagementSummary.ToString().Trim('\r', '\n');
		if (buildingEngagementPresentationRequest && firstPresentationFound)
			engagementRequest.Presentations.Add(
				FinalizePresentationParsing(
					engagementPresentation,
					abstractDescription,
					shortAbstract,
					elevatorPitch,
					additionalDetails));

		return engagementRequest;

	}

	private static void ParseEngagementAttribute(EngagementRequest engagementRequest, string markdownLine)
	{
		string[] tableRow = markdownLine.Split('|');
		if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.Permalink, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.Permalink = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.EngagementType, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.EngagementType = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.Status, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.Status = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.Name, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.Name = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.CountryCode, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.CountryCode = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.CountryDivisionCode, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.CountryDivisionCode = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.City, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.City = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.Venue, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.Venue = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.OverviewLocation, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.OverviewLocation = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.ListingLocation, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.ListingLocation = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.StartDate, StringComparison.InvariantCultureIgnoreCase)
			&& DateOnly.TryParse(GetAttributeValue(tableRow), out DateOnly startDate))
			engagementRequest.StartDate = startDate;
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.EndDate, StringComparison.InvariantCultureIgnoreCase)
			&& DateOnly.TryParse(GetAttributeValue(tableRow), out DateOnly endDate))
			engagementRequest.EndDate = endDate;
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.TimeZone, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.TimeZone = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.LanguageCode, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.LanguageCode = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.StartingCost, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.StartingCost = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.EndingCost, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.EndingCost = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.Url, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.Url = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.IncludeInPublicProfile, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.IncludeInPublicProfile = bool.Parse(GetAttributeValue(tableRow));
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.IsVirtual, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.IsVirtual = bool.Parse(GetAttributeValue(tableRow));
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.IsHybrid, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.IsHybrid = bool.Parse(GetAttributeValue(tableRow));
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementAttributes.IsPublic, StringComparison.InvariantCultureIgnoreCase))
			engagementRequest.IsPublic = bool.Parse(GetAttributeValue(tableRow));
	}

	private static void ParseEngagementDetails(string markdownLine, string? currentH3, StringBuilder engagementDescription, StringBuilder engagementSummary)
	{
		switch (currentH3)
		{
			case MarkdownEngagementDetailHeadings.Description:
				engagementDescription.AppendLine(markdownLine);
				break;
			case MarkdownEngagementDetailHeadings.Summary:
				engagementSummary.AppendLine(markdownLine);
				break;
		}
	}

	private static void ParseCallForSpeakersAttribute(string markdownLine, EngagementRequest engagementRequest)
	{
		string[] tableRow = markdownLine.Split('|');
		if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.Status, StringComparison.InvariantCultureIgnoreCase))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.Status = GetAttributeValue(tableRow);
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.Url, StringComparison.InvariantCultureIgnoreCase))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.Url = GetAttributeValue(tableRow);
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.StartDate, StringComparison.InvariantCultureIgnoreCase)
			&& DateOnly.TryParse(GetAttributeValue(tableRow), out DateOnly startDate))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.StartDate = startDate;
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.EndDate, StringComparison.InvariantCultureIgnoreCase)
			&& DateOnly.TryParse(GetAttributeValue(tableRow), out DateOnly endDate))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.EndDate = endDate;
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.ExpectedDecisionDate, StringComparison.InvariantCultureIgnoreCase)
			&& DateOnly.TryParse(GetAttributeValue(tableRow), out DateOnly expectedDecisionDate))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.ExpectedDecisionDate = expectedDecisionDate;
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.ActualDecisionDate, StringComparison.InvariantCultureIgnoreCase)
			&& DateOnly.TryParse(GetAttributeValue(tableRow), out DateOnly actualDecisionDate))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.ActualDecisionDate = actualDecisionDate;
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.IncludesSpeakerHonorarium, StringComparison.InvariantCultureIgnoreCase))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.IncludesSpeakerHonorarium = bool.Parse(GetAttributeValue(tableRow));
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.SpeakerHonorariumAmount, StringComparison.InvariantCultureIgnoreCase)
			&& decimal.TryParse(GetAttributeValue(tableRow), out decimal speakerHonorariumAmount))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.SpeakerHonorariumAmount = speakerHonorariumAmount;
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.SpeakerHonorariumCurrencyCode, StringComparison.InvariantCultureIgnoreCase))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.SpeakerHonorariumCurrencyCode = GetAttributeValue(tableRow);
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.SpeakerHonorariumNotes, StringComparison.InvariantCultureIgnoreCase))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.SpeakerHonorariumNotes = GetAttributeValue(tableRow);
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.IncludesTravelExpenses, StringComparison.InvariantCultureIgnoreCase))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.IncludesTravelExpenses = bool.Parse(GetAttributeValue(tableRow));
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.TravelExpensesNotes, StringComparison.InvariantCultureIgnoreCase))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.TravelExpensesNotes = GetAttributeValue(tableRow);
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.IncludesAccommodation, StringComparison.InvariantCultureIgnoreCase))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.IncludesAccommodation = bool.Parse(GetAttributeValue(tableRow));
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.AccommodationNotes, StringComparison.InvariantCultureIgnoreCase))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.AccommodationNotes = GetAttributeValue(tableRow);
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.EventFeeCovered, StringComparison.InvariantCultureIgnoreCase))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.EventFeeCovered = bool.Parse(GetAttributeValue(tableRow));
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.EventFeeNotes, StringComparison.InvariantCultureIgnoreCase))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.EventFeeNotes = GetAttributeValue(tableRow);
		}
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementCallForSpeakerAttributes.SubmissionLimit, StringComparison.InvariantCultureIgnoreCase)
			&& int.TryParse(GetAttributeValue(tableRow), out int submissionLimit))
		{
			engagementRequest.CallForSpeakerDetails ??= new();
			engagementRequest.CallForSpeakerDetails.SubmissionLimit = submissionLimit;
		}
	}

	private static void ParsePresentation(
		string markdownLine,
		string? currentH4,
		EngagementPresentationRequest engagementPresentation,
		StringBuilder abstractDescription,
		StringBuilder shortAbstract,
		StringBuilder elevatorPitch,
		StringBuilder additionalDetails)
	{
		switch (currentH4)
		{
			case MarkdownEngagementPresentationHeadings.PresentationAttributes:
				ParsePresentationAttribute(markdownLine, engagementPresentation);
				break;
			case MarkdownEngagementPresentationHeadings.Abstract:
				abstractDescription.AppendLine(markdownLine);
				break;
			case MarkdownEngagementPresentationHeadings.ShortAbstract:
				shortAbstract.AppendLine(markdownLine);
				break;
			case MarkdownEngagementPresentationHeadings.ElevatorPitch:
				elevatorPitch.AppendLine(markdownLine);
				break;
			case MarkdownEngagementPresentationHeadings.AdditionalDetails:
				additionalDetails.AppendLine(markdownLine);
				break;
			case MarkdownEngagementPresentationHeadings.Tags:
				if (markdownLine.StartsWith("- "))
					engagementPresentation.Tags.Add(ParseListItem(markdownLine));
				break;
			case MarkdownEngagementPresentationHeadings.LearningObjectives:
				if (markdownLine.StartsWith("- "))
					engagementPresentation.LearningObjectiveRequests.Add(new() { Text = ParseListItem(markdownLine), SortOrder = engagementPresentation.LearningObjectiveRequests.Count + 1 });
				break;
			case MarkdownEngagementPresentationHeadings.Downloads:
				if (markdownLine.Trim().StartsWith('|')
					&& !markdownLine.Trim().StartsWith("| Name")
					&& !markdownLine.Trim().StartsWith("| -")
					&& !markdownLine.Trim().StartsWith("|-"))
				{
					string[] tableRow = markdownLine.Split('|');
					engagementPresentation.Downloads.Add(new()
					{
						Name = tableRow[1].Trim(),
						Type = tableRow[2].Trim(),
						Path = tableRow[3].Trim()
					});
				}
				break;
		}
	}

	private static EngagementPresentationRequest FinalizePresentationParsing(
		EngagementPresentationRequest engagementPresentation,
		StringBuilder abstractDescription,
		StringBuilder shortAbstract,
		StringBuilder elevatorPitch,
		StringBuilder additionalDetails)
	{
		if (abstractDescription.ToString().Trim('\r', '\n').Length > 0)
			engagementPresentation.Abstract = abstractDescription.ToString().Trim('\r', '\n');
		if (shortAbstract.ToString().Trim('\r', '\n').Length > 0)
			engagementPresentation.ShortAbstract = shortAbstract.ToString().Trim('\r', '\n');
		if (elevatorPitch.ToString().Trim('\r', '\n').Length > 0)
			engagementPresentation.ElevatorPitch = elevatorPitch.ToString().Trim('\r', '\n');
		if (additionalDetails.ToString().Trim('\r', '\n').Length > 0)
			engagementPresentation.AdditionalDetails = additionalDetails.ToString().Trim('\r', '\n');
		return engagementPresentation;
	}

	private static void ParsePresentationAttribute(string markdownLine, EngagementPresentationRequest engagementPresentation)
	{
		string[] tableRow = markdownLine.Split('|');
		if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementPresentationAttributes.PresentationPermalink, StringComparison.InvariantCultureIgnoreCase))
			engagementPresentation.PresentationPermalink = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementPresentationAttributes.Status, StringComparison.InvariantCultureIgnoreCase))
			engagementPresentation.Status = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementPresentationAttributes.StartDateTime, StringComparison.InvariantCultureIgnoreCase))
			engagementPresentation.StartDateTime = DateTime.Parse(GetAttributeValue(tableRow));
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementPresentationAttributes.PresentationLength, StringComparison.InvariantCultureIgnoreCase))
			engagementPresentation.PresentationLength = int.Parse(GetAttributeValue(tableRow));
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementPresentationAttributes.Room, StringComparison.InvariantCultureIgnoreCase))
			engagementPresentation.Room = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementPresentationAttributes.EngagementPresentationUrl, StringComparison.InvariantCultureIgnoreCase))
			engagementPresentation.EngagementPresentationUrl = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementPresentationAttributes.LanguageCode, StringComparison.InvariantCultureIgnoreCase))
			engagementPresentation.LanguageCode = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementPresentationAttributes.Title, StringComparison.InvariantCultureIgnoreCase))
			engagementPresentation.Title = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementPresentationAttributes.PresentationShortTitle, StringComparison.InvariantCultureIgnoreCase))
			engagementPresentation.PresentationShortTitle = GetAttributeValue(tableRow);
		else if (string.Equals(GetAttributeType(tableRow), MarkdownEngagementPresentationAttributes.IsVirtual, StringComparison.InvariantCultureIgnoreCase) && bool.TryParse(GetAttributeValue(tableRow), out bool isVirtual))
			engagementPresentation.IsVirtual = isVirtual;
	}

	private static string GetAttributeType(string[] tableRow) => tableRow.Length > 1 ? tableRow[1].Trim() : string.Empty;

	private static string ParseListItem(string markdownLine) => markdownLine[2..].Trim();

	private static string GetAttributeValue(string[] tableRow) => tableRow.Length > 2 ? tableRow[2].Trim() : string.Empty;


}