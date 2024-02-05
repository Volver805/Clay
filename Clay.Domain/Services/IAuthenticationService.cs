using Clay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clay.Domain.Services
{
    public interface IAuthenticationService
    {
        public Task<string?> AuthenticateUser(string username, string password);
        public string GenerateBearerToken(User user);
    }
}
