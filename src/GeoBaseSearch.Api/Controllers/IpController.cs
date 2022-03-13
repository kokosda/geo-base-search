﻿using System.Net;
using GeoBaseSearch.Application.IpAddresses;
using GeoBaseSearch.Core.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace GeoBaseSearch.Api.Controllers;

[Route("ip")]
[ApiController]
public sealed class IpController : ControllerBase
{
	private readonly IGenericQueryHandler<LocationByIpQuery, LocationByIpDto> _locationByIpQueryHandler;

	public IpController(IGenericQueryHandler<LocationByIpQuery, LocationByIpDto> locationByIpQueryHandler)
	{
		_locationByIpQueryHandler = locationByIpQueryHandler ?? throw new ArgumentNullException(nameof(locationByIpQueryHandler));
	}

	[HttpGet]
	public async Task<ActionResult> Location([FromQuery] LocationByIpQuery query)
	{
		var responseContainer = await _locationByIpQueryHandler.HandleAsync(query);

		if (!responseContainer.IsSuccess)
			return new ContentResult
			{
				Content = responseContainer.Messages,
				StatusCode = (int)(responseContainer.Value?.HttpStatusCode ?? HttpStatusCode.BadRequest)
			};

		return new JsonResult(responseContainer.Value);
	}
}