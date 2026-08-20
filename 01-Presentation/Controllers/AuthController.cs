using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        public class LoginRequest
        {
            public string Usuario { get; set; }
            public string Senha { get; set; }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            if (req == null) return BadRequest();

            // credenciais estáticas conforme solicitado
            if (req.Usuario == "admin" && req.Senha == "123456")
            {
                var jwtSection = _config.GetSection("Jwt");
                var key = jwtSection.GetValue<string>("Key");
                var issuer = jwtSection.GetValue<string>("Issuer") ?? "Mod10Api";
                var audience = jwtSection.GetValue<string>("Audience") ?? "Mod10Clients";
                var duration = jwtSection.GetValue<int>("DurationMinutes", 120);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, req.Usuario),
                        new Claim(ClaimTypes.Role, "Admin")
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(duration),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { token = tokenString, expires = tokenDescriptor.Expires });
            }

            return Unauthorized();
        }
    }
}
