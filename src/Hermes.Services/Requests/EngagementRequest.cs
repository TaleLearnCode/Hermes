#nullable disable

namespace Hermes.Requests;

public class EngagementRequest
{
	public string Permalink { get; set; }
	public string EngagementType { get; set; }
	public string Status { get; set; }
	public string Name { get; set; }
	public string CountryCode { get; set; }
	public string CountryDivisionCode { get; set; }
	public string City { get; set; }
	public string Venue { get; set; }
	public string OverviewLocation { get; set; }
	public string ListingLocation { get; set; }
	public DateOnly? StartDate { get; set; }
	public DateOnly? EndDate { get; set; }
	public string TimeZone { get; set; }
	public string LanguageCode { get; set; }
	public string Description { get; set; }
	public string Summary { get; set; }
	public string Url { get; set; }
	public bool IncludeInPublicProfile { get; set; }
	public bool IsVirtual { get; set; }
	public bool IsPublic { get; set; }
	public bool IsEnabled { get; set; }
	public EngagementCallForSpeakerRequest? CallForSpeakerDetails { get; set; }
	public List<EngagementPresentationRequest> Presentations { get; set; } = [];
}