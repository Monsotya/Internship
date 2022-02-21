using PlanetariumModels;

namespace PlanetariumServices
{
    public interface IPosterService
    {
        List<Poster> Posters(DateTime dateFrom, DateTime dateTo);

        public List<Poster> GetAll();

        public Task<Poster> Add(Poster poster);
        public Poster GetById(int id);
        public Poster Update(Poster poster);
        public Task Delete(Poster poster);

    }
}