using Microsoft.EntityFrameworkCore;
using PlanetariumModels;

namespace PlanetariumRepositories
{
    public class PosterRepository : Repository<Poster>, IPosterRepository
    {
        public PosterRepository(PlanetariumServiceContext repositoryPatternDemoContext) : base(repositoryPatternDemoContext)
        {
        }
        public List<Poster> Posters(DateTime dateFrom, DateTime dateTo)
        {
            var posters = base.GetAll().Include(x => x.Hall).Include(x => x.Performance).Include(x => x.Tickets);
            List<Poster> result = new();

            foreach (var poster in posters)
            {
                if (poster.DateOfEvent.CompareTo(dateFrom) >= 0 && poster.DateOfEvent.CompareTo(dateTo) <= 0)
                {
                    result.Add(poster);
                }
            }

            return result;

        }
        public Poster GetByIdAsync(int id) => base.GetAll().Include(x => x.Hall).Include(x => x.Performance).Include(x => x.Tickets).FirstOrDefaultAsync(x => x.Id == id).Result;

        public new List<Poster> GetAll() => base.GetAll().Include(x => x.Hall).Include(x => x.Performance).Include(x => x.Tickets).ToList<Poster>();
    }
}
