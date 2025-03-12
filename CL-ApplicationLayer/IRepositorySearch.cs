using System.Linq.Expressions;

namespace CL_ApplicationLayer
{
    public interface IRepositorySearch<TModel, TEntity>
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TModel, bool>> predicate);
    }
}