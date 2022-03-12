using GeoBaseSearch.Infrastructure.DataAccess.Abstract;

namespace GeoBaseSearch.Api.Extensions;

public static class WebApplicationExtensions
{
	public static IApplicationBuilder UseInitialization(this WebApplication webApplication)
	{
		InitializeInMemoryDatabase(webApplication);
		return webApplication;
	}

	private static void InitializeInMemoryDatabase(WebApplication webApplication)
	{
		var inMemoryDatabase = webApplication.Services.GetRequiredService<IInMemoryDatabase>();
		var geoBaseFilePath = webApplication.Configuration["GeoBaseFilePath"];
		inMemoryDatabase.InitializeGeoBase(geoBaseFilePath);
	}
}