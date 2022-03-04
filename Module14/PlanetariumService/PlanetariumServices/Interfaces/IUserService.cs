using PlanetariumModels;

namespace PlanetariumServices
{
    public interface IUserService
    {
        string GetMyName();
        public List<Users> GetAllUsers();
        public Task<Users> Add(Users user);
    }
}
