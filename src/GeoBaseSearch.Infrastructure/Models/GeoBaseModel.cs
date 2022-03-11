namespace GeoBaseSearch.Infrastructure.Models;

public sealed class GeoBaseModel
{
	public HeaderModel? HeaderModel { get; init; }
	public IpAddressIntervalModel[] IpAddressIntervals { get; init; } = Array.Empty<IpAddressIntervalModel>();
}