using Domain.DTOs;
using Domain.Models;

namespace Application.DAOInterface;

public interface IFieldDao
{
    Task<IEnumerable<FieldLookupDto>> GetFieldsByOwnerId(int ownerId);
}