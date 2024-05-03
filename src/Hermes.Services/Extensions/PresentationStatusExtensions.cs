namespace Hermes.Extensions;

internal static class PresentationStatusExtensions
{
	internal static PresentationStatus ToPresentationStatus(this string status, HermesContext context)
	{
		PresentationStatus? statusRecord = context.PresentationStatuses.FirstOrDefault(s => s.PresentationStatusName == status);
		return statusRecord is null
			? throw new ArgumentException($"The presentation status '{status}' does not exist in the database.")
			: statusRecord;
	}
}