using GeoBaseSearch.Core.Interfaces;

namespace GeoBaseSearch.Core.Handlers
{
	public interface IGenericCommandHandler<in TCommand, TResult>
	{
		Task<IResponseContainerWithValue<TResult>> HandleAsync(TCommand command);
	}
}
