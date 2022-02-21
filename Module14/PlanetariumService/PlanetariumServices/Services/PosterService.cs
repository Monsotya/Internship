using Microsoft.EntityFrameworkCore;
using PlanetariumModels;
using PlanetariumRepositories;


namespace PlanetariumServices
{
    public class PosterService : IPosterService
    {
        private readonly IPosterRepository posterRepository;

        public PosterService(IPosterRepository posterRepository) => this.posterRepository = posterRepository;
        public List<Poster> Posters(DateTime dateFrom, DateTime dateTo) => posterRepository.Posters(dateFrom, dateTo);
        public List<Poster> GetAll() => posterRepository.GetAll().Include(x => x.Hall).Include(x => x.Performance).ToList<Poster>();
        public async Task<Poster> Add(Poster poster) => await posterRepository.AddAsync(poster);
        public Poster GetById(int id) => posterRepository.GetByIdAsync(id);
        public Poster Update(Poster poster) => posterRepository.UpdateAsync(poster).Result;
        public async Task Delete(Poster poster) => await posterRepository.DeleteAsync(poster);
    }
}
