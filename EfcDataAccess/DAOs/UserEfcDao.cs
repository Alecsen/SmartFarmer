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


    public async Task UpdateAsync(ProfileUpdateDto dto)
    {
        // Find the user with the given username
        AuthenticationUser? userToUpdate =
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
}

/*
public Task UpdateAsync(AuthenticationUser toUpdate)
{
    AuthenticationUser? existing = context.Users.FirstOrDefault(user => user.Username == toUpdate.Username);
    if (existing == null)
    {
        throw new Exception($"User with id {toUpdate.Username} does not exist!");
    }

    context.Users.Remove(existing);
    context.Add(toUpdate);

    context.SaveChanges();

    return Task.CompletedTask;
}
}
*/