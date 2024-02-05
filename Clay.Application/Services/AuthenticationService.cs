using Clay.Domain.Entities;
using Clay.Domain.Repositories;
using Clay.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clay.Application.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        public AuthenticationService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string? AuthenticateUser(string username, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(user => user.Username == username);

            if(user == null || validateUserPassword(user.HashPassword, password) != PasswordVerificationResult.Success)
            {
                return null;
            }

            return GenerateBearerToken(user);
        }

        public string GenerateBearerToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JWTSettings");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secert"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private PasswordVerificationResult validateUserPassword(string hashedPassword, string password)
        {
            var passwordHasher = new PasswordHasher<Object>();
            return passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
        }
    }
}
