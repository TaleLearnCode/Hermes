namespace Hermes.Services;

public class CallForSpeakerServices(string databaseConnectionString) : ServicesBase(databaseConnectionString)
{

	public async Task<List<CallForSpeakerStatus>> GetStatusesAsync()
		=> [.. (await GetAllAsync<CallForSpeakerStatus>()).OrderBy(x => x.SortOrder)];

	public string GetCallForSpeakerTemplate(InputOutputFormat outputFormat, string outputPath)
	{
		CallForSpeakerRequest callForSpeakerRequest = new();
		if (outputFormat == InputOutputFormat.Json)
		{
			callForSpeakerRequest.AddInstructions();
			return SerializeAndSaveFile<CallForSpeakerRequest>(outputPath, callForSpeakerRequest);
		}
		else if (outputFormat == InputOutputFormat.Markdown)
		{
			return SaveFile<CallForSpeakerRequest>(outputPath, callForSpeakerRequest.ToMarkdown(true));
		}
		else
		{
			return "[red]Unsupported output format[/]";
		}
	}

	#region Add

	public async Task<string> AddCallForSpeakerAsync(
		string inputPath,
		InputOutputFormat inputFormat,
		string? outputPath,
		InputOutputFormat? outputFormat)
	{
		CallForSpeakerRequest? callForSpeakerRequest = AssembleRequest(inputPath, inputFormat);
		if (callForSpeakerRequest == null)
			return "[red]Invalid input format[/]";
		return await AddCallForSpeakerAsync(callForSpeakerRequest, inputFormat, outputPath, outputFormat);
	}

	public async Task<string> AddCallForSpeakerAsync(
		CallForSpeakerRequest callForSpeakerRequest,
		InputOutputFormat inputFormat,
		string? outputPath,
		InputOutputFormat? outputFormat)
	{
		if (!(await IsRequestValidAsync(callForSpeakerRequest, true)))
			return "[red]Invalid request[/]";
		await CreateAsync(await ConvertRequestToCallForSpeaker(callForSpeakerRequest));
		await SaveOutputAsync(callForSpeakerRequest.Permalink, outputPath, inputFormat, outputFormat);
		return $"The call for speaker '{callForSpeakerRequest.Permalink}' has been added.";
	}

	private static CallForSpeakerRequest? AssembleRequest(string inputPath, InputOutputFormat inputFormat)
	{
		string input = File.ReadAllText(inputPath);
		if (inputFormat == InputOutputFormat.Json)
		{
			return JsonSerializer.Deserialize<CallForSpeakerRequest>(input);
		}
		else if (inputFormat == InputOutputFormat.Markdown)
		{
			return input.ToCallForSpeakerRequest();
		}
		else
		{
			return null;
		}
	}

	private async Task<bool> IsRequestValidAsync(CallForSpeakerRequest callForSpeakerRequest, bool checkForUniqueness = false)
	{

		ArgumentException.ThrowIfNullOrWhiteSpace(callForSpeakerRequest.EventName);
		ArgumentException.ThrowIfNullOrWhiteSpace(callForSpeakerRequest.EventCountryCode);

		callForSpeakerRequest.Permalink = GetPermalink(callForSpeakerRequest);
		if (checkForUniqueness)
			if (await GetFirstOrDefaultAsync<CallForSpeaker>(x => x.Permalink == callForSpeakerRequest.Permalink) is not null)
				throw new ArgumentException($"The '{callForSpeakerRequest.Permalink}' permalink already exists.");

		if (callForSpeakerRequest.EventStartDate > callForSpeakerRequest.EventEndDate)
			throw new ArgumentException("Event start date must be before event end date");
		if (callForSpeakerRequest.CallForSpeakerStartDate > callForSpeakerRequest.CallForSpeakerEndDate)
			throw new ArgumentException("Call for Speaker start date must be before call for speaker end date");

		List<CallForSpeakerStatus> callForSpeakerStatuses = await GetAllAsync<CallForSpeakerStatus>();
		if (string.IsNullOrWhiteSpace(callForSpeakerRequest.Status))
			callForSpeakerRequest.Status = callForSpeakerStatuses.First(x => x.IsDefault).CallForSpeakerStatusName;
		if (callForSpeakerStatuses.FirstOrDefault(x => x.CallForSpeakerStatusName == callForSpeakerRequest.Status) is null)
			throw new ArgumentException($"'{callForSpeakerRequest.Status}' is not a valid status.");

		Country? country = await GetFirstOrDefaultAsync<Country>(x => x.CountryCode == callForSpeakerRequest.EventCountryCode) ?? throw new ArgumentException($"'{callForSpeakerRequest.EventCountryCode}' is not a valid country code.");
		if (!string.IsNullOrWhiteSpace(callForSpeakerRequest.EventCountryDivisionCode))
		{
			if (!country.HasDivisions)
				throw new ArgumentException($"The country '{country.CountryName}' does not have divisions but you specified one.");
			else if (GetFirstOrDefaultAsync<CountryDivision>(x => x.CountryCode == callForSpeakerRequest.EventCountryCode && x.CountryDivisionCode == callForSpeakerRequest.EventCountryDivisionCode) is null)
				throw new ArgumentException($"'{callForSpeakerRequest.EventCountryDivisionCode}' is not a valid division code for '{country.CountryName}'.");
		}

		if (!string.IsNullOrWhiteSpace(callForSpeakerRequest.EventTimeZone)
			&& (await GetTimeZoneAsync(callForSpeakerRequest.EventTimeZone) is null))
			throw new ArgumentException($"'{callForSpeakerRequest.EventTimeZone}' is not a valid time zone.");

		if (callForSpeakerRequest.Permalink?.Length > 200)
			throw new ArgumentException("Permalink is too long. It must be less than 200 characters.");
		if (callForSpeakerRequest.EventName?.Length > 200)
			throw new ArgumentException("Event name is too long. It must be less than 200 characters.");
		if (callForSpeakerRequest.EventUrl?.Length > 200)
			throw new ArgumentException("Event URL is too long. It must be less than 200 characters.");
		if (callForSpeakerRequest.EventLocation?.Length > 300)
			throw new ArgumentException("Event location is too long. It must be less than 300 characters.");
		if (callForSpeakerRequest.EventCity?.Length > 100)
			throw new ArgumentException("Event city is too long. It must be less than 100 characters.");
		if (callForSpeakerRequest.SpeakerHonorariumNotes?.Length > 500)
			throw new ArgumentException("Speaker honorarium notes is too long. It must be less than 500 characters.");
		if (callForSpeakerRequest.TravelNotes?.Length > 500)
			throw new ArgumentException("Travel notes is too long. It must be less than 500 characters.");
		if (callForSpeakerRequest.AccomodationNotes?.Length > 500)
			throw new ArgumentException("Accommodation notes is too long. It must be less than 500 characters.");
		if (callForSpeakerRequest.EventFeeNotes?.Length > 500)
			throw new ArgumentException("Event fee notes is too long. It must be less than 500 characters.");


		return true;
	}

	private static string GetPermalink(CallForSpeakerRequest callForSpeakerRequest)
	{
		if (!string.IsNullOrWhiteSpace(callForSpeakerRequest.Permalink))
			return callForSpeakerRequest.Permalink;
		string eventName = callForSpeakerRequest.EventName;
		if (!eventName.Contains(callForSpeakerRequest.EventStartDate.Year.ToString()))
			eventName += $" {callForSpeakerRequest.EventStartDate.Year}";
		return eventName.ToKebabCase();
	}

	private async Task<Models.TimeZone?> GetTimeZoneAsync(string timeZone)
	{
		return await GetFirstOrDefaultAsync<Models.TimeZone>(
			x => x.TimeZoneId == timeZone
			|| x.StandardAbbreviation == timeZone
			|| x.DaylightSavingsAbbreviation == timeZone);
	}

	#endregion

	#region Get Call for Speaker

	public async Task<CallForSpeaker?> GetCallForSpeakerAsync(string permalink)
	{
		using HermesContext context = new(_databaseConnectionString);
		return await context.CallForSpeakers
			.Include(x => x.CallForSpeakerStatus)
			.Include(x => x.CountryDivision)
				.ThenInclude(x => x.CountryCodeNavigation)
			.Include(x => x.EventCountryCodeNavigation)
			.Include(x => x.EventTimeZone)
			.FirstOrDefaultAsync(x => x.Permalink == permalink);
	}

	#endregion

	private async Task<CallForSpeaker> ConvertRequestToCallForSpeaker(CallForSpeakerRequest callForSpeakerRequest)
	{
		CallForSpeaker response = new CallForSpeaker
		{
			Permalink = callForSpeakerRequest.Permalink,
			CallForSpeakerStatusId = (await GetFirstOrDefaultAsync<CallForSpeakerStatus>(x => x.CallForSpeakerStatusName == callForSpeakerRequest.Status))!.CallForSpeakerStatusId,
			EventName = callForSpeakerRequest.EventName,
			EventUrl = (!string.IsNullOrWhiteSpace(callForSpeakerRequest.EventTimeZone)) ? callForSpeakerRequest.EventUrl : null,
			EventStartDate = callForSpeakerRequest.EventStartDate,
			EventEndDate = callForSpeakerRequest.EventEndDate,
			EventLocation = (!string.IsNullOrWhiteSpace(callForSpeakerRequest.EventLocation)) ? callForSpeakerRequest.EventLocation : null,
			EventCity = (!string.IsNullOrWhiteSpace(callForSpeakerRequest.EventCity)) ? callForSpeakerRequest.EventCity : null,
			EventCountryCode = callForSpeakerRequest.EventCountryCode,
			EventCountryDivisionCode = (!string.IsNullOrWhiteSpace(callForSpeakerRequest.EventCountryDivisionCode)) ? callForSpeakerRequest.EventCountryDivisionCode : null,
			EventTimeZoneId = (!string.IsNullOrWhiteSpace(callForSpeakerRequest.EventTimeZone)) ? (await GetTimeZoneAsync(callForSpeakerRequest.EventTimeZone))?.TimeZoneId! : null,
			CallForSpeakerUrl = (!string.IsNullOrWhiteSpace(callForSpeakerRequest.CallForSpeakerUrl)) ? callForSpeakerRequest.CallForSpeakerUrl : null,
			CallForSpeakerStartDate = callForSpeakerRequest.CallForSpeakerStartDate,
			CallForSpeakerEndDate = callForSpeakerRequest.CallForSpeakerEndDate,
			SpeakerHonorarium = callForSpeakerRequest.IncludesSpeakerHonorarium,
			SpeakerHonorariumAmount = callForSpeakerRequest.SpeakerHonorariumAmount,
			SpeakerHonorariumCurrency = (!string.IsNullOrWhiteSpace(callForSpeakerRequest.SpeakerHonorariumCurrency)) ? callForSpeakerRequest.SpeakerHonorariumCurrency : null,
			SpeakerHonorariumNotes = (!string.IsNullOrWhiteSpace(callForSpeakerRequest.SpeakerHonorariumNotes)) ? callForSpeakerRequest.SpeakerHonorariumNotes : null,
			TravelExpensesCovered = callForSpeakerRequest.AreTravelExpenseCovered,
			TravelNotes = (!string.IsNullOrWhiteSpace(callForSpeakerRequest.TravelNotes)) ? callForSpeakerRequest.TravelNotes : null,
			AccomodationExpensesCovered = callForSpeakerRequest.AreAccommodationsCovered,
			AccomodationNotes = (!string.IsNullOrWhiteSpace(callForSpeakerRequest.AccomodationNotes)) ? callForSpeakerRequest.AccomodationNotes : null,
			EventFeeCovered = callForSpeakerRequest.EventFeeCovered,
			EventFeeNotes = (!string.IsNullOrWhiteSpace(callForSpeakerRequest.EventFeeNotes)) ? callForSpeakerRequest.EventFeeNotes : null,
			SubmissionLimit = callForSpeakerRequest.SubmissionLimit
		};
		return response;
	}

	private static CallForSpeakerRequest ConvertCallForSpeakerToRequest(CallForSpeaker callForSpeaker)
	{
		return new()
		{
			Permalink = callForSpeaker.Permalink,
			Status = callForSpeaker.CallForSpeakerStatus.CallForSpeakerStatusName,
			EventName = callForSpeaker.EventName,
			EventUrl = callForSpeaker.EventUrl,
			EventStartDate = callForSpeaker.EventStartDate,
			EventEndDate = callForSpeaker.EventEndDate,
			EventLocation = callForSpeaker.EventLocation,
			EventCity = callForSpeaker.EventCity,
			EventCountryCode = callForSpeaker.EventCountryCode,
			EventCountryDivisionCode = callForSpeaker.EventCountryDivisionCode,
			EventTimeZone = callForSpeaker.EventTimeZone.TimeZoneId,
			CallForSpeakerUrl = callForSpeaker.CallForSpeakerUrl,
			CallForSpeakerStartDate = callForSpeaker.CallForSpeakerStartDate,
			CallForSpeakerEndDate = callForSpeaker.CallForSpeakerEndDate,
			IncludesSpeakerHonorarium = callForSpeaker.SpeakerHonorarium,
			SpeakerHonorariumAmount = callForSpeaker.SpeakerHonorariumAmount,
			SpeakerHonorariumCurrency = callForSpeaker.SpeakerHonorariumCurrency,
			SpeakerHonorariumNotes = callForSpeaker.SpeakerHonorariumNotes,
			AreTravelExpenseCovered = callForSpeaker.TravelExpensesCovered,
			TravelNotes = callForSpeaker.TravelNotes,
			AreAccommodationsCovered = callForSpeaker.AccomodationExpensesCovered,
			AccomodationNotes = callForSpeaker.AccomodationNotes,
			EventFeeCovered = callForSpeaker.EventFeeCovered,
			EventFeeNotes = callForSpeaker.EventFeeNotes,
			SubmissionLimit = callForSpeaker.SubmissionLimit
		};
	}

	private async Task SaveOutputAsync(
		string permalink,
		string? outputPath,
		InputOutputFormat inputFormat,
		InputOutputFormat? outputFormat)
	{
		outputFormat ??= inputFormat;
		if (!string.IsNullOrWhiteSpace(outputPath) && outputFormat != InputOutputFormat.Console)
		{
			CallForSpeakerRequest callForSpeakerRequest = ConvertCallForSpeakerToRequest((await GetCallForSpeakerAsync(permalink))!);
			if (callForSpeakerRequest is not null)
			{
				if (outputFormat == InputOutputFormat.Json)
					SerializeAndSaveFile(outputPath ?? $"{callForSpeakerRequest.Permalink}.json", callForSpeakerRequest);
				else if (outputFormat == InputOutputFormat.Markdown)
					SaveFile<Presentation>(outputPath ?? $"{callForSpeakerRequest.Permalink}.md", callForSpeakerRequest.ToMarkdown());
			}
		}
	}

}