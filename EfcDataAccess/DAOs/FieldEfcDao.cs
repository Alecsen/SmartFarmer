using Application.DAOInterface;
using Domain.Models;

namespace EfcDataAccess.DAOs;

public class FieldEfcDao : IFieldDao
{
    
    
    
    
    public IEnumerable<Field> GetFieldsByOwnerId(int ownerId)
    {
        throw new NotImplementedException();
    }
}