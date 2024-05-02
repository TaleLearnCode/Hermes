#nullable disable
namespace Hermes.Requests;

public class PresentationTextRequest
{
	public string LanguageCode { get; set; } = "en";
	public string Title { get; set; }
	public string ShortTitle { get; set; }
	public string Abstract { get; set; }
	public string ShortAbstract { get; set; }
	public string ElevatorPitch { get; set; }
	public string AdditionalDetails { get; set; }
	public List<LearningObjectiveRequest> LearningObjectives { get; set; }
}