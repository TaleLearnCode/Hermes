namespace Hermes.Extensions;

internal static class EngagementCallForSpeakerExtensions
{

	internal static EngagementCallForSpeakerResponse ToResponse(this EngagementCallForSpeaker callForSpeaker)
	=> new()
	{
		Status = callForSpeaker.EngagementCallForSpeakerStatus.EngagementCallForSpeakerStatusName,
		StatusDescription = callForSpeaker.EngagementCallForSpeakerStatus.StatusDescription,
		Url = callForSpeaker.CallForSpeakersUrl,
		StartDate = callForSpeaker.StartDate,
		EndDate = callForSpeaker.EndDate,
		ExpectedDecisionDate = callForSpeaker.ExpectedDecisionDate,
		ActualDecisionDate = callForSpeaker.ActualDecisionDate,
		IncludesSpeakerHonorarium = callForSpeaker.SpeakerHonorarium,
		SpeakerHonorariumAmount = callForSpeaker.SpeakerHonorariumAmount,
		SpeakerHonorariumCurrencyCode = callForSpeaker.SpeakerHonorariumCurrency,
		SpeakerHonorariumNotes = callForSpeaker.SpeakerHonorariumNotes,
		IncludesTravelExpenses = callForSpeaker.TravelExpensesCovered,
		TravelExpensesNotes = callForSpeaker.TravelExpensesNotes,
		IncludesAccommodation = callForSpeaker.AccommodationCovered,
		AccommodationNotes = callForSpeaker.AccommodationNotes,
		EventFeeCovered = callForSpeaker.EventFeeCovered,
		EventFeeNotes = callForSpeaker.EventFeeNotes,
		SubmissionLimit = callForSpeaker.SubmissionLimit
	};

	internal static string ToMarkdown(this EngagementCallForSpeaker callForSpeaker)
	{
		StringBuilder response = new();
		response.AppendLine($"## {MarkdownEngagementHeadings.CallForSpeakerAttributes}");
		response.AppendLine("| Attribute | Value |");
		response.AppendLine("|---|---|");
		response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.Status} | {callForSpeaker.EngagementCallForSpeakerStatus.EngagementCallForSpeakerStatusName} |");
		response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.Url} | {callForSpeaker.CallForSpeakersUrl} |");
		response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.StartDate} | {callForSpeaker.StartDate} |");
		response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.EndDate} | {callForSpeaker.EndDate} |");
		if (callForSpeaker.ExpectedDecisionDate is not null)
			response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.ExpectedDecisionDate} | {callForSpeaker.ExpectedDecisionDate} |");
		if (callForSpeaker.ActualDecisionDate is not null)
			response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.ActualDecisionDate} | {callForSpeaker.ActualDecisionDate} |");
		response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.IncludesSpeakerHonorarium} | {callForSpeaker.SpeakerHonorarium} |");
		if (callForSpeaker.SpeakerHonorariumAmount is not null)
			response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.SpeakerHonorariumAmount} | {callForSpeaker.SpeakerHonorariumAmount} |");
		if (callForSpeaker.SpeakerHonorariumCurrency is not null)
			response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.SpeakerHonorariumCurrencyCode} | {callForSpeaker.SpeakerHonorariumCurrency} |");
		if (callForSpeaker.SpeakerHonorariumNotes is not null)
			response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.SpeakerHonorariumNotes} | {callForSpeaker.SpeakerHonorariumNotes} |");
		response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.IncludesTravelExpenses} | {callForSpeaker.TravelExpensesCovered} |");
		if (callForSpeaker.TravelExpensesNotes is not null)
			response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.TravelExpensesNotes} | {callForSpeaker.TravelExpensesNotes} |");
		response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.IncludesAccommodation} | {callForSpeaker.AccommodationCovered} |");
		if (callForSpeaker.AccommodationNotes is not null)
			response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.AccommodationNotes} | {callForSpeaker.AccommodationNotes} |");
		response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.EventFeeCovered} | {callForSpeaker.EventFeeCovered} |");
		if (callForSpeaker.EventFeeNotes is not null)
			response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.EventFeeNotes} | {callForSpeaker.EventFeeNotes} |");
		response.AppendLine($"{MarkdownEngagementCallForSpeakerAttributes.SubmissionLimit} | {callForSpeaker.SubmissionLimit} |");
		return response.ToString();
	}

}