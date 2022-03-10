using GeoBaseSearch.Core.Domain;

namespace GeoBaseSearch.Core.Interfaces
{
	public interface ISpecification<in T, TId> where T : EntityBase<TId>
	{
		IResponseContainer IsSatisfiedBy(T entity);
	}
}