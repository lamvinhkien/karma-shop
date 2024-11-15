namespace WebApp.Models;

public class CategorySales : Category
{
    public decimal Sales { get; set; }
    public string BackgroundColor { get; set; } = null!;
    public string HoverBackgroundColor { get; set; } = null!;
}