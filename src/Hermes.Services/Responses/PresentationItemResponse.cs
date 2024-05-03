#nullable disable

namespace Hermes.Responses;

public class PresentationItemResponse
{
	public string Permalink { get; set; }
	public string Title { get; set; }
	public string Type { get; set; }
	public string Status { get; set; }
	public string PublicRepoLink { get; set; }
	public string PrivateRepoLink { get; set; }
	public bool IncludeInPublicProfile { get; set; }
	public string DefaultLanguageCode { get; set; }
}