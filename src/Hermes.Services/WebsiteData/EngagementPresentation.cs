namespace Hermes.WebsiteData;

public class EngagementPresentation
{
	public string Title { get; set; } = null!;
	public string? Subtitle { get; set; }
	public string Description { get; set; } = null!;
	public string Slug { get; set; } = null!;
	public Uri Image { get; set; } = null!;
	public string ImageAltText { get; set; } = null!;
}