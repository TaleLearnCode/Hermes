using Hermes.Responses;

namespace Hermes.Extensions;

internal static class PresentationTextExtensions
{

	internal static PresentationTextRequest ToPresentationTextRequest(this PresentationText input)
		=> new()
		{
			LanguageCode = input.LanguageCode,
			Title = input.PresentationTitle,
			ShortTitle = input.PresentationShortTitle,
			Abstract = input.Abstract,
			ShortAbstract = input.ShortAbstract,
			ElevatorPitch = input.ElevatorPitch,
			AdditionalDetails = input.AdditionalDetails,
			LearningObjectives = input.LearningObjectives.Select(x => x.ToLearningObjectiveRequest()).ToList()
		};

	internal static PresentationTextResponse ToPresentationTextResponse(this PresentationText input)
		=> new()
		{
			LanguageCode = input.LanguageCode,
			Title = input.PresentationTitle,
			ShortTitle = input.PresentationShortTitle,
			Abstract = input.Abstract,
			ShortAbstract = input.ShortAbstract,
			ElevatorPitch = input.ElevatorPitch,
			AdditionalDetails = input.AdditionalDetails,
			LearningObjectives = input.LearningObjectives.Select(x => x.ToLearningObjectiveResponse()).ToList()
		};

}