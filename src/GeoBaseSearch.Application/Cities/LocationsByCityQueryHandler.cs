using GeoBaseSearch.Application.Handlers;
using GeoBaseSearch.Core.Interfaces;

namespace GeoBaseSearch.Application.Cities;

public sealed class LocationsByCitiesQueryHandler : GenericQueryHandlerBase<LocationsByCityQuery, LocationsByCityDto>
{
	protected override Task<IResponseContainerWithValue<LocationsByCityDto>> GetResultAsync(LocationsByCityQuery query)
	{
		throw new NotImplementedException();
	}
}