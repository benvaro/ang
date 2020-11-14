using JwtDemo.DataAccess.Entities;
using JwtDemo.Domain.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtDemo.Domain.Implementation
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public JWTTokenService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public string CreateToken(User user)
        {
            // JWT - JSON WEB Token
            // Header.Payload.Signature

            var roles = _userManager.GetRolesAsync(user).Result;

            var claims = new List<Claim>
            {
                new Claim("id", user.Id),
                new Claim("email", user.Email)
            };
            foreach (var item in roles)
            {
                claims.Add(new Claim("role", item));
            }

            var jwtTokenSecretKey = _configuration.GetValue<string>("SecretPhrase");

            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSecretKey));
            var signInCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(signingCredentials: signInCredentials, claims: claims, expires: DateTime.Now.AddDays(3));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
