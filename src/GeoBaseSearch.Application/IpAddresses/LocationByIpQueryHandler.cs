using System.Net;
using GeoBaseSearch.Application.Handlers;
using GeoBaseSearch.Core.Interfaces;
using GeoBaseSearch.Core.ResponseContainers;
using GeoBaseSearch.Domain.Locations;
using GeoBaseSearch.Infrastructure.IpAddresses.Interfaces;

namespace GeoBaseSearch.Application.IpAddresses;

public sealed class LocationByIpQueryHandler : GenericQueryHandlerBase<LocationByIpQuery, LocationByIpDto>
{
	private readonly IIpAddressConverter _ipAddressConverter;
	private readonly ILocationRepository _locationRepository;

	public LocationByIpQueryHandler(IIpAddressConverter ipAddressConverter, ILocationRepository locationRepository)
	{
		_ipAddressConverter = ipAddressConverter ?? throw new ArgumentNullException(nameof(ipAddressConverter));
		_locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
	}

	protected override async Task<IResponseContainerWithValue<LocationByIpDto>> GetResultAsync(LocationByIpQuery query)
	{
		var result = new ResponseContainerWithValue<LocationByIpDto>();

		var ipAddressResponseContainer = _ipAddressConverter.ConvertStringToUInt32IpAddress(query.Ip);

		if (!ipAddressResponseContainer.IsSuccess)
		{
			result.SetErrorValue(new LocationByIpDto { HttpStatusCode = HttpStatusCode.BadRequest }, ipAddressResponseContainer.Messages);
			return result;
		}

		var ipAddressUInt32 = ipAddressResponseContainer.Value;
		var location = await _locationRepository.GetLocationByIpAddress(ipAddressUInt32);

		if (location == null)
		{
			result.SetErrorValue(new LocationByIpDto { HttpStatusCode = HttpStatusCode.NotFound }, $"Provided value {query.Ip} can not be found among registered records.");
			return result;
		}

		var locationByIpDto = LocationByIpDto.FromLocation(location, HttpStatusCode.OK);
		result.SetSuccessValue(locationByIpDto);
		return result;
	}
}