using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterface;

public interface IIrrigationMachineLogic
{
    public Task<IEnumerable<IrrigationMachine>> GetAsync(int ownerId);

    Task<IrrigationMachine> CreateAsync(IrrigationMachineCreationDto dto);
    Task<List<IrrigationMachine>> GetByOwnerIdAsync(int ownerId);
}