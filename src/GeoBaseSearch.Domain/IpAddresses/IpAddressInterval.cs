using GeoBaseSearch.Core.Domain;
using GeoBaseSearch.Domain.Locations;

namespace GeoBaseSearch.Domain.IpAddresses;

public sealed class IpAddressInterval : EntityBase<int>
{
	public uint IpFrom { get; init; }
	public uint IpTo { get; init; }
	public Location? Location { get; init; }
}