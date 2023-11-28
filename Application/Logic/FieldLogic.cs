using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class FieldLogic : IFieldLogic
{
    private readonly IFieldDao fieldDao;
    private readonly IUserDao userDao;

    public FieldLogic(IFieldDao fieldDao, IUserDao userDao)
    {
        this.fieldDao = fieldDao;
        this.userDao = userDao;
    }

    public Task<IEnumerable<FieldLookupDto>> GetAsync(int ownerId)
    {
        if (ownerId == -1)
        {
            throw new Exception($"The Id {ownerId} is not a valid number");
        }
        
        return fieldDao.GetFieldsByOwnerId(ownerId);
    }

    public async Task<Field> CreateAsync(FieldCreationDto dto)
    {
        //checking if the user exists
        await userDao.GetByUserIdAsync(dto.OwnerId);

        if (dto.LocationData == null)
        {
            throw new Exception("There is not location data so field cannot be created");
        }
        
        Field field = new Field
        {
            Name = dto.FieldName,
            OwnerId = dto.OwnerId,
            LocationData = dto.LocationData,
            CropType = dto.CropType,
            ImportanceLevel = dto.ImportanceLevel
        };

        Field created = await fieldDao.CreateAsync(field);
        return created;
    }
}