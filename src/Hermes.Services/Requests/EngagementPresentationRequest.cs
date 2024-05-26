#nullable disable

namespace Hermes.Requests;

public class EngagementPresentationRequest
{
	public string PresentationPermalink { get; set; }
	public string Status { get; set; }
	public DateTime? StartDateTime { get; set; }
	public int PresentationLength { get; set; }
	public string Room { get; set; }
	public string EngagementPresentationUrl { get; set; }
	public string LanguageCode { get; set; } = "en";
	public string Title { get; set; }
	public string PresentationShortTitle { get; set; }
	public string Abstract { get; set; }
	public string ShortAbstract { get; set; }
	public string ElevatorPitch { get; set; }
	public string AdditionalDetails { get; set; }
	public List<LearningObjectiveRequest> LearningObjectiveRequests { get; set; } = [];
	public List<string> Tags { get; set; } = [];
}