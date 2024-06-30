namespace Hermes.WebsiteData;

public class Engagement
{
	public string Title { get; set; } = null!;
	public string Date { get; set; } = null!;
	public string Location { get; set; } = null!;
	public string Venue { get; set; } = null!;
	public Uri? Image { get; set; }
	public string? ImageAltText { get; set; }
	public string Description { get; set; } = null!;
	public Uri? WebsiteUrl { get; set; }
	public Uri? RegistrationUrl { get; set; }
	public List<EngagementPresentation> Presentations { get; set; } = new();
	public string Slug { get; set; } = null!;
}