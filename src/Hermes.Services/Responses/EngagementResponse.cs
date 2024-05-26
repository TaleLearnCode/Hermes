namespace Hermes.Responses;

public class EngagementResponse
{
	public string Permalink { get; set; } = null!;
	public string EngagementType { get; set; } = null!;
	public string? EngagementTypeDescription { get; set; }
	public string Status { get; set; } = null!;
	public string? StatusDescription { get; set; }
	public string Name { get; set; } = null!;
	public CountryResponse Country { get; set; } = null!;
	public CountryDivisionResponse? CountryDivision { get; set; }
	public string City { get; set; } = null!;
	public string? Venue { get; set; }
	public string? OverviewLocation { get; set; }
	public string? ListingLocation { get; set; }
	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }
	public string TimeZone { get; set; } = null!;
	public string LanguageCode { get; set; } = null!;
	public string? Description { get; set; }
	public string? Summary { get; set; }
	public string? Url { get; set; }
	public bool IncludeInPublicProfile { get; set; }
	public bool IsVirtual { get; set; }
	public bool IsHybrid { get; set; }
	public bool IsPublic { get; set; }
	public bool IsEnabled { get; set; }
	public EngagementCallForSpeakerResponse? CallForSpeakerDetails { get; set; }
	public List<EngagementPresentationResponse>? Presentations { get; set; }
}