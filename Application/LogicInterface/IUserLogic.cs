using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterface;

public interface IUserLogic
{
    public Task<User> CreateAsync(UserCreationDTO dto);

    public Task<User> ValidateLogin(AuthUserLoginDto dto);
    
    public Task<User> GetAsync(UserSearchParametersDTO dto);
    public Task UpdateAsync(ProfileUpdateDto dto);
}