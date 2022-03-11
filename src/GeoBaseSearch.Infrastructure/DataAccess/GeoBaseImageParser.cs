using GeoBaseSearch.Infrastructure.DataAccess.Abstract;
using GeoBaseSearch.Infrastructure.Models;

namespace GeoBaseSearch.Infrastructure.DataAccess;

public sealed class GeoBaseImageParser : IGeoBaseImageParser
{
	private const int SIZE_OF_INT32 = 4;
	private const int SIZE_OF_UINT32 = 4;
	private const int SIZE_OF_UINT64 = 8;

	public GeoBaseModel Parse(byte[] geoBaseImage)
	{
		if (geoBaseImage == null)
			throw new ArgumentNullException(nameof(geoBaseImage));

		var result = new GeoBaseModel
		{
			HeaderModel = GetHeader(geoBaseImage)
		};
		return result;
	}

	private static HeaderModel GetHeader(byte[] geoBaseImage)
	{
		var shift = 0;
		var version = BitConverter.ToInt32(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_INT32));

		shift += SIZE_OF_INT32;
		var name = new ReadOnlySpan<byte>(geoBaseImage, shift, 32).ToArray().Select(i => (sbyte)i).ToArray();

		shift += 32;
		var timestamp = BitConverter.ToUInt64(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_UINT64));

		shift += SIZE_OF_UINT64;
		var records = BitConverter.ToInt32(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_INT32));

		shift += SIZE_OF_INT32;
		var offsetRanges = BitConverter.ToUInt32(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_UINT32));

		shift += SIZE_OF_UINT32;
		var offsetCities = BitConverter.ToUInt32(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_UINT32));

		shift += SIZE_OF_UINT32;
		var offsetLocations = BitConverter.ToUInt32(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_UINT32));

		var result = new HeaderModel
		{
			Version = version,
			Name = name,
			Timestamp = timestamp,
			Records = records,
			OffsetRanges = offsetRanges,
			OffsetCities = offsetCities,
			OffsetLocations = offsetLocations
		};

		return result;
	}
}