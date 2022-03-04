using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlanetariumModels;
using PlanetariumService.Models;
using PlanetariumServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PlanetariumService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static UserUI user = new UserUI();
        private readonly IConfiguration configuration;
        private readonly IUserService userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = userService.GetMyName();
            return Ok(userName);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserUI>> Register(Users request)
        {
            user.Username = request.Username;
            user.Password = request.UserPassword;
            user.Role = request.UserRole;

            userService.Add(request);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(Users request)
        {

            if (userService.GetAllUsers().FirstOrDefault(x => x.Username == request.Username) == null)
            {
                return BadRequest("User not found.");
            }

            if (userService.GetAllUsers().FirstOrDefault(x => x.Username == request.Username).UserPassword != request.UserPassword)
            {
                return BadRequest("Wrong password.");
            }

            user.Username = request.Username;
            user.Password = request.UserPassword;
            user.Role = userService.GetAllUsers().FirstOrDefault(x => x.Username == request.Username).UserRole;
            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(UserUI user)
        {
            List<Claim> claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, user.Username),
                 new Claim(ClaimTypes.Role, user.Role)
             };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }

}

