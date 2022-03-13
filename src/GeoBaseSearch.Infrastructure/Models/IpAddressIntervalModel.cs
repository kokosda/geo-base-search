namespace GeoBaseSearch.Infrastructure.Models;

public sealed class IpAddressIntervalModel
{
	public int Id { get; init; }
	public uint IpFrom { get; init; }
	public uint IpTo { get; init; }
	public LocationModel? Location { get; init; }
}