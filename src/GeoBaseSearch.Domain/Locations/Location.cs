using GeoBaseSearch.Core.Domain;

namespace GeoBaseSearch.Domain.Locations;

public sealed class Location : EntityBase<int>
{
	public string Country { get; init; } = string.Empty;
	public string Region { get; init; } = string.Empty;
	public string Postal { get; init; } = string.Empty;
	public string City { get; init; } = string.Empty;
	public string Organization { get; init; } = string.Empty;
	public float Longitude { get; init; }
	public float Latitude { get; init; }
}