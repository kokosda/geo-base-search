using GeoBaseSearch.Infrastructure.DataAccess.Abstract;
using Microsoft.Net.Http.Headers;

namespace GeoBaseSearch.Api.Extensions;

public static class WebApplicationExtensions
{
	public static IApplicationBuilder UseInitialization(this WebApplication app)
	{
		if (app == null)
			throw new ArgumentNullException(nameof(app));

		InitializeInMemoryDatabase(app);
		return app;
	}

	public static IApplicationBuilder UseResponseCachingSettings(this WebApplication app)
	{
		if (app == null)
			throw new ArgumentNullException(nameof(app));

		app.Use(async (context, next) =>
		{
			context.Response.GetTypedHeaders().CacheControl =
				new CacheControlHeaderValue
				{
					Public = true,
					MaxAge = TimeSpan.FromDays(1)
				};
			context.Response.Headers[HeaderNames.Vary] = new [] { "Accept-Encoding" };

			await next();
		});

		return app;
	}

	private static void InitializeInMemoryDatabase(WebApplication app)
	{
		var inMemoryDatabase = app.Services.GetRequiredService<IInMemoryDatabase>();
		var geoBaseFilePath = app.Configuration["GeoBaseFilePath"];
		inMemoryDatabase.InitializeGeoBase(geoBaseFilePath);
	}
}