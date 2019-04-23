using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChatWpf.Web.Server.Data;
using Dna;
using Microsoft.IdentityModel.Tokens;

namespace ChatWpf.Web.Server.Authentication
{
    public static class JwtTokenExtensionMethods
    {
        public static string GenerateJwtToken(this ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),

                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),

                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(FrameworkDI.Configuration["Jwt:SecretKey"])),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: FrameworkDI.Configuration["Jwt:Issuer"],
                audience: FrameworkDI.Configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMonths(3));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
