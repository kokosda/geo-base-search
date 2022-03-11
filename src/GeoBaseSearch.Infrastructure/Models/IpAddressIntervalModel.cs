namespace GeoBaseSearch.Infrastructure.Models;

public sealed class IpAddressIntervalModel
{
	public uint IpFrom { get; init; }
	public uint IpTo { get; init; }
	public LocationModel? Location { get; init; }
}