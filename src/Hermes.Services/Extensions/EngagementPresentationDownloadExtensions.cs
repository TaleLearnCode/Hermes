namespace Hermes.Extensions;

internal static class EngagementPresentationDownloadExtensions
{

	internal static EngagementPresentationDownloadResponse ToResponse(this EngagementPresentationDownload engagementPresentationDownload)
		=> new()
		{
			Type = engagementPresentationDownload.DownloadType.DownloadTypeName,
			Name = engagementPresentationDownload.DownloadName,
			Path = engagementPresentationDownload.DownloadPath
		};

	internal static List<EngagementPresentationDownloadResponse> ToResponse(this IEnumerable<EngagementPresentationDownload> engagementPresentationDownloads)
		=> engagementPresentationDownloads.Select(ToResponse).ToList();

	internal static string ToMarkdown(this IEnumerable<EngagementPresentationDownload> engagementPresentationDownloads)
	{
		StringBuilder response = new();
		response.AppendLine($"#### {MarkdownEngagementPresentationHeadings.Downloads}");
		response.AppendLine();
		response.AppendLine("| Name | Type | Path |");
		response.AppendLine("|---|---|---|");
		foreach (EngagementPresentationDownload download in engagementPresentationDownloads)
			response.AppendLine($"| {download.DownloadName} | {download.DownloadType.DownloadTypeName} | {download.DownloadPath} |");
		response.AppendLine();
		return response.ToString();
	}

}