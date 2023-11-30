using Application.DAOInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class FieldEfcDao : IFieldDao
{

    private readonly SmartFarmerAppContext context;

    public FieldEfcDao(SmartFarmerAppContext context)
    {
        this.context = context;
    }


    public async Task<IEnumerable<FieldLookupDto>> GetFieldsByOwnerId(int ownerId)
    {
        // This will retrieve all Field objects with the matching ownerId
        var fields = await context.Fields
            .Where(field => field.Owner.Id == ownerId)
            .ToListAsync();
        
        List<FieldLookupDto> result = new List<FieldLookupDto>();

        foreach (Field field in fields)
        {
            FieldLookupDto dto = new FieldLookupDto();
            dto.Id = field.Id;
            dto.FieldName = field.Name;
            dto.locationData = field.LocationData;
            dto.CropType = field.CropType;
            
            result.Add(dto);
        }
        
        return result;
    }

    public async Task<Field> CreateAsync(Field field)
    {
        EntityEntry<Field> newField = await context.Fields.AddAsync(field);
        await context.SaveChangesAsync();
        return newField.Entity;
    }

    public async Task<Field> GetFieldById(int fieldId)
    {
        var field = await context.Fields
            .Where(field => field.Id == fieldId)
            .ToListAsync();

        if (!field.Any())
        {
            throw new Exception($"There is not any fields with the id of {fieldId}");
        }
        
        if (field.Count > 1)
        {
            throw new Exception($"There are multiple fields containing the same ID of {fieldId}");
        }

        Field returnField = field[0];
        Console.WriteLine($"this is what fields contain {returnField}");

        return returnField;
    }
}