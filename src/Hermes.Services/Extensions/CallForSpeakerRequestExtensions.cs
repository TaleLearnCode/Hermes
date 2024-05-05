﻿namespace Hermes.Extensions;

internal static class CallForSpeakerRequestExtensions
{
	private const string _permalinkInstructions = "[REQUIRED] The unique permalink for the call for speakers";
	private const string _statusInstructions = "[REQUIRED] The status of the call for speakers.";
	private const string _callForSpeakersUrlInstructions = "[OPTIONAL] The URL for the call for speakers webpage.";
	private const string _callForSpeakersStartDateInstructions = "[OPTIONAL] The start date of the call for speakers.";
	private const string _callForSpeakersEndDateInstructions = "[OPTIONAL] The end date of the call for speakers.";
	private const string _includesHonorariumInstructions = "[OPTIONAL] Indication whether the event pays speakers an honorarium. Default is false.";
	private const string _honorariumAmountInstructions = "[OPTIONAL] If the event pays an honorarium, specify the amount here.";
	private const string _honorariumCurrencyInstructions = "[OPTIONAL] The currency for the honorarium amount.";
	private const string _honorariumNotesInstructions = "[OPTIONAL] Any notes regarding the speaker honorarium.";
	private const string _areTravelExpensesCoveredInstructions = "[OPTIONAL] Indication whether the event pays for travel costs. Default is false.";
	private const string _travelNotesInstructions = "[OPTIONAL] Any notes about the event's coverage of travel costs.";
	private const string _areAccommodationsCoveredInstructions = "[OPTIONAL] Indication whether the event pays for the accommodations during the event. Default is false.";
	private const string _accomodationNotesInstructions = "OPTIONAL] Any notes about the event's accommodation coverage.";
	private const string _eventFeeCoveredInstructions = "[OPTIONAL] Indicating whether the event fee is covered for speakers. Default is true.";
	private const string _eventFeeNotesInstructions = "[OPTIONAL] Any notes about the event fee being covered or note.";
	private const string _submissionLimitInstructions = "[OPTIONAL] The number of submissions allowed per speaker. Either do not specify a value or specify 0 if there is no limit.";
	private const string _eventNameInstructions = "[REQUIRED] The name of the event.";
	private const string _eventUrlInstructions = "[OPTIONAL] The URL for the event.";
	private const string _eventStartDateInstructions = "[REQUIRED] The event start date.";
	private const string _eventEndDateInstructions = "[REQUIRED] The event end date.";
	private const string _eventLocationInstructions = "[REQUIRED] The location of the event.";
	private const string _eventCityInstructions = "[REQUIRED] The city where the event is held.";
	private const string _eventCountryCodeInstructions = "[REQUIRED] The 2-character code for the country where the event is held.";
	private const string _eventCountryDivisionCodeInstructions = "[OPTIONAL] The 2-character code for the country division where the event is held.";
	private const string _eventTimeZoneInstructions = "[OPTIONAL] The IANA time zone for the event's location.";

	internal static string ToMarkdown(this CallForSpeakerRequest callForSpeakerRequest, bool populateWithInstructions = false)
	{
		StringBuilder response = new();
		response.AppendLine($"## {MarkdownCallForSpeakersHeadings.CallForSpeakerDetails}");
		response.AppendLine("| Attribute | Value |");
		response.AppendLine("|---|---|");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.Permalink} | {((!populateWithInstructions) ? callForSpeakerRequest.Permalink : _permalinkInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.Status} | {((!populateWithInstructions) ? callForSpeakerRequest.Status : _statusInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.CallForSpeakerUrl} | {((!populateWithInstructions) ? callForSpeakerRequest.CallForSpeakerUrl : _callForSpeakersUrlInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.CallForSpeakersStartDate} | {((!populateWithInstructions) ? callForSpeakerRequest.CallForSpeakerStartDate : CallForSpeakerRequestExtensions._callForSpeakersStartDateInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.CallForSpeakersEndDate} | {((!populateWithInstructions) ? callForSpeakerRequest.CallForSpeakerEndDate : CallForSpeakerRequestExtensions._callForSpeakersEndDateInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.IncludesSpeakerHonorarium} | {((!populateWithInstructions) ? callForSpeakerRequest.IncludesSpeakerHonorarium : CallForSpeakerRequestExtensions._includesHonorariumInstructions)} | ");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.SpeakerHonorariumAmount} | {((!populateWithInstructions) ? callForSpeakerRequest.SpeakerHonorariumAmount : CallForSpeakerRequestExtensions._honorariumAmountInstructions)} | ");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.SpeakerHonorariumCurrency} | {((!populateWithInstructions) ? callForSpeakerRequest.SpeakerHonorariumCurrency : _honorariumCurrencyInstructions)} | ");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.SpeakerHonorariumNotes} | {((!populateWithInstructions) ? callForSpeakerRequest.SpeakerHonorariumNotes : _honorariumNotesInstructions)} | ");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.AreTravelExpensesCovered} | {((!populateWithInstructions) ? callForSpeakerRequest.AreTravelExpenseCovered : CallForSpeakerRequestExtensions._areTravelExpensesCoveredInstructions)} | ");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.TravelNotes} | {((!populateWithInstructions) ? callForSpeakerRequest.TravelNotes : _travelNotesInstructions)} | ");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.AreAccommodationsCovered} | {((!populateWithInstructions) ? callForSpeakerRequest.AreAccommodationsCovered : CallForSpeakerRequestExtensions._areAccommodationsCoveredInstructions)} | ");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.AccomodationNotes} | {((!populateWithInstructions) ? callForSpeakerRequest.AccomodationNotes : _accomodationNotesInstructions)} | ");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.IsEventFeeCovered} | {((!populateWithInstructions) ? callForSpeakerRequest.EventFeeCovered : CallForSpeakerRequestExtensions._eventFeeCoveredInstructions)} | ");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventFeeNotes} | {((!populateWithInstructions) ? callForSpeakerRequest.EventFeeNotes : _eventFeeNotesInstructions)} | ");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.SubmissionLimit} | {((!populateWithInstructions) ? callForSpeakerRequest.SubmissionLimit : CallForSpeakerRequestExtensions._submissionLimitInstructions)} | ");
		response.AppendLine();
		response.AppendLine($"## {MarkdownCallForSpeakersHeadings.EventDetails}");
		response.AppendLine("| Attribute | Value |");
		response.AppendLine("|---|---|");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventName} | {((!populateWithInstructions) ? callForSpeakerRequest.EventName : _eventNameInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventUrl} | {((!populateWithInstructions) ? callForSpeakerRequest.EventUrl : _eventUrlInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventStartDate} | {((!populateWithInstructions) ? callForSpeakerRequest.EventStartDate : CallForSpeakerRequestExtensions._eventStartDateInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventEndDate} | {((!populateWithInstructions) ? callForSpeakerRequest.EventEndDate : CallForSpeakerRequestExtensions._eventEndDateInstructions)} | ");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventLocation} | {((!populateWithInstructions) ? callForSpeakerRequest.EventLocation : _eventLocationInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventCity} | {((!populateWithInstructions) ? callForSpeakerRequest.EventCity : _eventCityInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventCountryCode} | {((!populateWithInstructions) ? callForSpeakerRequest.EventCountryCode : _eventCountryCodeInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventCountryDivisionCode} | {((!populateWithInstructions) ? callForSpeakerRequest.EventCountryDivisionCode : _eventCountryDivisionCodeInstructions)} |");
		response.AppendLine($"{MarkdownCallForSpeakersAttributes.EventTimeZone} | {((!populateWithInstructions) ? callForSpeakerRequest.EventTimeZone : _eventTimeZoneInstructions)} |");
		return response.ToString();
	}

	internal static void AddInstructions(this CallForSpeakerRequest callForSpeaker)
	{
		callForSpeaker.Permalink = _permalinkInstructions;
		callForSpeaker.Status = _statusInstructions;
		callForSpeaker.CallForSpeakerUrl = _callForSpeakersUrlInstructions;
		callForSpeaker.CallForSpeakerStartDate = DateOnly.FromDateTime(DateTime.UtcNow);
		callForSpeaker.CallForSpeakerEndDate = DateOnly.FromDateTime(DateTime.UtcNow);
		callForSpeaker.IncludesSpeakerHonorarium = false;
		callForSpeaker.SpeakerHonorariumAmount = 0;
		callForSpeaker.SpeakerHonorariumCurrency = _honorariumCurrencyInstructions;
		callForSpeaker.SpeakerHonorariumNotes = _honorariumNotesInstructions;
		callForSpeaker.AreTravelExpenseCovered = false;
		callForSpeaker.TravelNotes = _travelNotesInstructions;
		callForSpeaker.AreAccommodationsCovered = false;
		callForSpeaker.AccomodationNotes = _accomodationNotesInstructions;
		callForSpeaker.EventFeeCovered = true;
		callForSpeaker.EventFeeNotes = _eventFeeNotesInstructions;
		callForSpeaker.SubmissionLimit = default;
		callForSpeaker.EventName = _eventNameInstructions;
		callForSpeaker.EventUrl = _eventUrlInstructions;
		callForSpeaker.EventStartDate = DateOnly.FromDateTime(DateTime.UtcNow);
		callForSpeaker.EventEndDate = DateOnly.FromDateTime(DateTime.UtcNow);
		callForSpeaker.EventLocation = _eventLocationInstructions;
		callForSpeaker.EventCity = _eventCityInstructions;
		callForSpeaker.EventCountryCode = _eventCountryCodeInstructions;
		callForSpeaker.EventCountryDivisionCode = _eventCountryDivisionCodeInstructions;
		callForSpeaker.EventTimeZone = _eventTimeZoneInstructions;
	}

}