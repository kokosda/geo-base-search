using GeoBaseSearch.Infrastructure.DataAccess.Abstract;
using GeoBaseSearch.Infrastructure.Models;

namespace GeoBaseSearch.Infrastructure.DataAccess;

public sealed class InMemoryDatabase : IInMemoryDatabase
{
	private readonly IGeoBaseLoader _geoBaseLoader;

	public GeoBaseModel? GeoBase { get; private set; }

	public InMemoryDatabase(IGeoBaseLoader geoBaseLoader)
	{
		_geoBaseLoader = geoBaseLoader ?? throw new ArgumentNullException(nameof(geoBaseLoader));
	}

	public void InitializeGeoBase(string geoBaseFilePath)
	{
		GeoBase = _geoBaseLoader.Load(geoBaseFilePath);
	}
}