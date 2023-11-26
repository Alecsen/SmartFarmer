using Domain.DTOs;
using Domain.Models;

namespace Application.DAOInterface;

public interface IFieldDao
{
    Task<IEnumerable<FieldLookupDto>> GetFieldsByOwnerId(int ownerId);
    Task<Field> CreateAsync(Field field);
    Task<Field> GetFieldById(int fieldId);
}