﻿using Clay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clay.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetUserByUsernameAndPassword(string username, string password);
    }
}