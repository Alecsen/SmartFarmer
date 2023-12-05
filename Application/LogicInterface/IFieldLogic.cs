using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterface;

public interface IFieldLogic
{
    public Task<IEnumerable<FieldLookupDto>> GetAsync(int ownerId);
    public Task<Field> GetByIdAsync(int fieldId);
    Task<Field> CreateAsync(FieldCreationDto dto);
    Task<Task> PerformCalculation();
}