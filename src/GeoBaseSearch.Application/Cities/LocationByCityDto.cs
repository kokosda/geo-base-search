using GeoBaseSearch.Application.IpAddresses;
using GeoBaseSearch.Domain.Locations;

namespace GeoBaseSearch.Application.Cities;

public class LocationByCityDto
{
	public string Country { get; init; } = string.Empty;
	public string Region { get; init; } = string.Empty;
	public string Postal { get; init; } = string.Empty;
	public string City { get; init; } = string.Empty;
	public string Organization { get; init; } = string.Empty;
	public float Longitude { get; init; }
	public float Latitude { get; init; }

	public static LocationByCityDto FromLocation(Location? location)
	{
		if (location is null)
			throw new ArgumentNullException(nameof(location));

		var result = new LocationByCityDto
		{
			Country = location.Country,
			Region = location.Region,
			Postal = location.Postal,
			City = location.City,
			Organization = location.Organization,
			Longitude = location.Longitude,
			Latitude = location.Latitude
		};
		return result;
	}
}