using GeoBaseSearch.Core.Domain;

namespace GeoBaseSearch.Core.Interfaces
{
	public interface IGenericRepository
	{
		Task<T> CreateAsync<T, TId>(T entity) where T : EntityBase<TId>;
		Task<T> GetAsync<T, TId>(TId id) where T : EntityBase<TId>;
		Task UpdateAsync<T, TId>(T entity) where T : EntityBase<TId>;
		Task DeleteAsync<T, TId>(TId id) where T : EntityBase<TId>;
		Task<IResponseContainer> ApplyChangesAsync();
	}
}
