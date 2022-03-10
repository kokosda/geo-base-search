using GeoBaseSearch.Core.Handlers;
using GeoBaseSearch.Core.Interfaces;

namespace GeoBaseSearch.Application.Handlers
{
	public abstract class QueryHandlerBase<T> : IQueryHandler<T>
	{
		public async Task<IResponseContainer> HandleAsync(T query)
		{
			if (query == null)
				throw new ArgumentNullException(nameof(query));

			var result = await GetResultAsync(query);
			return result;
		}

		protected abstract Task<IResponseContainer> GetResultAsync(T command);
	}
}
