using Domain.DTOs;
using Domain.Models;

namespace Application.DAOInterface;

public interface ICropTypeImportanceDao
{
    //porpuse is to ne abæe to show a list of a users croptypes in the front end
    Task<CropTypeImportance> GetCropTypeByOwnerId(int userId);
    
    //Purpose, this should be used when a field is created or updated, no endpoint needed.
    Task<CropTypeImportance> CreateCropType(CropTypeImportance cropTypeImportance);

    //For the front-end to send a change in the importance level. 
    Task<CropTypeImportance> UpdateCropTypeImportance(CropTypeImportanceUpdateDto dto);
}