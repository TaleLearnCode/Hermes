namespace Hermes.Extensions;

internal static class EngagementExtensions
{

	internal static EngagementResponse ToResponse(this Engagement engagement)
		=> new()
		{
			Permalink = engagement.Permalink,
			EngagementType = engagement.EngagementType.EngagementTypeName,
			EngagementTypeDescription = engagement.EngagementType.TypeDescription,
			Status = engagement.EngagementStatus.EngagementStatusName,
			StatusDescription = engagement.EngagementStatus.StatusDescription,
			Name = engagement.EngagementName,
			Country = engagement.CountryCodeNavigation.ToResponse(),
			CountryDivision = engagement.CountryDivision?.ToResponse(),
			City = engagement.City,
			Venue = engagement.Venue,
			OverviewLocation = engagement.OverviewLocation,
			ListingLocation = engagement.ListingLocation,
			StartDate = engagement.StartDate,
			EndDate = engagement.EndDate,
			TimeZone = engagement.TimeZone.TimeZoneId,
			LanguageCode = engagement.LanguageCode,
			Description = engagement.EngagementDescription,
			Summary = engagement.EngagementSummary,
			Url = engagement.EngagementUrl,
			IncludeInPublicProfile = engagement.IncludeInPublicProfile,
			IsVirtual = engagement.IsVirtual,
			IsHybrid = engagement.IsHybrid,
			IsPublic = engagement.IsPublic,
			IsEnabled = engagement.IsEnabled,
			CallForSpeakerDetails = engagement.EngagementCallForSpeaker?.ToResponse(),
			Presentations = engagement.EngagementPresentations.ToResponse() ?? null
		};

	internal static string ToMarkdown(this Engagement engagement)
	{
		StringBuilder response = new();
		AddDocumentTitle(engagement, response);
		AddTemplateAttributes(response);
		AddEngagementAttributes(engagement, response);
		AddEngagementDetails(engagement, response);
		AddCallForSpeakerAttributes(engagement, response);
		AddPresentations(engagement, response);
		return response.ToString();
	}

	private static void AddDocumentTitle(Engagement engagement, StringBuilder response)
	{
		response.AppendLine($"# {engagement.EngagementName}");
		response.AppendLine();
	}

	private static void AddTemplateAttributes(StringBuilder response)
	{
		response.AppendLine($"## {MarkdownEngagementHeadings.TemplateAttributes}");
		response.AppendLine("| Attribute | Value |");
		response.AppendLine("|---|---|");
		response.AppendLine("| Template | engagement |");
		response.AppendLine("| Template Version | 1.0 |");
		response.AppendLine();
	}

	private static void AddEngagementAttributes(Engagement engagement, StringBuilder response)
	{
		response.AppendLine($"## {MarkdownEngagementHeadings.EngagementAttributes}");
		response.AppendLine("| Attribute | Value |");
		response.AppendLine("|---|---|");
		response.AppendLine($"{MarkdownEngagementAttributes.Permalink} | {engagement.Permalink} |");
		response.AppendLine($"{MarkdownEngagementAttributes.EngagementType} | {engagement.EngagementType.EngagementTypeName} |");
		response.AppendLine($"{MarkdownEngagementAttributes.Status} | {engagement.EngagementStatus.EngagementStatusName} |");
		response.AppendLine($"{MarkdownEngagementAttributes.Name} | {engagement.EngagementName} |");
		response.AppendLine($"{MarkdownEngagementAttributes.CountryCode} | {engagement.CountryCodeNavigation.CountryCode} |");
		if (engagement.CountryDivision is not null)
			response.AppendLine($"{MarkdownEngagementAttributes.CountryDivisionCode} | {engagement.CountryDivision.CountryDivisionCode} |");
		response.AppendLine($"{MarkdownEngagementAttributes.City} | {engagement.City} |");
		if (engagement.Venue is not null)
			response.AppendLine($"{MarkdownEngagementAttributes.Venue} | {engagement.Venue} |");
		if (engagement.OverviewLocation is not null)
			response.AppendLine($"{MarkdownEngagementAttributes.OverviewLocation} | {engagement.OverviewLocation} |");
		if (engagement.ListingLocation is not null)
			response.AppendLine($"{MarkdownEngagementAttributes.ListingLocation} | {engagement.ListingLocation} |");
		response.AppendLine($"{MarkdownEngagementAttributes.StartDate} | {engagement.StartDate} |");
		response.AppendLine($"{MarkdownEngagementAttributes.EndDate} | {engagement.EndDate} |");
		response.AppendLine($"{MarkdownEngagementAttributes.TimeZone} | {engagement.TimeZone.TimeZoneId} |");
		response.AppendLine($"{MarkdownEngagementAttributes.LanguageCode} | {engagement.LanguageCode} |");
		response.AppendLine($"{MarkdownEngagementAttributes.Url} | {engagement.EngagementUrl} |");
		response.AppendLine($"{MarkdownEngagementAttributes.IncludeInPublicProfile} | {engagement.IncludeInPublicProfile} |");
		response.AppendLine($"{MarkdownEngagementAttributes.IsVirtual} | {engagement.IsVirtual} |");
		response.AppendLine($"{MarkdownEngagementAttributes.IsHybrid} | {engagement.IsHybrid} |");
		response.AppendLine($"{MarkdownEngagementAttributes.IsPublic} | {engagement.IsPublic} |");
		response.AppendLine($"{MarkdownEngagementAttributes.IsEnabled} | {engagement.IsEnabled} |");
		response.AppendLine();
	}

	private static void AddEngagementDetails(Engagement engagement, StringBuilder response)
	{
		if (!string.IsNullOrWhiteSpace(engagement.EngagementDescription) || !string.IsNullOrWhiteSpace(engagement.EngagementSummary))
		{
			response.AppendLine($"## {MarkdownEngagementHeadings.EngagementDetails}");
			if (!string.IsNullOrWhiteSpace(engagement.EngagementDescription))
			{
				response.AppendLine();
				response.AppendLine($"### {MarkdownEngagementDetailHeadings.Description}");
				response.AppendLine(engagement.EngagementDescription);
			}
			if (!string.IsNullOrWhiteSpace(engagement.EngagementSummary))
			{
				response.AppendLine();
				response.AppendLine($"### {MarkdownEngagementDetailHeadings.Summary}");
				response.AppendLine(engagement.EngagementSummary);
			}
		}
	}

	private static void AddCallForSpeakerAttributes(Engagement engagement, StringBuilder response)
	{
		if (engagement.EngagementCallForSpeaker is not null)
		{
			response.AppendLine();
			response.AppendLine($"## {MarkdownEngagementHeadings.CallForSpeakerAttributes}");
			response.AppendLine(engagement.EngagementCallForSpeaker.ToMarkdown());
		}
	}

	private static void AddPresentations(Engagement engagement, StringBuilder response)
	{
		if (engagement.EngagementPresentations.Count > 0)
		{
			response.AppendLine();
			response.AppendLine($"## {MarkdownEngagementHeadings.Presentations}");
			foreach (EngagementPresentation? presentation in engagement.EngagementPresentations)
			{
				response.AppendLine();
				response.AppendLine(presentation.ToMarkdown());
			}
		}
	}

}