using Application.DAOInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class IrrigationMachineEfcDao : IIrrigationMachineDao
{
    private readonly SmartFarmerAppContext context;

    public IrrigationMachineEfcDao(SmartFarmerAppContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<IrrigationMachine>> GetIrrigationMachineByFieldId(int fieldId)
    {
        var irrigationMachine = await context.IrrigationMachines
            .Where(irrigationMachine => irrigationMachine.FieldId == fieldId)
            .ToListAsync();

        return irrigationMachine;
    }

    public async Task<IrrigationMachine> CreateAsync(IrrigationMachine irrigationMachine)
    {
        EntityEntry<IrrigationMachine> newIrrigationMachine =
            await context.IrrigationMachines.AddAsync(irrigationMachine);
        await context.SaveChangesAsync();
        return newIrrigationMachine.Entity;
    }

    public Task<List<IrrigationMachine>> GetIrrigationMachineByOwnerId(int ownerId)
    {
        var irrigationMachine = context.IrrigationMachines
            .Where(irrigationMachine => irrigationMachine.OwnerId == ownerId)
            .ToListAsync();

        return irrigationMachine;
    }

    public async Task<IrrigationMachine> UpdateAsync(int id, IrrigationMachineUpdateDto dto)
    {
        IrrigationMachine irrigationMachineToUpdate =
            await context.IrrigationMachines.FirstOrDefaultAsync(machine => machine.Id == id) ??
            throw new InvalidOperationException();

        if (irrigationMachineToUpdate != null)
        {
            if (dto.FieldId != 0 && dto.FieldId != null)
            {
                irrigationMachineToUpdate.FieldId = dto.FieldId;
            }
            
            irrigationMachineToUpdate.IsRunning = dto.IsRunning;
            
            
            context.IrrigationMachines.Update(irrigationMachineToUpdate);
            await context.SaveChangesAsync();

            // Return the updated irrigation machine
            return irrigationMachineToUpdate;
        }
        else
        {
            throw new InvalidOperationException($"Irrigation machine with id {id} not found.");
        }
    }
}