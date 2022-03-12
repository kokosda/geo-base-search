using GeoBaseSearch.Application.Cities;
using GeoBaseSearch.Core.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace GeoBaseSearch.Api.Controllers;

[Route("city")]
[ApiController]
public sealed class CityController : ControllerBase
{
	private readonly IGenericQueryHandler<LocationsByCityQuery, LocationsByCityDto> _locationsByCityQueryHandler;

	public CityController(IGenericQueryHandler<LocationsByCityQuery, LocationsByCityDto> locationsByCityQueryHandler)
	{
		_locationsByCityQueryHandler = locationsByCityQueryHandler ?? throw new ArgumentNullException(nameof(locationsByCityQueryHandler));
	}

	[HttpGet]
	public async Task<ActionResult> Locations([FromQuery] LocationsByCityQuery query)
	{
		var responseContainer = await _locationsByCityQueryHandler.HandleAsync(query);

		if (!responseContainer.IsSuccess)
			return NotFound(responseContainer.Messages);

		return new JsonResult(responseContainer.Value);
	}
}