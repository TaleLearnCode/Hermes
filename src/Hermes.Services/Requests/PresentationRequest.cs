namespace Hermes.Requests;

public class PresentationRequest
{
	public int Id { get; set; }
	public int TypeId { get; set; }
	public int StatusId { get; set; }
	public string? PublicRepoLink { get; set; }
	public string? PrivateRepoLink { get; set; }
	public required string Permalink { get; set; }
	public bool IsArchived { get; set; } = false;
	public string DefaultLanguageCode { get; set; } = "en";
	public List<PresentationTextRequest> Texts { get; set; } = [];
	public List<string>? Tags { get; set; }
}