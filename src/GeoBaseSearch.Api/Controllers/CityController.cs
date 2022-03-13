using GeoBaseSearch.Application.Cities;
using GeoBaseSearch.Core.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace GeoBaseSearch.Api.Controllers;

[Route("api/v1/city")]
[ApiController]
public sealed class CityController : ControllerBase
{
	private readonly IGenericQueryHandler<LocationsByCityQuery, LocationsByCityDto> _locationsByCityQueryHandler;

	public CityController(IGenericQueryHandler<LocationsByCityQuery, LocationsByCityDto> locationsByCityQueryHandler)
	{
		_locationsByCityQueryHandler = locationsByCityQueryHandler ?? throw new ArgumentNullException(nameof(locationsByCityQueryHandler));
	}

	[HttpGet]
	[Route("locations")]
	public async Task<ActionResult> GetLocations([FromQuery] LocationsByCityQuery query)
	{
		var responseContainer = await _locationsByCityQueryHandler.HandleAsync(query);

		if (!responseContainer.IsSuccess)
			return NotFound(responseContainer.Messages);

		return new JsonResult(responseContainer.Value);
	}
}