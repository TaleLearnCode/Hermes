using System.Text;

namespace Hermes.Types;

internal class SubmissionDetail
{
	internal string? PresentationPermalink { get; set; } = null;
	internal string Status { get; set; } = "Under Review";
	internal DateOnly SubmissionDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
	internal DateOnly? DecisionDate { get; set; } = null;
	internal string LanguageCode { get; set; } = "en";
	internal string? Title { get; set; } = null;
	internal int? Length { get; set; } = 60;
	internal string? Track { get; set; } = null;
	internal string? Level { get; set; } = null;
	internal StringBuilder Description { get; set; } = new();
	internal StringBuilder ElevatorPitch { get; set; } = new();
	internal StringBuilder AdditionalDetails { get; set; } = new();
	internal List<string> LearningObjectives { get; set; } = [];
	internal List<string> Tags { get; set; } = [];
}