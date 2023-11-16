namespace Domain.DTOs;

public class FieldCreationDto
{
    public int OwnerId { get; }
    public string FieldName { get; }

    public FieldCreationDto(int ownerId, string fieldName)
    {
        OwnerId = ownerId;
        FieldName = fieldName;
    }
}