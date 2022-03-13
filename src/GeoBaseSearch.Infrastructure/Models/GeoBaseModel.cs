namespace GeoBaseSearch.Infrastructure.Models;

public sealed class GeoBaseModel
{
	public HeaderModel? HeaderModel { get; init; }
	public IpAddressIntervalModel[] IpAddressIntervalsSortedByIpRanges { get; init; } = Array.Empty<IpAddressIntervalModel>();
	public IpAddressIntervalModel[] IpAddressIntervalsSortedByCityName { get; set; } = Array.Empty<IpAddressIntervalModel>();
}