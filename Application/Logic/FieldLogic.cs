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

    public Task<IEnumerable<FieldLookupDto>> GetAsync(int OwnerId)
    {
        return fieldDao.GetFieldsByOwnerId(OwnerId);
    }

    public async Task<Field> CreateAsync(FieldCreationDto dto)
    {
        //checking if the user exists
        await userDao.GetByUserIdAsync(dto.OwnerId);
        
        Field field = new Field
        {
            Name = dto.FieldName,
            OwnerId = dto.OwnerId
        };

        Field created = await fieldDao.CreateAsync(field);
        return created;
    }
}