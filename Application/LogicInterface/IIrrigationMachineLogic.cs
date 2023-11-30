using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterface;

public interface IIrrigationMachineLogic
{
    public Task<IEnumerable<IrrigationMachineLookupDto>> GetAsync(int ownerId);

    Task<IrrigationMachine> CreateAsync(IrrigationMachineCreationDto dto);
}