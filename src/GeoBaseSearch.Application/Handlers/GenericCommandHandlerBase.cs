using GeoBaseSearch.Core.Handlers;
using GeoBaseSearch.Core.Interfaces;

namespace GeoBaseSearch.Application.Handlers
{
	public abstract class GenericCommandHandlerBase<TCommand, TResult> : IGenericCommandHandler<TCommand, TResult>
	{
		public async Task<IResponseContainerWithValue<TResult>> HandleAsync(TCommand command)
		{
			if (command == null)
				throw new ArgumentNullException(nameof(command));

			return await GetResultAsync(command);
		}

		protected abstract Task<IResponseContainerWithValue<TResult>> GetResultAsync(TCommand command);
	}
}
