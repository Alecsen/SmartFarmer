namespace Domain.Models;

public class CropTypeImportance
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string CropType { get; set; }
    public bool Important { get; set; }

}