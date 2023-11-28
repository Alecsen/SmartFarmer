﻿namespace Domain.Models;

public class Field
{
    public int Id { get; set;}
    public int OwnerId { get; set; }
    public User Owner { get; private set; }
    public string Name { get; set; }
    public string CropType { get; set; }
    public int? ImportanceLevel { get; set; }
    
    public string? LocationData { get; set; }
    
}