namespace Hermes.Extensions;

internal static class EngagementPresentationLearningObjectiveExtensions
{
	internal static LearningObjectiveResponse ToResponse(this EngagementPresentationLearningObjective learningObjective)
		=> new()
		{
			Text = learningObjective.LearningObjectiveText,
			SortOrder = learningObjective.SortOrder
		};
}