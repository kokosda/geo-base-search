﻿namespace GeoBaseSearch.Domain.Locations;

public interface ILocationRepository
{
	Task<Location?> GetLocationByIpAddress(uint ipAddress);
}