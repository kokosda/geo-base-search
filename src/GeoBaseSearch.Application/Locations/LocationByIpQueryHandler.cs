using GeoBaseSearch.Application.Handlers;
using GeoBaseSearch.Core.Interfaces;

namespace GeoBaseSearch.Application.Locations;

public sealed class LocationByIpQueryHandler : GenericQueryHandlerBase<LocationByIpQuery, LocationByIpDto>
{
	protected override Task<IResponseContainerWithValue<LocationByIpDto>> GetResultAsync(LocationByIpQuery query)
	{
		throw new NotImplementedException();
	}
}