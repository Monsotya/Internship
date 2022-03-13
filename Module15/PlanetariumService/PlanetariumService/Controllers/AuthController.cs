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
        private readonly ILogger<AuthController> log;

        public AuthController(IConfiguration configuration, IUserService userService, ILogger<AuthController> log)
        {
            this.log = log;
            this.configuration = configuration;
            this.userService = userService;
        }

        /// <summary>
        /// Gets user`s name
        /// </summary>
        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {            
            try
            {
                var userName = userService.GetMyName();
                log.LogInformation("Got user`s name");
                return Ok(userName);
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Something went wrong");
                throw;
            }            
        }

        /// <summary>
        /// Registration for new user
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<UserUI>> Register(Users request)
        {
            user.Username = request.Username;
            user.Password = request.UserPassword;
            user.Role = request.UserRole;
            try
            {
                userService.Add(request);
                log.LogInformation("New user added");
                return Ok(user);
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Registration failed");
                throw;
            }            
        }

        /// <summary>
        /// Login for user
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(Users request)
        {

            if (userService.GetAllUsers().FirstOrDefault(x => x.Username == request.Username) == null)
            {
                log.LogError("Not found");
                return BadRequest("User not found.");
            }

            if (userService.GetAllUsers().FirstOrDefault(x => x.Username == request.Username).UserPassword != request.UserPassword)
            {
                log.LogError("Wrong password");
                return BadRequest("Wrong password.");
            }

            user.Username = request.Username;
            user.Password = request.UserPassword;
            try
            {
                user.Role = userService.GetAllUsers().FirstOrDefault(x => x.Username == request.Username).UserRole;
                string token = CreateToken(user);
                log.LogInformation("Login was successful");
                return Ok(token);
            }
            catch (Exception exception)
            {
                log.LogError(exception, "Login failed");
                throw;
            }
            
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

