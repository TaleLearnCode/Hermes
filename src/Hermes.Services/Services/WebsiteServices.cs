using Hermes.WebsiteData;

namespace Hermes.Services;

public class WebsiteServices(string databaseConnectionString) : ServicesBase(databaseConnectionString)
{

	public async Task<string> GetEngagementsAsync(string outputPath)
	{

		using HermesContext context = new(_databaseConnectionString);
		List<Models.Engagement> engagements = await context.Engagements
			.Include(e => e.EngagementPresentations)
				.ThenInclude(x => x.Presentation)
					.ThenInclude(x => x.PresentationTexts.Where(x => x.LanguageCode == "en"))
			.Include(e => e.EngagementPresentations)
				.ThenInclude(x => x.EngagementPresentationStatus)
			.Include(c => c.CountryCodeNavigation)
			.Include(s => s.EngagementStatus)
			.Where(e => e.EngagementStatus.DisplayOnWebsite && e.IncludeInPublicProfile)
			.OrderByDescending(e => e.StartDate)
			.ToListAsync();

		List<WebsiteData.Engagement> upcomingEngagements = [];
		Dictionary<int, List<WebsiteData.Engagement>> pastEngagements = [];
		foreach (Models.Engagement engagement in engagements)
		{
			WebsiteData.Engagement websiteEngagement = new()
			{
				Title = engagement.EngagementName,
				Date = engagement.WebsiteDataDate(),
				Location = engagement.WebsiteDataLocation(),
				Venue = engagement.Venue,
				// TODO: Add image to Engagement
				//Image = new Uri(engagement.ImageUrl),
				// TODO: Determine proper alt text for image (engagement name; something more descriptive)
				ImageAltText = engagement.EngagementName,
				// TODO: Add description to Engagement
				Description = engagement.EngagementDescription,
				Slug = engagement.Permalink,
				WebsiteUrl = string.IsNullOrWhiteSpace(engagement.EngagementUrl) ? null : new Uri(engagement.EngagementUrl),
				// TODO: Add registration URL to Engagement
				//RegistrationUrl = string.IsNullOrWhiteSpace(engagement.RegistrationUrl) ? null : new Uri(engagement.RegistrationUrl)
			};
			foreach (Models.EngagementPresentation presentation in engagement.EngagementPresentations.Where(x => x.EngagementPresentationStatus.IncludeOnWebsite))
			{
				websiteEngagement.Presentations.Add(new()
				{
					Title = presentation.Presentation.PresentationTexts.First().PresentationTitle,
					// TODO: Add subtitle to EngagementPresentation
					//Subtitle = presentation.Presentation.PresentationTexts.First().Subtitle,
					// TODO: Should we be using the short abstract or the elevator pitch or something else?
					Description = presentation.Presentation.PresentationTexts.First().ShortAbstract,
					Slug = presentation.Presentation.Permalink,
					// TODO: Add image to EngagementPresentation
					//Image = new Uri(presentation.Presentation.ImageUrl),
					Image = new Uri("https://via.placeholder.com/150"),
					// TODO: Determine proper alt text for image (presentation title; something more descriptive)
					ImageAltText = presentation.Presentation.PresentationTexts.First().PresentationTitle
				});
			}
			if (engagement.StartDate >= (DateOnly.FromDateTime(DateTime.UtcNow)).AddDays(1))
			{
				upcomingEngagements.Add(websiteEngagement);
			}
			else
			{
				pastEngagements.TryAdd(engagement.StartDate.Year, []);
				pastEngagements[engagement.StartDate.Year].Add(websiteEngagement);
			}
		}

		Engagements websiteData = new() { Upcoming = upcomingEngagements, };
		foreach (KeyValuePair<int, List<WebsiteData.Engagement>> pastEngagement in pastEngagements)
			websiteData.Past.Add(new() { Year = pastEngagement.Key, Engagements = pastEngagement.Value });

		return SaveFile<Engagements>(outputPath, JsonSerializer.Serialize(websiteData, _jsonSerializerOptions));

	}



}