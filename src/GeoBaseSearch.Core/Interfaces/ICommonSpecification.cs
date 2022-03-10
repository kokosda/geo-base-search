namespace GeoBaseSearch.Core.Interfaces
{
	public interface ICommonSpecification<in T>
	{
		Task<IResponseContainer> IsSatisfiedBy(T subject);
	}
}
