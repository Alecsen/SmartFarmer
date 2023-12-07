using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IIrrigationMachineService
{
    Task<IEnumerable<IrrigationMachine>> GetByOwnerId(int ownerId);
    Task UpdateAsync(int id, int ownerId, IrrigationMachineUpdateDto dto);
}