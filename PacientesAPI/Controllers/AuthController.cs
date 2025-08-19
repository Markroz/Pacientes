using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PacientesAPI.Models.Request;
using Pacientes.DataBase.Utils;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PacientesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DbContextAPI _context;
        private readonly IConfiguration _config;

        public AuthController(DbContextAPI context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Usuario == request.Username);
            if (user is null) return Unauthorized("Usuario o contraseña incorrectos");

            var ok = BCrypt.Net.BCrypt.Verify(request.Password, user.Contrasenia);
            if (!ok) return Unauthorized("Usuario o contraseña incorrectos");

            var token = GenerateJwtToken(user.Usuario, user.Rol);

            var jwt = _config.GetSection("Jwt");
            var expiresHours = int.TryParse(jwt["ExpiresHours"], out var h) ? h : 12;
            return Ok(new { token, user.Rol, expires = DateTime.UtcNow.AddHours(expiresHours) });
        }

        private string GenerateJwtToken(string username, string role)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

            var expiresHours = int.TryParse(jwt["ExpiresHours"], out var h) ? h : 12;

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expiresHours),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
