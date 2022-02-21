using PlanetariumModels;

namespace PlanetariumRepositories
{
    public interface IPosterRepository
    {
        List<Poster> Posters(DateTime dateFrom, DateTime dateTo);

        public IQueryable<Poster> GetAll();
        public Task<Poster> AddAsync(Poster entity);
        public Task<Poster> UpdateAsync(Poster entity);
        public Task<Poster> DeleteAsync(Poster entity);
        public Poster GetByIdAsync(int id);
    }
}