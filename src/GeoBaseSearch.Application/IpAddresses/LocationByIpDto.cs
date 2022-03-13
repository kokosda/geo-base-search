using System.Net;
using GeoBaseSearch.Domain.Locations;

namespace GeoBaseSearch.Application.IpAddresses;

public sealed class LocationByIpDto
{
	public string Country { get; init; } = string.Empty;
	public string Region { get; init; } = string.Empty;
	public string Postal { get; init; } = string.Empty;
	public string City { get; init; } = string.Empty;
	public string Organization { get; init; } = string.Empty;
	public float Longitude { get; init; }
	public float Latitude { get; init; }
	public HttpStatusCode HttpStatusCode { get; init; }

	public static LocationByIpDto FromLocation(Location location, HttpStatusCode httpStatusCode)
	{
		var result = new LocationByIpDto
		{
			Country = location.Country,
			Region = location.Region,
			Postal = location.Postal,
			City = location.City,
			Organization = location.Organization,
			Longitude = location.Longitude,
			Latitude = location.Latitude,
			HttpStatusCode = httpStatusCode
		};
		return result;
	}
}