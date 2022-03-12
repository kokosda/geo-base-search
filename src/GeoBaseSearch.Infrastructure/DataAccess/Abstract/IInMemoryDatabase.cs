using GeoBaseSearch.Infrastructure.Models;

namespace GeoBaseSearch.Infrastructure.DataAccess.Abstract;

public interface IInMemoryDatabase
{
	GeoBaseModel? GeoBase { get; }
	void InitializeGeoBase(string geoBaseFilePath);
}