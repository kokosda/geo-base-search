using GeoBaseSearch.Domain.IpAddresses;
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
		var ipAddressIntervals = _inMemoryDatabase.GeoBase?.IpAddressIntervalsSortedByIpRanges;

		if (ipAddressIntervals is null)
			return Task.FromResult((Location?)null);

		var ipAddressIntervalFound = FindIpAddressIntervalByIpAddress(ipAddressIntervals, ipAddress);

		if (ipAddressIntervalFound is null)
			return Task.FromResult((Location?)null);

		if (ipAddress < ipAddressIntervalFound.IpFrom || ipAddress > ipAddressIntervalFound.IpTo)
			return Task.FromResult((Location?)null);

		var result = ipAddressIntervalFound.Location?.ToLocation();
		return Task.FromResult(result);
	}

	public Task<Location?[]> GetLocationsByCity(string city)
	{
		if (string.IsNullOrWhiteSpace(city))
			throw new ArgumentNullException(nameof(city));

		var ipAddressIntervals = _inMemoryDatabase.GeoBase?.IpAddressIntervalsSortedByCityName;

		if (ipAddressIntervals is null)
			return Task.FromResult(Array.Empty<Location?>());

		var ipAddressIntervalPosition = FindIpAddressIntervalPositionByCityName(city, ipAddressIntervals);

		if (ipAddressIntervalPosition == int.MinValue)
			return Task.FromResult(Array.Empty<Location?>());

		var ipAddressIntervalFound = ipAddressIntervals[ipAddressIntervalPosition];

		if (ipAddressIntervalFound.Location?.City.Equals(city, StringComparison.InvariantCulture) == false)
			return Task.FromResult(Array.Empty<Location?>());

		var matchingPositionsRange = GetCityMatchingPositionsRange(ipAddressIntervalPosition, ipAddressIntervals, city);
		var firstPosition = matchingPositionsRange.Start.Value;
		var lastPosition = matchingPositionsRange.End.Value;
		var takeCount = lastPosition - firstPosition + 1;
		var result = ipAddressIntervals.Skip(firstPosition).Take(takeCount).Select(iai => iai.Location?.ToLocation()).ToArray();
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

	private static int FindIpAddressIntervalPositionByCityName(string cityName, IpAddressIntervalModel[] ipAddressIntervals)
	{
		if (ipAddressIntervals.Length == 0)
			return int.MinValue;

		var start = 0;
		var end = ipAddressIntervals.Length - 1;

		while (start < end)
		{
			var mid = (start + end) / 2;
			var stringComparisonResult = string.Compare(cityName, ipAddressIntervals[mid].Location?.City, StringComparison.InvariantCulture);

			if (stringComparisonResult < 0)
				end = mid;
			else if (stringComparisonResult > 0)
				start = mid + 1;
			else
			{
				start = mid;
				break;
			}
		}

		var result = start;
		return result;
	}

	private Range GetCityMatchingPositionsRange(int ipAddressIntervalPosition, IpAddressIntervalModel[] ipAddressIntervals, string city)
	{
		var firstMatchingPosition = ipAddressIntervalPosition;

		for (var i = ipAddressIntervalPosition - 1; i >= 0; i--)
		{
			if (ipAddressIntervals[i].Location?.City.Equals(city, StringComparison.InvariantCulture) == false)
				break;

			firstMatchingPosition = i;
		}

		var lastMatchingPosition = ipAddressIntervalPosition;

		for (var i = ipAddressIntervalPosition + 1; i < ipAddressIntervals.Length; i++)
		{
			if (ipAddressIntervals[i].Location?.City.Equals(city, StringComparison.InvariantCulture) == false)
				break;

			lastMatchingPosition = i;
		}

		var result = new Range(firstMatchingPosition, lastMatchingPosition);
		return result;
	}
}