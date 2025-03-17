using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Services.Token
{
    public class TokenService(DataContext context,IConfiguration configuration) : ITokenService
    {
        public async Task<string> GenerateJwtToken(Guid userId)
        {
            var user = await context.Admins.FirstOrDefaultAsync(s => s.Id == userId);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
            var token = new JwtSecurityToken(

               issuer: "http://localhost:5275",
               audience: "https://localhost:7075",
               claims,
               expires: DateTime.UtcNow.AddYears(1),
               signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
