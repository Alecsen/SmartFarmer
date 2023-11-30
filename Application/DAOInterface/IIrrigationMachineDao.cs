using Domain.DTOs;
using Domain.Models;

namespace Application.DAOInterface;

public interface IIrrigationMachineDao
{
    
    Task<IEnumerable<IrrigationMachine>> GetIrrigationMachineByFieldId(int fieldId);
    
    Task<IrrigationMachine> CreateAsync(IrrigationMachine irrigationMachine);
}