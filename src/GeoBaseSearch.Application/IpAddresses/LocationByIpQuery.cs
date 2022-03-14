using System.ComponentModel.DataAnnotations;

namespace GeoBaseSearch.Application.IpAddresses;

public sealed class LocationByIpQuery
{
	[Required]
	public string Ip { get; init; } = string.Empty;
}