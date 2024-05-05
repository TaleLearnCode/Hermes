using Hermes.Types;
using Spectre.Console;
using System.Text;

namespace Hermes.Extensions;

internal static class PresentationExtensions
{

	internal static PresentationRequest ToPresentationRequest(this Presentation presentation)
	{
		PresentationText presentationText = presentation.PresentationTexts
			.Where(x => x.LanguageCode == presentation.DefaultLanguageCode).FirstOrDefault()
			?? presentation.PresentationTexts.First();
		return new()
		{
			Title = presentationText.PresentationTitle,
			ShortTitle = presentationText.PresentationShortTitle,
			ElevatorPitch = presentationText.ElevatorPitch,
			ShortAbstract = presentationText.ShortAbstract,
			Abstract = presentationText.Abstract,
			Resources = presentation.Resources,
			AdditionalDetails = presentationText.AdditionalDetails,
			Permalink = presentation.Permalink,
			PresentationType = presentation.PresentationType.PresentationTypeName,
			PresentationStatus = presentation.PresentationStatus.PresentationStatusName,
			PublicRepoLink = presentation.PublicRepoLink.ToString(),
			PrivateRepoLink = presentation.PrivateRepoLink.ToString(),
			Thumbnail = presentation.OriginalThumbnailUrl.ToString(),
			IncludeInPublicProfile = presentation.IncludeInPublicProfile,
			DefaultLanguageCode = presentation.DefaultLanguageCode,
			IsArchived = presentation.IsArchived,
			LearningObjectives = presentationText.LearningObjectives.Select(x => x.LearningObjectiveText).ToList(),
			Tags = presentation.PresentationTags.Select(x => x.Tag.TagName).ToList()
		};
	}

	internal static string ToMarkdown(this Presentation presentation)
	{

		PresentationText presentationText = presentation.PresentationTexts
			.Where(x => x.LanguageCode == presentation.DefaultLanguageCode).FirstOrDefault()
			?? presentation.PresentationTexts.First();

		StringBuilder response = new();
		response.AppendLine($"## {MarkdownPresentationHeadings.PresentationAttributes}");
		response.AppendLine("| Attribute | Value |");
		response.AppendLine("|---|---|");
		response.AppendLine($"{MarkdownPresentationAttributes.Permalink} | {presentation.Permalink} |");
		response.AppendLine($"{MarkdownPresentationAttributes.Title} | {presentationText.PresentationTitle} |");
		response.AppendLine($"{MarkdownPresentationAttributes.ShortTitle} | {presentationText.PresentationShortTitle} |");
		response.AppendLine($"{MarkdownPresentationAttributes.PresentationType} | {presentation.PresentationType.PresentationTypeName} |");
		response.AppendLine($"{MarkdownPresentationAttributes.PresentationStatus} | {presentation.PresentationStatus.PresentationStatusName} |");
		response.AppendLine($"{MarkdownPresentationAttributes.PublicRepoLink} | {presentation.PublicRepoLink} |");
		response.AppendLine($"{MarkdownPresentationAttributes.PrivateRepoLink} | {presentation.PrivateRepoLink} |");
		response.AppendLine($"{MarkdownPresentationAttributes.Thumbnail} | {presentation.OriginalThumbnailUrl} |");
		response.AppendLine($"{MarkdownPresentationAttributes.IncludeInPublicProfile} | {presentation.IncludeInPublicProfile.ToString()} |");
		response.AppendLine($"{MarkdownPresentationAttributes.DefaultLanguageCode} | {presentation.DefaultLanguageCode} |");
		response.AppendLine($"{MarkdownPresentationAttributes.IsArchived} | {presentation.IsArchived.ToString()} |");
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.ElevatorPitch}");
		response.AppendLine(presentationText.ElevatorPitch);
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.ShortAbstract}");
		response.AppendLine(presentationText.ShortAbstract);
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.Abstract}");
		response.AppendLine(presentationText.Abstract);
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.Tags}");
		foreach (PresentationTag presentationTag in presentation.PresentationTags)
			response.AppendLine($"- {presentationTag.Tag.TagName}");
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.LearningObjectives}");
		foreach (LearningObjective learningObjective in presentationText.LearningObjectives)
			response.AppendLine($"- {learningObjective.LearningObjectiveText}");
		response.AppendLine($"## {MarkdownPresentationHeadings.Resources}");
		response.AppendLine(presentation.Resources);
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.AdditionalDetails}");
		response.AppendLine(presentationText.AdditionalDetails);


		return response.ToString();

	}

}