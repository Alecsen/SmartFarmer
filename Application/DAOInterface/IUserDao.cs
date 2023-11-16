using Domain.DTOs;
using Domain.Models;

namespace Application.DAOInterface;

public interface IUserDao
{
    Task<AuthenticationUser> CreateAsync(AuthenticationUser user);
    Task<AuthenticationUser?> GetByUsernameAsync(string userName);
    Task UpdateAsync(ProfileUpdateDto dto);
    Task<AuthenticationUser?> GetByUserIdAsync(int id);
}