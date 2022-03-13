using GeoBaseSearch.Domain.Locations;
using GeoBaseSearch.Infrastructure.DataAccess.Abstract;
using GeoBaseSearch.Infrastructure.Models;

namespace GeoBaseSearch.Infrastructure.Locations;

public sealed class LocationInMemoryRepository : ILocationRepository
{
	private readonly IInMemoryDatabase _inMemoryDatabase;

	public LocationInMemoryRepository(IInMemoryDatabase inMemoryDatabase)
	{
		_inMemoryDatabase = inMemoryDatabase ?? throw new ArgumentNullException(nameof(inMemoryDatabase));
	}

	public Task<Location?> GetLocationByIpAddress(uint ipAddress)
	{
		var ipAddressIntervals = _inMemoryDatabase.GeoBase?.IpAddressIntervals;

		if (ipAddressIntervals is null)
			return Task.FromResult((Location?)null);

		var ipAddressInterval = FindIpAddressIntervalByIpAddress(ipAddressIntervals, ipAddress);

		if (ipAddressInterval is null)
			return Task.FromResult((Location?)null);

		if (ipAddress < ipAddressInterval.IpFrom || ipAddress > ipAddressInterval.IpTo)
			return Task.FromResult((Location?)null);

		var result = ipAddressInterval.Location?.ToLocation();
		return Task.FromResult(result);
	}

	private static IpAddressIntervalModel? FindIpAddressIntervalByIpAddress(IpAddressIntervalModel[] ipAddressIntervals, uint ipAddress)
	{
		if (ipAddressIntervals.Length == 0)
			return null;

		var start = 0;
		var end = ipAddressIntervals.Length - 1;

		while (start < end)
		{
			var mid = (start + end) / 2;

			if (ipAddress < ipAddressIntervals[mid].IpFrom)
				end = mid;
			else if (ipAddress > ipAddressIntervals[mid].IpTo)
				start = mid + 1;
			else
			{
				start = mid;
				break;
			}
		}

		var result = ipAddressIntervals[start];
		return result;
	}
}