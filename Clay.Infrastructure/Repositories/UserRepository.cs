using Clay.Domain.Entities;
using Clay.Domain.Repositories;
using Clay.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clay.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User?> GetUserByUsernameAndPassword(string username, string password)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || validateUserPassword(user.HashPassword, password) != PasswordVerificationResult.Success)
            {
                return null;
            }

            return user;
        }

        private PasswordVerificationResult validateUserPassword(string hashedPassword, string password)
        {
            var passwordHasher = new PasswordHasher<Object>();
            return passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
        }
    }
}
