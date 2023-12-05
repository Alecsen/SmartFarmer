using Application.DAOInterface;
using Domain.DTOs;
using Domain.Models;

namespace EfcDataAccess.DAOs;

public class CropTypeImportanceEfcDao : ICropTypeImportanceDao
{
    public Task<CropTypeImportance> GetCropTypeByOwnerId(int ownerId)
    {
        throw new NotImplementedException();
    }

    public Task<CropTypeImportance> CreateCropType(CropTypeImportance cropTypeImportance)
    {
        throw new NotImplementedException();
    }

    public Task<CropTypeImportance> UpdateCropTypeImportance(CropTypeImportanceUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}