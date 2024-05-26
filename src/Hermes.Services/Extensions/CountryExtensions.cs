using Hermes.Responses;

namespace Hermes.Extensions;

internal static class CountryExtensions
{
	internal static CountryResponse ToResponse(this Country country)
		=> new()
		{
			Code = country.CountryCode,
			Name = country.CountryName,
			WorldRegionCode = country.WorldRegionCodeNavigation.WorldRegionCode,
			WorldRegionName = country.WorldRegionCodeNavigation.WorldRegionName,
			M49Code = country.M49code,
			HasDivisions = country.HasDivisions,
			DivisionName = country.DivisionName
		};
}