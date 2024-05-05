using Hermes.Responses;
using Hermes.Types;
using Spectre.Console;
using System.Text;

namespace Hermes.Extensions;

internal static class PresentationExtensions
{

	internal static PresentationRequest ToPresentationRequest(this Presentation input)
		=> new()
		{
			//LanguageCode = input.LanguageCode,
			//Title = input.PresentationTitle,
			//ShortTitle = input.PresentationShortTitle,
			//Abstract = input.Abstract,
			//ShortAbstract = input.ShortAbstract,
			//ElevatorPitch = input.ElevatorPitch,
			//AdditionalDetails = input.AdditionalDetails,
			//LearningObjectives = input.LearningObjectives.Select(x => x.ToLearningObjectiveRequest()).ToList()
			Type = input.PresentationType.PresentationTypeName,
			Status = input.PresentationStatus.PresentationStatusName,
			PublicRepoLink = input.PublicRepoLink,
			PrivateRepoLink = input.PrivateRepoLink,
			Permalink = input.Permalink,
			IsArchived = input.IsArchived,
			IncludeInPublicProfile = input.IncludeInPublicProfile,
			DefaultLanguageCode = input.DefaultLanguageCode,
			Texts = input.PresentationTexts.Select(x => x.ToPresentationTextRequest()).ToList(),
			Tags = input.PresentationTags.Select(x => x.Tag.TagName).ToList()
		};

	internal static PresentationResponse ToPresentationResponse(this Presentation input)
		=> new()
		{
			Type = input.PresentationType.PresentationTypeName,
			Status = input.PresentationStatus.PresentationStatusName,
			PublicRepoLink = input.PublicRepoLink,
			PrivateRepoLink = input.PrivateRepoLink,
			Permalink = input.Permalink,
			IsArchived = input.IsArchived,
			IncludeInPublicProfile = input.IncludeInPublicProfile,
			DefaultLanguageCode = input.DefaultLanguageCode,
			Texts = input.PresentationTexts.Select(x => x.ToPresentationTextResponse()).ToList(),
			Tags = input.PresentationTags.Select(x => x.Tag.TagName).ToList()
		};

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