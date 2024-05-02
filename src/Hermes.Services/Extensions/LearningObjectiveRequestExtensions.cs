//namespace Hermes.Extensions;

//internal static class LearningObjectiveRequestExtensions
//{

//	internal static LearningObjective ToLearningObjective(this LearningObjectiveRequest learningObjectiveRequest, int presentationTextId = default)
//	=> new()
//	{
//		LearningObjectiveId = learningObjectiveRequest.Id,
//		PresentationTextId = presentationTextId,
//		LearningObjectiveText = learningObjectiveRequest.Text,
//		SortOrder = learningObjectiveRequest.SortOrder
//	};

//	internal static List<LearningObjective> ToLeaningObjectiveList(this List<LearningObjectiveRequest> learningObjectiveRequests, int presentationTextId = default)
//		=> learningObjectiveRequests.Select(learningObjectiveRequest => learningObjectiveRequest.ToLearningObjective(presentationTextId)).ToList();

//}