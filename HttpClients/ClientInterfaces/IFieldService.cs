using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IFieldService
{
    Task<IEnumerable<FieldLookupDto>> GetFieldsByUserId(int userId);
    Task<Field> CreateField(FieldCreationDto dto);
}