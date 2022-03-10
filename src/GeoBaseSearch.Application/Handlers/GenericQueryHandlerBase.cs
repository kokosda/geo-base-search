using GeoBaseSearch.Core.Handlers;
using GeoBaseSearch.Core.Interfaces;

namespace GeoBaseSearch.Application.Handlers
{
	public abstract class GenericQueryHandlerBase<TQuery, TResult> : IGenericQueryHandler<TQuery, TResult>
	{
		public async Task<IResponseContainerWithValue<TResult>> HandleAsync(TQuery query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			return await GetResultAsync(query);
		}

		protected abstract Task<IResponseContainerWithValue<TResult>> GetResultAsync(TQuery query);
	}
}
