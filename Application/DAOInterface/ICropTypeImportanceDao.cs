using Domain.Models;

namespace Application.DAOInterface;

public interface ICropTypeImportanceDao
{
    Task<CropTypeImportance> GetCropTypeByOwnerId(int ownerId);
}