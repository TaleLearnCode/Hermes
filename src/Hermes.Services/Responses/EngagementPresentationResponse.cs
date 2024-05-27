namespace Hermes.Responses;

public class EngagementPresentationResponse
{
	public string PresentationPermalink { get; set; } = null!;
	public string Status { get; set; } = null!;
	public string? StatusDescription { get; set; }
	public DateTime? StartDateTime { get; set; }
	public int PresentationLength { get; set; }
	public string? Room { get; set; }
	public string? EngagementPresentationUrl { get; set; }
	public string LanguageCode { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string? PresentationShortTitle { get; set; }
	public string Abstract { get; set; } = null!;
	public string? ShortAbstract { get; set; }
	public string? ElevatorPitch { get; set; }
	public string? AdditionalDetails { get; set; }
	public bool IsHybrid { get; set; }
	public List<LearningObjectiveResponse>? LearningObjectiveResponses { get; set; }
	public List<string>? Tags { get; set; }
	public List<EngagementPresentationDownloadResponse>? Downloads { get; set; }
}