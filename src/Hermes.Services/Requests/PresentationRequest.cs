#nullable disable

namespace Hermes.Requests;

public class PresentationRequest
{
	public string Type { get; set; }
	public string Status { get; set; }
	public string PublicRepoLink { get; set; }
	public string PrivateRepoLink { get; set; }
	public string Permalink { get; set; }
	public bool IsArchived { get; set; } = false;
	public string DefaultLanguageCode { get; set; } = "en";
	public List<PresentationTextRequest> Texts { get; set; } = [];
	public List<string> Tags { get; set; } = [];
}