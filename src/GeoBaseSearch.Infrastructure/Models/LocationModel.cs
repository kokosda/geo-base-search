namespace GeoBaseSearch.Infrastructure.Models;

public sealed class LocationModel
{
	public int Id { get; init; }
	public string Country { get; init; } = string.Empty;
	public string Region { get; init; } = string.Empty;
	public string Postal { get; init; } = string.Empty;
	public string City { get; init; } = string.Empty;
	public string Organization { get; init; } = string.Empty;
	public float Longitude { get; init; }
	public float Latitude { get; init; }
}