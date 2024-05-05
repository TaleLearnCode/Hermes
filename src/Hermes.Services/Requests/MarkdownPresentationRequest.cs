#nullable disable

namespace Hermes.Requests;

public class MarkdownPresentationRequest
{
	public string Title { get; set; }
	public string ShortTitle { get; set; }
	public string ElevatorPitch { get; set; }
	public string ShortAbstract { get; set; }
	public string Abstract { get; set; }
	public string Resources { get; set; }
	public string AdditionalDetails { get; set; }
	public string Permalink { get; set; }
	public string PresentationType { get; set; }
	public string PresentationStatus { get; set; }
	public string PublicRepoLink { get; set; }
	public string PrivateRepoLink { get; set; }
	public string Thumbnail { get; set; }
	public bool IncludeInPublicProfile { get; set; }
	public string DefaultLanguageCode { get; set; }
	public bool IsArchived { get; set; }
	public List<string> LearningObjectives { get; set; } = [];
	public List<string> Tags { get; set; } = [];
}