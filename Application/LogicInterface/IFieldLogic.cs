using Domain.Models;

namespace Application.LogicInterface;

public interface IFieldLogic
{
    public Task<IEnumerable<Field>> GetAsync(int OwnerId);
}