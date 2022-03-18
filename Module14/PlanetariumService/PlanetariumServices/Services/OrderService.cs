using PlanetariumModels;
using PlanetariumRepositories;

namespace PlanetariumServices
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Orders> ordersRepository;
        public OrderService(IRepository<Orders> ordersRepository) => this.ordersRepository = ordersRepository;
        public List<Orders> GetAll() => ordersRepository.GetAll().ToList<Orders>();
        public async Task<Orders> Add(Orders order) => await ordersRepository.AddAsync(order);
    }
}
