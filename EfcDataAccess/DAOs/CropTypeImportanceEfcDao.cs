using Application.DAOInterface;
using Domain.Models;

namespace EfcDataAccess.DAOs;

public class CropTypeImportanceEfcDao : ICropTypeImportanceDao
{
    public Task<CropTypeImportance> GetCropTypeByOwnerId(int ownerId)
    {
        throw new NotImplementedException();
    }
}