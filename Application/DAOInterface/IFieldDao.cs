using Domain.Models;

namespace Application.DAOInterface;

public interface IFieldDao
{
    IEnumerable<Field> GetFieldsByOwnerId(int ownerId);
}