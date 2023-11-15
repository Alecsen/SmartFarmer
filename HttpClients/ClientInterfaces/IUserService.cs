using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDTO dto);

    Task<UserLoginDTO> Login(UserCreationDTO dto);
    Task<AuthenticationUser> GetAsync(string? username);
    Task<int> GetCurrentUserId();
    Task UpdateAsync(ProfileUpdateDto dto);
}