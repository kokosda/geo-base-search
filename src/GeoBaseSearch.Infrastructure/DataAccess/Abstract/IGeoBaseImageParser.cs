using GeoBaseSearch.Infrastructure.Models;

namespace GeoBaseSearch.Infrastructure.DataAccess.Abstract;

public interface IGeoBaseImageParser
{
	GeoBaseModel Parse(byte[] geoBaseImage);
}