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
        EntityEntry<IrrigationMachine> newIrrigationMachine = await context.IrrigationMachines.AddAsync(irrigationMachine);
        await context.SaveChangesAsync();
        return newIrrigationMachine.Entity;
    }
    
}