using System.Net;
using GeoBaseSearch.Application.Handlers;
using GeoBaseSearch.Core.Interfaces;
using GeoBaseSearch.Core.ResponseContainers;
using GeoBaseSearch.Domain.Locations;

namespace GeoBaseSearch.Application.Cities;

public sealed class LocationsByCitiesQueryHandler : GenericQueryHandlerBase<LocationsByCityQuery, LocationsByCityDto>
{
	private readonly ILocationRepository _locationRepository;

	public LocationsByCitiesQueryHandler(ILocationRepository locationRepository)
	{
		_locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
	}

	protected override async Task<IResponseContainerWithValue<LocationsByCityDto>> GetResultAsync(LocationsByCityQuery query)
	{
		var result = new ResponseContainerWithValue<LocationsByCityDto>();
		var locations = await _locationRepository.GetLocationsByCity(query.City);

		if (!locations.Any())
		{
			result.SetErrorValue(new LocationsByCityDto { HttpStatusCode = HttpStatusCode.NotFound }, $"Locations associated with the city {query.City} could not be found.");
			return result;
		}

		var locationByIpDto = new LocationsByCityDto
		{
			Locations = locations.Select(LocationByCityDto.FromLocation).ToArray()
		};

		result.SetSuccessValue(locationByIpDto);
		return result;
	}
}