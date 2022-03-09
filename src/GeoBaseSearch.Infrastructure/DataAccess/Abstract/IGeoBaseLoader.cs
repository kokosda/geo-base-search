using GeoBaseSearch.Infrastructure.Models;

namespace GeoBaseSearch.Infrastructure.DataAccess.Abstract;

public interface IGeoBaseLoader
{
    GeoBaseModel Load(string geoBaseFilePath);
}