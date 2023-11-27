using Domain.DTOs;
using Domain.Models;

namespace Application.DAOInterface;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
    Task UpdateAsync(ProfileUpdateDto dto);
    Task<User?> GetByUserIdAsync(int id);
}