using Hermes.Responses;

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

}