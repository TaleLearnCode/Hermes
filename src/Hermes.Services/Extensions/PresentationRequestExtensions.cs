using Hermes.Types;
using System.Text;

namespace Hermes.Extensions;

internal static class PresentationRequestExtensions
{

	internal static string ToMarkdown(this PresentationRequest presentationRequest)
	{
		StringBuilder response = new();
		response.AppendLine($"## {MarkdownPresentationHeadings.PresentationAttributes}");
		response.AppendLine("| Attribute | Value |");
		response.AppendLine("|---|---|");
		response.AppendLine($"{MarkdownPresentationAttributes.Permalink} | {presentationRequest.Permalink} |");
		response.AppendLine($"{MarkdownPresentationAttributes.Title} | {presentationRequest.Title} | ");
		response.AppendLine($"{MarkdownPresentationAttributes.ShortTitle} | {presentationRequest.ShortTitle}  | ");
		response.AppendLine($"{MarkdownPresentationAttributes.PresentationType} | {presentationRequest.PresentationType}  | ");
		response.AppendLine($"{MarkdownPresentationAttributes.PresentationStatus} | {presentationRequest.PresentationStatus}   | ");
		response.AppendLine($"{MarkdownPresentationAttributes.PublicRepoLink} | {presentationRequest.PublicRepoLink} |");
		response.AppendLine($"{MarkdownPresentationAttributes.PrivateRepoLink} | {presentationRequest.PrivateRepoLink} |");
		response.AppendLine($"{MarkdownPresentationAttributes.Thumbnail} | {presentationRequest.Thumbnail} |");
		response.AppendLine($"{MarkdownPresentationAttributes.IncludeInPublicProfile} | {presentationRequest.IncludeInPublicProfile.ToString()} |");
		response.AppendLine($"{MarkdownPresentationAttributes.DefaultLanguageCode} | {presentationRequest.DefaultLanguageCode} |");
		response.AppendLine($"{MarkdownPresentationAttributes.IsArchived} | {presentationRequest.IsArchived.ToString()} |");
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.ElevatorPitch}");
		response.AppendLine(presentationRequest.ElevatorPitch);
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.ShortAbstract}");
		response.AppendLine(presentationRequest.ShortAbstract);
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.Abstract}");
		response.AppendLine(presentationRequest.Abstract);
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.Tags}");
		foreach (string? tag in presentationRequest.Tags)
			response.AppendLine($"- {tag}");
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.LearningObjectives}");
		foreach (string learningObjective in presentationRequest.LearningObjectives)
			response.AppendLine($"- {learningObjective}");
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.Resources}");
		response.AppendLine(presentationRequest.Resources);
		response.AppendLine();
		response.AppendLine($"## {MarkdownPresentationHeadings.AdditionalDetails}");
		response.AppendLine(presentationRequest.AdditionalDetails);

		return response.ToString();

	}

}