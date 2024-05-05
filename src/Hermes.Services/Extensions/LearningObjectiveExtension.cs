using Hermes.Responses;

namespace Hermes.Extensions;

internal static class LearningObjectiveExtension
{

	internal static LearningObjectiveRequest ToLearningObjectiveRequest(this LearningObjective input)
		=> new()
		{
			Text = input.LearningObjectiveText,
			SortOrder = input.SortOrder
		};

	internal static LearningObjectiveResponse ToLearningObjectiveResponse(this LearningObjective input)
		=> new()
		{
			Text = input.LearningObjectiveText,
			SortOrder = input.SortOrder
		};

}