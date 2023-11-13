using Application.DAOInterface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDao
{

    private readonly SmartFarmerAppContext context;

    public UserEfcDao(SmartFarmerAppContext context)
    {
        this.context = context;
    }

    public async Task<AuthenticationUser> CreateAsync(AuthenticationUser user)
    {
        EntityEntry<AuthenticationUser> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<AuthenticationUser?> GetByUsernameAsync(string userName)
    {
        AuthenticationUser? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }
    
    public async Task<AuthenticationUser> UpdateAsync(string username, string? email, string? password)
    {
        // Find the user with the given username
        AuthenticationUser? userToUpdate = await context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

        if (userToUpdate != null)
        {
            if (email != null)
                userToUpdate.Email = email;

            if (password != null)
                userToUpdate.Password = password;
            
            context.Users.Update(userToUpdate);
            await context.SaveChangesAsync();

            return userToUpdate;
        }
        
        throw new InvalidOperationException($"User with username {username} not found.");
    }
}