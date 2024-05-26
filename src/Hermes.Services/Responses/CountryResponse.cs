namespace Hermes.Responses;

public class CountryResponse
{
	public required string Code { get; set; }
	public required string Name { get; set; }
	public required string WorldRegionCode { get; set; }
	public required string WorldRegionName { get; set; }
	public required string M49Code { get; set; }
	public bool HasDivisions { get; set; }
	public string? DivisionName { get; set; }
}