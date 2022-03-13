using System;
using System.Linq;
using GeoBaseSearch.Domain.Locations;
using GeoBaseSearch.Infrastructure.DataAccess.Abstract;
using GeoBaseSearch.Infrastructure.Locations;
using GeoBaseSearch.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace GeoBaseSearch.Infrastructure.Tests.Locations;

[TestFixture]
public sealed class LocationInMemoryRepositoryTests
{
	private LocationInMemoryRepository? _locationsInMemoryRepository;
	private Mock<IInMemoryDatabase>? _inMemoryDatabaseMock;

	[SetUp]
	public void SetUp()
	{
		_inMemoryDatabaseMock = new Mock<IInMemoryDatabase>();
		_locationsInMemoryRepository = new LocationInMemoryRepository(_inMemoryDatabaseMock.Object);
	}

	[TestCase(65U, 60, TestName = "When IP address presented in the list, then return location found. 65")]
	[TestCase(10U, 10, TestName = "When IP address presented in the list, then return location found. 10")]
	[TestCase(900000U, 900000, TestName = "When IP address presented in the list, then return location found. 900000")]
	[TestCase(900010U, 900000, TestName = "When IP address presented in the list, then return location found. 900010")]
	[TestCase(2010U, 2000, TestName = "When IP address presented in the list, then return location found. 2010")]
	public void GetLocationByIpAddress_WhenIpAddressPresentedInTheList_ReturnsLocationFound(uint ipAddress, int expectedLocationId)
	{
		// Arrange
		_inMemoryDatabaseMock?.Setup(id => id.GeoBase).Returns(new GeoBaseModel
		{
			IpAddressIntervalsSortedByIpRanges = GetIpAddresses().Select(ia => new IpAddressIntervalModel
			{
				IpFrom = (uint)ia,
				IpTo = (uint)(ia + 10),
				Location = new LocationModel { Id = ia }
			}).ToArray()
		});

		// Act
		var result = _locationsInMemoryRepository?.GetLocationByIpAddress(ipAddress).Result;

		// Assert
		Assert.IsNotNull(result);
		Assert.AreEqual(expectedLocationId, result?.Id);
	}

	[TestCase(3U, TestName = "When IP address is not presented in the list, then return null. 3")]
	[TestCase(2011U, TestName = "When IP address is not presented in the list, then return null. 2011")]
	[TestCase(900011U, TestName = "When IP address is not presented in the list, then return null. 900011")]
	[TestCase(899999U, TestName = "When IP address is not presented in the list, then return null. 899999")]
	public void GetLocationByIpAddress_WhenIpAddressIsNotPresentedInTheList_ReturnsNull(uint ipAddress)
	{
		// Arrange
		_inMemoryDatabaseMock?.Setup(id => id.GeoBase).Returns(new GeoBaseModel
		{
			IpAddressIntervalsSortedByIpRanges = GetIpAddresses().Select(ia => new IpAddressIntervalModel
			{
				IpFrom = (uint)ia,
				IpTo = (uint)(ia + 10),
				Location = new LocationModel { Id = ia }
			}).ToArray()
		});

		// Act
		var result = _locationsInMemoryRepository?.GetLocationByIpAddress(ipAddress).Result;

		// Assert
		Assert.IsNull(result);
	}

	[TestCase("Vancouver", 5, TestName = "When city is presented in the list, then return collection of locations. Vancouver")]
	[TestCase("Antwerp", 1, TestName = "When city is presented in the list, then return collection of locations. Antwerp")]
	[TestCase("Vats-lav", 3, TestName = "When city is presented in the list, then return collection of locations. Vats-lav")]
	public void GetLocationsByCity_WhenCityIsPresentedInTheList_ReturnsCollectionOfLocations(string city, int expectedCollectionLength)
	{
		// Arrange
		var cities = new[]
		{
			"Antwerp",
			"Vans",
			"Vans",
			"Vancouver",
			"Vancouver",
			"Vancouver",
			"Vancouver",
			"Vancouver",
			"Vats-lav",
			"Vats-lav",
			"Vats-lav"
		};

		_inMemoryDatabaseMock?.Setup(imd => imd.GeoBase).Returns(new GeoBaseModel
		{
			IpAddressIntervalsSortedByCityName = cities.Select(c => new IpAddressIntervalModel
			{
				Location = new LocationModel { City = c }
			}).ToArray()
		});

		// Act
		var result = _locationsInMemoryRepository?.GetLocationsByCity(city).Result;

		// Assert
		Assert.IsNotNull(result);
		Assert.That(result?.Length, Is.EqualTo(expectedCollectionLength));

		foreach (var location in result ?? Array.Empty<Location>())
		{
			Assert.IsNotNull(location);
			Assert.AreEqual(city, location?.City);
		}
	}

	[TestCase("Vats", TestName = "When city is not presented in the list, then return an empty collection of locations. Vats")]
	[TestCase("Ant", TestName = "When city is not presented in the list, then return an empty collection of locations. Ant")]
	[TestCase("Van", TestName = "When city is not presented in the list, then return an empty collection of locations. Van")]
	public void GetLocationsByCity_WhenCityIsNotPresentedInTheList_ReturnsEmptyCollectionOfLocations(string city)
	{
		// Arrange
		var cities = new[]
		{
			"Antwerp",
			"Vans",
			"Vans",
			"Vancouver",
			"Vancouver",
			"Vancouver",
			"Vancouver",
			"Vancouver",
			"Vats-lav",
			"Vats-lav",
			"Vats-lav"
		};

		_inMemoryDatabaseMock?.Setup(imd => imd.GeoBase).Returns(new GeoBaseModel
		{
			IpAddressIntervalsSortedByCityName = cities.Select(c => new IpAddressIntervalModel
			{
				Location = new LocationModel { City = c }
			}).ToArray()
		});

		// Act
		var result = _locationsInMemoryRepository?.GetLocationsByCity(city).Result;

		// Assert
		Assert.IsNotNull(result);
		Assert.That(result?.Length, Is.EqualTo(0));
	}

	private int[] GetIpAddresses()
	{
		var result = new[]
		{
			10,
			50,
			60,
			80,
			2000,
			900000
		};
		return result;
	}
}