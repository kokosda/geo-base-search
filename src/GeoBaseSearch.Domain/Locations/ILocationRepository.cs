namespace GeoBaseSearch.Domain.Locations;

public interface ILocationRepository
{
	Task<Location?> GetLocationByIpAddress(uint ipAddress);
	Task<Location?[]> GetLocationsByCity(string city);
}