#nullable disable

namespace Hermes.Responses;

public class PresentationResponse
{
	public string Type { get; set; }
	public string Status { get; set; }
	public string PublicRepoLink { get; set; }
	public string PrivateRepoLink { get; set; }
	public string Permalink { get; set; }
	public bool IsArchived { get; set; } = false;
	public bool IncludeInPublicProfile { get; internal set; }
	public string DefaultLanguageCode { get; set; } = "en";
	public List<PresentationTextResponse> Texts { get; set; } = [];
	public List<string> Tags { get; set; } = [];
}