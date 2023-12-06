using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class IrrigationMachineLogic : IIrrigationMachineLogic
{
    
    private readonly IIrrigationMachineDao irrigationMachineDao;
    private readonly IUserDao userDao;

    public IrrigationMachineLogic(IIrrigationMachineDao irrigationMachineDao, IUserDao userDao)
    {
        this.irrigationMachineDao = irrigationMachineDao;
        this.userDao = userDao;
    }

    public Task<IEnumerable<IrrigationMachine>> GetAsync(int fieldId)
    {
        return irrigationMachineDao.GetIrrigationMachineByFieldId(fieldId);
    }

    public async Task<IrrigationMachine> CreateAsync(IrrigationMachineCreationDto dto)
    {

        if (dto.OwnerId != null)
        {
            try
            {
                // This method will throw an exception if the user does not exist
                await userDao.GetByUserIdAsync(dto.OwnerId);
            }
            catch (InvalidOperationException)
            {
                // Handle the exception here, for example, you can throw a new exception with a custom message
                throw new InvalidOperationException("The user does not exist.");
            }
        }
        
        IrrigationMachine irrigationMachine = new IrrigationMachine
        {
            OwnerId = dto.OwnerId,
            WaterAmount = dto.WaterAmount,
            IsRunning = dto.IsRunning
        };
        var created = await irrigationMachineDao.CreateAsync(irrigationMachine);
        
        return created;
    }

    public async Task<List<IrrigationMachine>> GetByOwnerIdAsync(int ownerId)
    {
       return await irrigationMachineDao.GetIrrigationMachineByOwnerId(ownerId);
    }
}