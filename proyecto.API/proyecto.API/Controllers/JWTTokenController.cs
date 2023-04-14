using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using proyecto.API.Entities;
using proyecto.API.Entities.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTTokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly NombreDbContext _context;
        public JWTTokenController(IConfiguration configuration, NombreDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post(LoginUser user)
        {
            if (user == null || user.Username == null || user.Password == null)
            {
                return BadRequest();
            }

            var userData = await _context.Usuarios.Include(c => c.IdrolNavigation).FirstOrDefaultAsync(
                u => u.Username == user.Username &&
                u.Clave == user.Password
                );

            if (userData == null)
            {
                return NoContent();
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("username", userData.Username),
                new Claim("name", userData.Nombre),
                new Claim("role", userData.IdrolNavigation.Descripcion)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    jwt.Issuer,
            jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signIn
                );

            LoginResponse loginResponse = new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return Ok(loginResponse);
        }
    }
}
