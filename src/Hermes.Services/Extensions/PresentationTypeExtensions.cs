namespace Hermes.Extensions;

internal static class PresentationTypeExtensions
{

	internal static PresentationType ToPresentationType(this string type, HermesContext context)
	{
		PresentationType? typeRecord = context.PresentationTypes.FirstOrDefault(t => t.PresentationTypeName == type);
		return typeRecord is null
			? throw new ArgumentException($"The presentation type '{type}' does not exist in the database.")
			: typeRecord;
	}

}