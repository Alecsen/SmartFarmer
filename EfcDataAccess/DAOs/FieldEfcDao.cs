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

        if (!fields.Any())
        {
            throw new Exception($"No fields found for owner with ID {ownerId}.");
        }

        List<FieldLookupDto> result = new List<FieldLookupDto>();

        foreach (Field field in fields)
        {
            FieldLookupDto dto = new FieldLookupDto();
            dto.Id = field.Id;
            dto.FieldName = field.Name;
            
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
}