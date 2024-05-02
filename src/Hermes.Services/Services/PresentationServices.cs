namespace Hermes.Services;

public class PresentationServices(string databaseConnectionString) : ServicesBase(databaseConnectionString)
{

	//public async Task<Presentation> CreatePresentationAsync(PresentationRequest presentationRequest)
	//{
	//	HermesContext context = new(_databaseConnectionString);
	//	Presentation presentation = presentationRequest.ToPresentation();
	//	if (presentationRequest.Tags is not null && presentationRequest.Tags.Count > 0)
	//		presentation.PresentationTags = presentationRequest.Tags.ToPresentationTagList(context);
	//	await context.Presentations.AddAsync(presentation);
	//	await context.SaveChangesAsync();
	//	return presentation;
	//}


	/// <summary>
	/// Saves a template object of type `Presentation` as a JSON file at the specified `path`.
	/// </summary>
	/// <param name="path">The file path where the JSON file will be saved.</param>
	/// <returns>A message indicating the success of the operation, including the name of the template object and the path where the JSON file is saved.</returns>
	/// <exception cref="IOException">If an I/O error occurs while writing the JSON file.</exception>
	public async Task<string> GetTemplateAsync(string path)
	{

		List<PresentationType> presentationTypes = await GetWhereAsync<PresentationType>(x => x.IsEnabled == true);
		List<PresentationStatus> presentationStatuses = await GetWhereAsync<PresentationStatus>(x => x.IsEnabled == true);

		PresentationRequest template = new()
		{
			Type = $"Required -  The type of presentation [{string.Join(", ", presentationTypes)}]",
			Status = $"Required - The status of the presentation [{string.Join(", ", presentationStatuses)}]",
			PublicRepoLink = "Optional - The public repository link of the presentation",
			PrivateRepoLink = "Optional - The private repository link of the presentation",
			Permalink = "Required - The permalink of the presentation; used as the identifier",
			DefaultLanguageCode = "Optional - The default language code of the presentation; default is 'en'",
			Texts =
			[
				new() {
					LanguageCode = "Required - The default language code of the presentation",
					Title = "Required - The title of the presentation",
					ShortTitle = "Optional - The short title of the presentation",
					Abstract = "Optional - The abstract of the presentation",
					ShortAbstract = "Optional - The short abstract of the presentation",
					ElevatorPitch = "Optional - The elevator pitch of the presentation",
					AdditionalDetails = "Optional - Additional details of the presentation",
					LearningObjectives =
					[
						new() {
							Text = "Required - The text of the learning objective",
						}
					]
				}
			],
			Tags = ["Optional - one or more tags associated with the presentation."]
		};

		return SaveTemplateToFile(path, template);
	}

}