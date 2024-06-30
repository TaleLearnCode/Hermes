namespace Hermes.WebsiteData;

public class PastEngagements
{
	public int Year { get; set; }
	public List<Engagement> Engagements { get; set; } = new();
}