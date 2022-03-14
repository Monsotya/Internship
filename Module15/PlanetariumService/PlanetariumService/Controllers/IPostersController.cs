using Microsoft.AspNetCore.Mvc;
using PlanetariumModels;
using PlanetariumService.Models;

namespace PlanetariumService.Controllers
{
    public interface IPostersController
    {
        ActionResult<List<PosterUI>> AddPosters();
        ActionResult<Poster> Create([Bind(new[] { "Id,DateOfEvent,Price,PerformanceId,HallId" }), FromQuery] Poster poster);
        void CreateTickets([FromQuery] int id);
        ActionResult<int> Delete([FromQuery] int id);
        ActionResult<Poster> Edit([FromQuery] int id, [Bind(new[] { "Id,DateOfEvent,Price,PerformanceId,HallId" })] Poster poster);
        ActionResult<PosterUI> PosterDetails(int id);
        ActionResult<List<PosterUI>> Posters(DateTime? dateFrom = null, DateTime? dateTo = null);
    }
}