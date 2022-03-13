using System.Net;
using System.Text.Json.Serialization;

namespace GeoBaseSearch.Application.Cities;

public sealed class LocationsByCityDto
{
	public LocationByCityDto[] Locations { get; init; } = Array.Empty<LocationByCityDto>();

	[JsonIgnore]
	public HttpStatusCode HttpStatusCode { get; set; }
}