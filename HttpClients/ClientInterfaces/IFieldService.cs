using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IFieldService
{
    Task<IEnumerable<FieldLookupDto>> GetFieldsByUserId(int userId);
    Task<Field> GetFieldById(int fieldId);
    Task<Field> CreateField(FieldCreationDto dto);
}