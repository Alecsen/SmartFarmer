using Application.DAOInterface;
using Application.LogicInterface;
using Domain.Models;

namespace Application.Logic;

public class FieldLogic : IFieldLogic
{
    private readonly IFieldDao fieldDao;

    public FieldLogic(IFieldDao fieldDao)
    {
        this.fieldDao = fieldDao;
    }

    public Task<IEnumerable<Field>> GetAsync(int OwnerId)
    {
        return fieldDao.GetFieldsByOwnerId(OwnerId);
    }
}