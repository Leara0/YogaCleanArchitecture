namespace YogaApp.Domain.Entities;

public class Category
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? CategoryDescription { get; set; }
    public List<int> PosesInThisCategory { get; set; } 
}