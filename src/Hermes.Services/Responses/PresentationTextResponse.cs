#nullable disable
namespace Hermes.Responses;

public class PresentationTextResponse
{
	public string LanguageCode { get; set; } = "en";
	public string Title { get; set; }
	public string ShortTitle { get; set; }
	public string Abstract { get; set; }
	public string ShortAbstract { get; set; }
	public string ElevatorPitch { get; set; }
	public string AdditionalDetails { get; set; }
	public List<LearningObjectiveResponse> LearningObjectives { get; set; }
}