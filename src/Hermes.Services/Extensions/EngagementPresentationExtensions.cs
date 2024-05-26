namespace Hermes.Extensions;

internal static class EngagementPresentationExtensions
{

	internal static EngagementPresentationResponse ToResponse(this EngagementPresentation engagementPresentation)
	=> new()
	{
		PresentationPermalink = engagementPresentation.PresentationId,
		Status = engagementPresentation.EngagementPresentationStatus.EngagementPresentationStatusName,
		StatusDescription = engagementPresentation.EngagementPresentationStatus.StatusDescription,
		StartDateTime = engagementPresentation.StartDateTime,
		PresentationLength = engagementPresentation.PresentationLength,
		Room = engagementPresentation.Room,
		EngagementPresentationUrl = engagementPresentation.EngagementPresentationUrl,
		LanguageCode = engagementPresentation.LangaugeCode,
		Title = engagementPresentation.PresentationTitle,
		PresentationShortTitle = engagementPresentation.PresentationShortTitle,
		Abstract = engagementPresentation.Abstract,
		ShortAbstract = engagementPresentation.ShortAbstract,
		ElevatorPitch = engagementPresentation.ElevatorPitch,
		AdditionalDetails = engagementPresentation.AdditionalDetails,
		IsHybrid = engagementPresentation.IsVirtual,
		LearningObjectiveResponses = engagementPresentation.EngagementPresentationLearningObjectives.Select(lo => lo.ToResponse()).ToList(),
		Tags = engagementPresentation.EngagementPresentationTags.Select(t => t.Tag.TagName).ToList()
	};

	internal static List<EngagementPresentationResponse>? ToResponse(this IEnumerable<EngagementPresentation> engagementPresentations)
	{
		if (engagementPresentations.Any())
			return engagementPresentations.Select(ep => ep.ToResponse()).ToList();
		else
			return null;
	}

	internal static string ToMarkdown(this EngagementPresentation engagementPresentation)
	{
		StringBuilder response = new();
		response.AppendLine($"### {engagementPresentation.PresentationTitle}");
		response.AppendLine();
		response.AppendLine($"#### {MarkdownEngagementPresentationHeadings.PresentationAttributes}");
		response.AppendLine("| Attribute | Value |");
		response.AppendLine("|---|---|");
		response.AppendLine($"| {MarkdownEngagementPresentationAttributes.PresentationPermalink} | {engagementPresentation.PresentationId} |");
		response.AppendLine($"| {MarkdownEngagementPresentationAttributes.Status} | {engagementPresentation.EngagementPresentationStatus.EngagementPresentationStatusName} |");
		if (engagementPresentation.StartDateTime is not null)
			response.AppendLine($"| {MarkdownEngagementPresentationAttributes.StartDateTime} | {engagementPresentation.StartDateTime} |");
		response.AppendLine($"| {MarkdownEngagementPresentationAttributes.PresentationLength} | {engagementPresentation.PresentationLength} |");
		if (engagementPresentation.Room is not null)
			response.AppendLine($"| {MarkdownEngagementPresentationAttributes.Room} | {engagementPresentation.Room} |");
		if (engagementPresentation.EngagementPresentationUrl is not null)
			response.AppendLine($"| {MarkdownEngagementPresentationAttributes.EngagementPresentationUrl} | {engagementPresentation.EngagementPresentationUrl} |");
		response.AppendLine($"| {MarkdownEngagementPresentationAttributes.LanguageCode} | {engagementPresentation.LangaugeCode} |");
		response.AppendLine($"| {MarkdownEngagementPresentationAttributes.Title} | {engagementPresentation.PresentationTitle} |");
		if (engagementPresentation.PresentationShortTitle is not null)
			response.AppendLine($"| {MarkdownEngagementPresentationAttributes.PresentationShortTitle} | {engagementPresentation.PresentationShortTitle} |");
		if (engagementPresentation.IsVirtual)
			response.AppendLine($"| {MarkdownEngagementPresentationAttributes.IsVirtual} | {engagementPresentation.IsVirtual} |");
		response.AppendLine();
		response.AppendLine($"#### {MarkdownEngagementPresentationHeadings.Abstract}");
		response.AppendLine(engagementPresentation.Abstract);
		if (engagementPresentation.ShortAbstract is not null)
		{
			response.AppendLine();
			response.AppendLine($"#### {MarkdownEngagementPresentationHeadings.ShortAbstract}");
			response.AppendLine(engagementPresentation.ShortAbstract);
		}
		if (engagementPresentation.ElevatorPitch is not null)
		{
			response.AppendLine();
			response.AppendLine($"#### {MarkdownEngagementPresentationHeadings.ElevatorPitch}");
			response.AppendLine(engagementPresentation.ElevatorPitch);
		}
		if (engagementPresentation.AdditionalDetails is not null)
		{
			response.AppendLine();
			response.AppendLine($"#### {MarkdownEngagementPresentationHeadings.AdditionalDetails}");
			response.AppendLine(engagementPresentation.AdditionalDetails);
		}
		if (engagementPresentation.EngagementPresentationLearningObjectives.Count > 0)
		{
			response.AppendLine();
			response.AppendLine($"#### {MarkdownEngagementPresentationHeadings.LearningObjectives}");
			foreach (EngagementPresentationLearningObjective? learningObjective in engagementPresentation.EngagementPresentationLearningObjectives)
				response.AppendLine($"- {learningObjective.LearningObjectiveText}");
		}
		if (engagementPresentation.EngagementPresentationTags.Count > 0)
		{
			response.AppendLine();
			response.AppendLine($"#### {MarkdownEngagementPresentationHeadings.Tags}");
			foreach (EngagementPresentationTag? tag in engagementPresentation.EngagementPresentationTags)
				response.AppendLine($"- {tag.Tag.TagName}");
		}
		return response.ToString();
	}

	internal static string ToMarkdown(this IEnumerable<EngagementPresentation> engagementPresentations)
	{
		StringBuilder response = new();
		foreach (EngagementPresentation engagementPresentation in engagementPresentations)
		{
			response.AppendLine(engagementPresentation.ToMarkdown());
			response.AppendLine();
		}
		return response.ToString();
	}

}