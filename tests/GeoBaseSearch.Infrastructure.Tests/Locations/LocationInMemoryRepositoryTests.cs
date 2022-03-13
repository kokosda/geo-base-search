using System.Linq;
using GeoBaseSearch.Infrastructure.DataAccess.Abstract;
using GeoBaseSearch.Infrastructure.Locations;
using GeoBaseSearch.Infrastructure.Models;
using Moq;
using NUnit.Framework;

namespace GeoBaseSearch.Infrastructure.Tests.Locations;

[TestFixture]
public sealed class LocationInMemoryRepositoryTests
{
	private LocationInMemoryRepository _locationsInMemoryRepository;
	private Mock<IInMemoryDatabase> _inMemoryDatabaseMock;

	[SetUp]
	public void SetUp()
	{
		_inMemoryDatabaseMock = new Mock<IInMemoryDatabase>();
		_locationsInMemoryRepository = new LocationInMemoryRepository(_inMemoryDatabaseMock.Object);
	}

	[TestCase(65, 60, TestName = "When IP address presented in the list, then return location found. 65")]
	[TestCase(10, 10, TestName = "When IP address presented in the list, then return location found. 10")]
	[TestCase(900000, 900000, TestName = "When IP address presented in the list, then return location found. 900000")]
	[TestCase(900010, 900000, TestName = "When IP address presented in the list, then return location found. 900010")]
	[TestCase(2010, 2000, TestName = "When IP address presented in the list, then return location found. 2010")]
	public void GetLocationByIpAddress_WhenIpAddressPresentedInTheList_ReturnsLocationFound(int ipAddress, int expectedLocationId)
	{
		// Arrange
		_inMemoryDatabaseMock.Setup(id => id.GeoBase).Returns(new GeoBaseModel
		{
			IpAddressIntervals = GetIpAddresses().Select(ia => new IpAddressIntervalModel
			{
				IpFrom = (uint)ia,
				IpTo = (uint)(ia + 10),
				Location = new LocationModel { Id = ia }
			}).ToArray()
		});

		// Act
		var result = _locationsInMemoryRepository.GetLocationByIpAddress(ipAddress).Result;

		// Assert
		Assert.IsNotNull(result);
		Assert.AreEqual(expectedLocationId, result?.Id);
	}

	[TestCase(3, TestName = "When IP address is not presented in the list, then return null. 3")]
	[TestCase(2011, TestName = "When IP address is not presented in the list, then return null. 2011")]
	[TestCase(900011, TestName = "When IP address is not presented in the list, then return null. 900011")]
	[TestCase(899999, TestName = "When IP address is not presented in the list, then return null. 899999")]
	public void GetLocationByIpAddress_WhenIpAddressIsNotPresentedInTheList_ReturnsNull(int ipAddress)
	{
		// Arrange
		_inMemoryDatabaseMock.Setup(id => id.GeoBase).Returns(new GeoBaseModel
		{
			IpAddressIntervals = GetIpAddresses().Select(ia => new IpAddressIntervalModel
			{
				IpFrom = (uint)ia,
				IpTo = (uint)(ia + 10),
				Location = new LocationModel { Id = ia }
			}).ToArray()
		});

		// Act
		var result = _locationsInMemoryRepository.GetLocationByIpAddress(ipAddress).Result;

		// Assert
		Assert.IsNull(result);
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