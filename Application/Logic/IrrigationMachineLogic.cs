using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class IrrigationMachineLogic : IIrrigationMachineLogic
{
    
    private readonly IIrrigationMachineDao irrigationMachineDao;

    public IrrigationMachineLogic(IIrrigationMachineDao irrigationMachineDao)
    {
        this.irrigationMachineDao = irrigationMachineDao;
    }

    public Task<IEnumerable<IrrigationMachine>> GetAsync(int fieldId)
    {
        return irrigationMachineDao.GetIrrigationMachineByFieldId(fieldId);
    }

    public async Task<IrrigationMachine> CreateAsync(IrrigationMachineCreationDto dto)
    {
        
        IrrigationMachine irrigationMachine = new IrrigationMachine
        {
            Id = dto.Id,
            FieldId = dto.FieldId,
            WaterAmount = dto.WaterAmount,
            IsRunning = dto.IsRunning
        };
        var created = await irrigationMachineDao.CreateAsync(irrigationMachine);
        
        return created;
    }
}