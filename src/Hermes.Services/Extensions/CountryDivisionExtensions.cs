using Hermes.Responses;

namespace Hermes.Extensions;

internal static class CountryDivisionExtensions
{
	internal static CountryDivisionResponse ToResponse(this CountryDivision countryDivision)
		=> new()
		{
			Code = countryDivision.CountryDivisionCode,
			CountryCode = countryDivision.CountryCode,
			Name = countryDivision.CountryDivisionName,
			CategoryName = countryDivision.CategoryName
		};
}