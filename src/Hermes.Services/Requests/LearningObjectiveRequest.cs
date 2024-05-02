namespace Hermes.Requests;

public class LearningObjectiveRequest
{
	public int Id { get; set; }
	public required string Text { get; set; }
	public required int SortOrder { get; set; }
}