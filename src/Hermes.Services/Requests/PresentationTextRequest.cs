namespace Hermes.Requests;

public class PresentationTextRequest
{
	public int Id { get; set; }
	public required string LanguageCode { get; set; }
	public required string Title { get; set; }
	public string? ShortTitle { get; set; }
	public string? Abstract { get; set; }
	public string? ShortAbstract { get; set; }
	public string? ElevatorPitch { get; set; }
	public string? AdditionalDetails { get; set; }
	public List<LearningObjectiveRequest>? LearningObjectives { get; set; }
}