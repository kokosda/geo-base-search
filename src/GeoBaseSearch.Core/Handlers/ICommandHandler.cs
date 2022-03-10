using GeoBaseSearch.Core.Interfaces;

namespace GeoBaseSearch.Core.Handlers
{
	public interface ICommandHandler<in T>
	{
		Task<IResponseContainer> HandleAsync(T command);
	}
}
