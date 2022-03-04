using Microsoft.AspNetCore.Http;
using PlanetariumModels;
using PlanetariumRepositories;
using System.Security.Claims;

namespace PlanetariumServices
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRepository<Users> userRepository;

        public UserService(IHttpContextAccessor httpContextAccessor, IRepository<Users> userRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (httpContextAccessor.HttpContext != null)
            {
                result = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

        public List<Users> GetAllUsers() => userRepository.GetAll().ToList<Users>();
        public async Task<Users> Add(Users user) => await userRepository.AddAsync(user);

    }
}
