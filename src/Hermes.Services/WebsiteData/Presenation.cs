namespace Hermes.WebsiteData;

public class Presentation
{
	public string Title { get; set; } = null!;
	public string? Subtitle { get; set; }
	public string Description { get; set; } = null!;
	public string Abstract { get; set; } = null!;
	public List<string> LearningObjectives { get; set; } = [];
	public string Slug { get; set; } = null!;
	public Uri? Image { get; set; }
	public string ImageAltText { get; set; } = null!;
	public List<RelatedPresentation> RelatedPresentation { get; set; } = [];
	public List<string> Tags { get; set; } = [];
	public string Duration { get; set; } = null!;
	public Uri? RepoUrl { get; set; } = null!;
	public List<Engagement> Engagements { get; set; } = [];
	public string Resources { get; set; } = null!;
}