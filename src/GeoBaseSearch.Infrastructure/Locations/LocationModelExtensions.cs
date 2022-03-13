using GeoBaseSearch.Domain.Locations;
using GeoBaseSearch.Infrastructure.Models;

namespace GeoBaseSearch.Infrastructure.Locations;

public static class LocationModelExtensions
{
	public static Location ToLocation(this LocationModel locationModel)
	{
		var result = new Location
		{
			City = locationModel.City,
			Country = locationModel.Country,
			Id = locationModel.Id,
			Latitude = locationModel.Latitude,
			Longitude = locationModel.Longitude,
			Organization = locationModel.Organization,
			Postal = locationModel.Postal,
			Region = locationModel.Region
		};

		return result;
	}
}