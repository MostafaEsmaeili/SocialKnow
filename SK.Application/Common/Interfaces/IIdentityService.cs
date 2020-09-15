﻿using SK.Application.Common.Models;
using SK.Domain.Entities;
using System.Threading.Tasks;

namespace SK.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<AppUser> GetUserByEmailAsync(string email);

        Task<(Result Result, string UserId)> CreateUserAsync(AppUser user, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}

