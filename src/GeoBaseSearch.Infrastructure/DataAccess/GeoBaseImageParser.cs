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

	public GeoBaseModel Parse(byte[] geoBaseImage)
	{
		if (geoBaseImage == null)
			throw new ArgumentNullException(nameof(geoBaseImage));

		var headerModel = GetHeader(geoBaseImage);
		var ipAddressIntervals = GetIpAddressIntervals(geoBaseImage, headerModel);

		var result = new GeoBaseModel
		{
			HeaderModel = headerModel,
			IpAddressIntervals = ipAddressIntervals,
			IpAddressIntervalsSortedByCityName = GetIpAddressIntervalSortedByCityName(geoBaseImage, headerModel, ipAddressIntervals)
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

	private static IpAddressIntervalModel[] GetIpAddressIntervals(byte[] geoBaseImage, HeaderModel headerModel)
	{
		var result = new IpAddressIntervalModel[headerModel.Records];
		var shift = (int)headerModel.OffsetRanges;

		for (var i = 0; i < result.Length; i++)
		{
			var ipFrom = BitConverter.ToUInt32(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_UINT32));
			shift += SIZE_OF_UINT32;

			var ipTo = BitConverter.ToUInt32(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_UINT32));
			shift += SIZE_OF_UINT32;

			var locationIndex = BitConverter.ToUInt32(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_UINT32));
			shift += SIZE_OF_UINT32;

			result[i] = new IpAddressIntervalModel
			{
				IpFrom = ipFrom,
				IpTo = ipTo,
				Location = GetLocation(geoBaseImage, locationIndex, headerModel)
			};
		}

		return result;
	}

	private static LocationModel GetLocation(byte[] geoBaseImage, uint locationIndex, HeaderModel headerModel)
	{
		var shift = (int)locationIndex * 96 + (int)headerModel.OffsetLocations;

		var country = Encoding.UTF8.GetString(new ReadOnlySpan<byte>(geoBaseImage, shift, 8));
		shift += 8;

		var region = Encoding.UTF8.GetString(new ReadOnlySpan<byte>(geoBaseImage, shift, 12));
		shift += 12;

		var postal = Encoding.UTF8.GetString(new ReadOnlySpan<byte>(geoBaseImage, shift, 12));
		shift += 12;

		var city = Encoding.UTF8.GetString(new ReadOnlySpan<byte>(geoBaseImage, shift, 24));
		shift += 24;

		var organization = Encoding.UTF8.GetString(new ReadOnlySpan<byte>(geoBaseImage, shift, 32));
		shift += 32;

		var latitude = BitConverter.ToSingle(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_FLOAT));
		shift += SIZE_OF_FLOAT;

		var longitude = BitConverter.ToSingle(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_FLOAT));

		var result = new LocationModel
		{
			Country = country,
			Region = region,
			Postal = postal,
			City = city,
			Organization = organization,
			Latitude = latitude,
			Longitude = longitude
		};

		return result;
	}

	private static IpAddressIntervalModel[] GetIpAddressIntervalSortedByCityName(byte[] geoBaseImage, HeaderModel headerModel, IpAddressIntervalModel[] ipAddressIntervalModels)
	{
		var result = new IpAddressIntervalModel[headerModel.Records];
		var shift = (int)headerModel.OffsetCities;

		for (var i = 0; i < result.Length; i++)
		{
			var index = BitConverter.ToInt32(new ReadOnlySpan<byte>(geoBaseImage, shift, SIZE_OF_INT32));
			shift += SIZE_OF_INT32;

			var recordsRelativeIndex = index / 96;
			result[i] = ipAddressIntervalModels[recordsRelativeIndex];
		}

		return result;
	}
}