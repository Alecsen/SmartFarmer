using Domain.Models;

namespace Application.DAOInterface;

public interface IFieldDao
{
    Task<IEnumerable<Field>> GetFieldsByOwnerId(int ownerId);
}