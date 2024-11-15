namespace WebApp.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string ImageURL { get; set; } = null!;
    public string BackgroundColor { get; set; } = null!;
}