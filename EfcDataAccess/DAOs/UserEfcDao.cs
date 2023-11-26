using Application.DAOInterface;
using Domain.DTOs;
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

    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.Username.ToLower().Equals(userName.ToLower())
        );
        return existing;
    }


    public async Task UpdateAsync(ProfileUpdateDto dto)
    {
        // Find the user with the given username
        User? userToUpdate =
            await context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == dto.Username.ToLower());

        if (userToUpdate != null)
        {
            if (dto.Email != "")
                userToUpdate.Email = dto.Email;

            if (dto.Password != "")
                userToUpdate.Password = dto.Password;

            context.Users.Update(userToUpdate);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"User with username {dto.Username} not found.");
        }
        
    }

    public async Task<User?> GetByUserIdAsync(int id)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (existing != null)
        {
            return existing;
        }
        
        throw new InvalidOperationException($"User with user id is not found.");
        
    }
}