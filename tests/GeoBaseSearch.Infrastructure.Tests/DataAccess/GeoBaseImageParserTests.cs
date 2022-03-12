using System;
using System.IO;
using System.Linq;
using GeoBaseSearch.Infrastructure.DataAccess;
using NUnit.Framework;

namespace GeoBaseSearch.Infrastructure.Tests.DataAccess;

[TestFixture]
public sealed class GeoBaseImageParserTests
{
	private GeoBaseImageParser _geoBaseImageParser = new GeoBaseImageParser();

	[TestCase(TestName = "When correct GeoBase image provided, then return GeoBaseModel.")]
	public void Parse_WhenCorrectGeoBaseImageProvided_ReturnsGeoBaseModel()
	{
		// Arrange
		var geoBaseImage = File.ReadAllBytes("Resources/geobase.dat");

		// Act
		var result = _geoBaseImageParser.Parse(geoBaseImage);

		// Assert
		Assert.IsNotNull(result);
		Assert.IsNotNull(result.HeaderModel);
		Assert.AreEqual(1, result.HeaderModel?.Version);
		Assert.AreEqual(1487167858, result.HeaderModel?.Timestamp);
		Assert.AreEqual(100000, result.HeaderModel?.Records);
		Assert.AreEqual(60, result.HeaderModel?.OffsetRanges);
		Assert.AreEqual(10800060, result.HeaderModel?.OffsetCities);
		Assert.AreEqual(1200060, result.HeaderModel?.OffsetLocations);
		Assert.IsNotNull(result.IpAddressIntervals);
		Assert.AreEqual(100000, result.IpAddressIntervals.Length);

		foreach (var ipAddressInterval in result.IpAddressIntervals)
		{
			Assert.IsNotNull(ipAddressInterval);
			Assert.IsNotNull(ipAddressInterval.Location);
			Assert.That(ipAddressInterval.Location?.Country.StartsWith("cou_"), Is.True);
			Assert.That(ipAddressInterval.Location?.Region.StartsWith("reg_"), Is.True);
			Assert.That(ipAddressInterval.Location?.Postal.StartsWith("pos_"), Is.True);
			Assert.That(ipAddressInterval.Location?.City.StartsWith("cit_"), Is.True);
			Assert.That(ipAddressInterval.Location?.Organization.StartsWith("org_"), Is.True);
		}

		Assert.IsNotNull(result.IpAddressIntervalsSortedByCityName);
		Assert.AreEqual(result.IpAddressIntervals.Length, result.IpAddressIntervalsSortedByCityName.Length);

		var prevIpAddressInterval = result.IpAddressIntervalsSortedByCityName[0];
		Assert.IsNotNull(prevIpAddressInterval);

		foreach (var ipAddressInterval in result.IpAddressIntervalsSortedByCityName.Skip(1))
		{
			Assert.IsNotNull(ipAddressInterval);
			Assert.That(string.Compare(ipAddressInterval.Location?.City, prevIpAddressInterval.Location?.City, StringComparison.InvariantCulture), Is.GreaterThanOrEqualTo(0));

			prevIpAddressInterval = ipAddressInterval;
		}
	}
}