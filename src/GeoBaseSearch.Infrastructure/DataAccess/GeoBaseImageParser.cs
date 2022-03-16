using System.Runtime.CompilerServices;
using System.Text;
using GeoBaseSearch.Infrastructure.DataAccess.Abstract;
using GeoBaseSearch.Infrastructure.Models;

namespace GeoBaseSearch.Infrastructure.DataAccess;

public sealed class GeoBaseImageParser : IGeoBaseImageParser
{
	private const int SIZE_OF_INT32 = 4;
	private const int SIZE_OF_UINT32 = 4;
	private const int SIZE_OF_UINT64 = 8;
	private const int SIZE_OF_FLOAT = 4;
	private const int SIZE_OF_LOCATION_MODEL = 96;

	public GeoBaseModel Parse(byte[] geoBaseImage)
	{
		if (geoBaseImage == null)
			throw new ArgumentNullException(nameof(geoBaseImage));

		var headerModel = GetHeader(geoBaseImage);
		var ipAddressIntervals = GetIpAddressIntervals(geoBaseImage, headerModel);

		var result = new GeoBaseModel
		{
			HeaderModel = headerModel,
			IpAddressIntervalsSortedByIpRanges = ipAddressIntervals,
			IpAddressIntervalsSortedByCityName = GetIpAddressIntervalSortedByCityName(geoBaseImage, headerModel, ipAddressIntervals)
		};

		return result;
	}

	private static HeaderModel GetHeader(byte[] geoBaseImage)
	{
		var shift = 0;

		var version = BitConverter.ToInt32(geoBaseImage, shift);
		shift += SIZE_OF_INT32;

		var name = Encoding.UTF8.GetString(geoBaseImage, shift, GetNullStringIndex(geoBaseImage, shift, 32));
		shift += 32;

		var timestamp = BitConverter.ToUInt64(geoBaseImage, shift);
		shift += SIZE_OF_UINT64;

		var records = BitConverter.ToInt32(geoBaseImage, shift);
		shift += SIZE_OF_INT32;

		var offsetRanges = BitConverter.ToUInt32(geoBaseImage, shift);
		shift += SIZE_OF_UINT32;

		var offsetCities = BitConverter.ToUInt32(geoBaseImage, shift);
		shift += SIZE_OF_UINT32;

		var offsetLocations = BitConverter.ToUInt32(geoBaseImage, shift);

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

	private static IpAddressIntervalModel[] GetIpAddressIntervals(byte[] geoBaseImage, HeaderModel headerModel)
	{
		var result = new IpAddressIntervalModel[headerModel.Records];
		var shift = (int)headerModel.OffsetRanges;

		for (var i = 0; i < result.Length; i++)
		{
			var ipFrom = BitConverter.ToUInt32(geoBaseImage, shift);
			shift += SIZE_OF_UINT32;

			var ipTo = BitConverter.ToUInt32(geoBaseImage, shift);
			shift += SIZE_OF_UINT32;

			var locationIndex = BitConverter.ToUInt32(geoBaseImage, shift);
			shift += SIZE_OF_UINT32;

			result[i] = new IpAddressIntervalModel
			{
				Id = i + 1,
				IpFrom = ipFrom,
				IpTo = ipTo,
				Location = GetLocation(geoBaseImage, ref locationIndex, headerModel)
			};
		}

		return result;
	}

	private static LocationModel GetLocation(byte[] geoBaseImage, ref uint locationIndex, HeaderModel headerModel)
	{
		var shift = (int)locationIndex * SIZE_OF_LOCATION_MODEL + (int)headerModel.OffsetLocations;

		var result = new LocationModel
		{
			Id = (int)locationIndex + 1,
			Country = Encoding.UTF8.GetString(geoBaseImage, shift, GetNullStringIndex(geoBaseImage, shift, 8)),
			Region = Encoding.UTF8.GetString(geoBaseImage, shift + 8, GetNullStringIndex(geoBaseImage, shift + 8, 12)),
			Postal = Encoding.UTF8.GetString(geoBaseImage, shift + 8 + 12, GetNullStringIndex(geoBaseImage, shift + 8 + 12, 12)),
			City = Encoding.UTF8.GetString(geoBaseImage, shift + 8 + 12 + 12, GetNullStringIndex(geoBaseImage, shift + 8 + 12 + 12, 24)),
			Organization = Encoding.UTF8.GetString(geoBaseImage, shift + 8 + 12 + 12 + 24, GetNullStringIndex(geoBaseImage, shift + 8 + 12 + 12 + 24, 32)),
			Latitude = BitConverter.ToSingle(geoBaseImage, shift + 8 + 12 + 12 + 24 + 32),
			Longitude = BitConverter.ToSingle(geoBaseImage, shift + 8 + 12 + 12 + 24 + 32 + SIZE_OF_FLOAT)
		};

		return result;
	}

	private static IpAddressIntervalModel[] GetIpAddressIntervalSortedByCityName(byte[] geoBaseImage, HeaderModel headerModel, IpAddressIntervalModel[] ipAddressIntervalModels)
	{
		var result = new IpAddressIntervalModel[headerModel.Records];
		var shift = (int)headerModel.OffsetCities;

		for (var i = 0; i < result.Length; i++)
		{
			var index = BitConverter.ToInt32(geoBaseImage, shift);
			shift += SIZE_OF_INT32;

			var recordsRelativeIndex = index / SIZE_OF_LOCATION_MODEL;
			result[i] = ipAddressIntervalModels[recordsRelativeIndex];
		}

		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int GetNullStringIndex(byte[] geoBaseImage, int shift, int length)
	{
		for (var i = shift; i < shift + length; i++)
		{
			if (geoBaseImage[i] == 0)
				return i - shift;
		}

		return length - 1;
	}
}