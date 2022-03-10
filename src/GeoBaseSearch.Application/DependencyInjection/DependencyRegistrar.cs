using Microsoft.Extensions.DependencyInjection;

namespace GeoBaseSearch.Application.DependencyInjection
{
	public static class DependencyRegistrar
	{
		public static IServiceCollection AddApplicationLevelServices(this IServiceCollection serviceCollection)
		{
			return serviceCollection;
		}
	}
}
