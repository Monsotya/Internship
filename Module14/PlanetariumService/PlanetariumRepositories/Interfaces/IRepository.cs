
namespace PlanetariumRepositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> AddAsync(TEntity entity);
        IQueryable<TEntity> GetAll();
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}