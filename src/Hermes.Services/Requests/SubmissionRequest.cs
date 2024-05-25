namespace Hermes.Requests;

public class SubmissionRequest
{
	public string Status { get; set; } = "Under Review";
	public DateOnly SubmissionDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
	public DateOnly? ExpectedDecisionDate { get; set; } = null;
	public DateOnly? DecisionDate { get; set; } = null;
	public string LanguageCode { get; set; } = "en";
	public string PresentationPermalink { get; set; } = null!;
	public string? SessionTitle { get; set; } = null;
	public string? SessionDescription { get; set; } = null;
	public int? SessionLength { get; set; } = null;
	public string? SessionTrack { get; set; } = null;
	public string? SessionLevel { get; set; } = null;
	public string? ElevatorPitch { get; set; } = null;
	public string? AdditionalDetails { get; set; } = null;
	public List<string>? LearningObjectives { get; set; } = null;
	public List<string>? Tags { get; set; } = null;
}