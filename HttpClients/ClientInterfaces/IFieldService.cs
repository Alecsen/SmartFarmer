using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IFieldService
{
    Task<IEnumerable<FieldLookupDto>> GetFieldsByUserId(int userId);
}