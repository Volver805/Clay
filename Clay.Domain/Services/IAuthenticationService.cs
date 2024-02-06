using Clay.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Clay.Domain.Services
{
    public interface IAuthenticationService
    {
        public string? AuthenticateUser(string username, string password);
        public string GenerateBearerToken(User user);
        public int? GetCurrentUserIdFromToken(HttpContext context);
        public bool isAdmin(int userId);
    }
}
