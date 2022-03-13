using System.ComponentModel.DataAnnotations;

namespace GeoBaseSearch.Application.Cities;

public sealed class LocationsByCityQuery
{
	[Required]
	public string City { get; init; } = string.Empty;
}