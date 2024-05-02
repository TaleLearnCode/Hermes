namespace Hermes.Extensions;

internal static class PresentationTextRequestExtensions
{

	internal static PresentationText ToPresentationText(this PresentationTextRequest presentationTextRequest, int presentationId = default)
	=> new()
	{
		PresentationTextId = presentationTextRequest.Id,
		PresentationId = presentationId,
		LanguageCode = presentationTextRequest.LanguageCode,
		PresentationTitle = presentationTextRequest.Title,
		PresentationShortTitle = presentationTextRequest.ShortTitle,
		Abstract = presentationTextRequest.Abstract,
		ShortAbstract = presentationTextRequest.ShortAbstract,
		ElevatorPitch = presentationTextRequest.ElevatorPitch,
		AdditionalDetails = presentationTextRequest.AdditionalDetails,
		LearningObjectives = presentationTextRequest.LearningObjectives?.ToLeaningObjectiveList(presentationId)
	};

	internal static List<PresentationText> ToPresentationTextList(this List<PresentationTextRequest> presentationTextRequests, int presentationId = default)
		=> presentationTextRequests.Select(presentationTextRequest => presentationTextRequest.ToPresentationText(presentationId)).ToList();

}