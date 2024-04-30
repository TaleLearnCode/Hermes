namespace Hermes.Services;

public class PresentationServices(string databaseConnectionString)
{

	private readonly string _databaseConnectionString = databaseConnectionString;

	public async Task<Presentation> CreatePresentationAsync(PresentationRequest presentationRequest)
	{
		HermesContext context = new(_databaseConnectionString);
		Presentation presentation = presentationRequest.ToPresentation();
		if (presentationRequest.Tags is not null && presentationRequest.Tags.Count > 0)
			presentation.PresentationTags = presentationRequest.Tags.ToPresentationTagList(context);
		await context.Presentations.AddAsync(presentation);
		await context.SaveChangesAsync();
		return presentation;
	}

}