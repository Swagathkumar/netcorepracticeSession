using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using netcorepracticeSession.Data;
using netcorepracticeSession.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace netcorepracticeSession.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        ApiDbContext _dbContext = new ApiDbContext();
        private IConfiguration _config;                                                      //for JWT & Authenciation

        public UserController(IConfiguration config)                             //for JWT & Authenciation
        {
            _config = config;
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromBody] User user)
        {
            var userExists = _dbContext.users.FirstOrDefault(u => u.Email == user.Email);
            if (userExists != null)
            {
                return BadRequest("email address already exists");
            }
            _dbContext.users.Add(user);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] User user)
        {
            var currentuser = _dbContext.users.FirstOrDefault(u=>u.Email == user.Email && u.Password == user.Password);
            if(currentuser == null)
            {
                return NotFound();
            }
            //_dbContext.users.Add(user);
            //_dbContext.SaveChanges();
            //return StatusCode(StatusCodes.Status201Created);
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
               new Claim(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(jwt);


        }
    }
}
