using GeoBaseSearch.Infrastructure.DataAccess.Abstract;
using GeoBaseSearch.Infrastructure.Models;

namespace GeoBaseSearch.Infrastructure.DataAccess;

public sealed class GeoBaseFileLoader : IGeoBaseLoader
{
    private readonly IGeoBaseImageParser _geoBaseImageParser;

    public GeoBaseFileLoader(IGeoBaseImageParser geoBaseImageParser)
    {
        _geoBaseImageParser = geoBaseImageParser ?? throw new ArgumentNullException(nameof(geoBaseImageParser));
    }

    public GeoBaseModel Load(string geoBaseFilePath)
    {
        if (string.IsNullOrWhiteSpace(geoBaseFilePath))
            throw new ArgumentNullException(nameof(geoBaseFilePath));

        var geoBaseImage = File.ReadAllBytes(geoBaseFilePath);
        var result = _geoBaseImageParser.Parse(geoBaseImage);
        return result;
    }
}