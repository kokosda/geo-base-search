using System.IO;
using GeoBaseSearch.Infrastructure.DataAccess;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
		Assert.AreEqual(1, result.HeaderModel.Version);
		Assert.AreEqual(1487167858, result.HeaderModel.Timestamp);
		Assert.AreEqual(100000, result.HeaderModel.Records);
		Assert.AreEqual(1200060, result.HeaderModel.OffsetLocations);
	}
}