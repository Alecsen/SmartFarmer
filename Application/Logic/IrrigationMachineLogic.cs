using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class IrrigationMachineLogic : IIrrigationMachineLogic
{
    
    private readonly IIrrigationMachineDao irrigationMachineDao;
    private readonly IUserDao userDao;
    private readonly IFieldDao fieldDao;
    public IrrigationMachineLogic(IIrrigationMachineDao irrigationMachineDao, IUserDao userDao, IFieldDao fieldDao)
    {
        this.irrigationMachineDao = irrigationMachineDao;
        this.userDao = userDao;
        this.fieldDao = fieldDao;
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

    public async Task UpdateAsync(int id, int ownerId, IrrigationMachineUpdateDto dto)
    {
        // Retrieve all the fields assigned to the user from the database
        IEnumerable<FieldLookupDto> userFields = await fieldDao.GetFieldsByOwnerId(ownerId);

        // Check if the new fieldId is in the list of fields assigned to the user
        if (userFields.Any(field => field.Id == dto.FieldId))
        {
            await irrigationMachineDao.UpdateAsync(id, dto);
        }
        else
        {
            throw new InvalidOperationException($"Field with id {dto.FieldId} not assigned to the user.");
        }
    }
}