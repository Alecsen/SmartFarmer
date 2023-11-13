using Domain.Models;

namespace Application.DAOInterface;

public interface IUserDao
{
    Task<AuthenticationUser> CreateAsync(AuthenticationUser user);
    Task<AuthenticationUser?> GetByUsernameAsync(string userName);
    Task<AuthenticationUser> UpdateAsync(string username, string? email, string? password);
}