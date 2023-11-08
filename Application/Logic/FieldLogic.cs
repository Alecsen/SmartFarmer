using Application.DAOInterface;

namespace Application.Logic;

public class FieldLogic
{
    private readonly IFieldDao fieldDao;

    public FieldLogic(IFieldDao fieldDao)
    {
        this.fieldDao = fieldDao;
    }
}