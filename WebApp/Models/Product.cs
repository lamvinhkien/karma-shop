namespace WebApp.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageURL { get; set; } = null!;
    public string Availability { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string DetailDescription { get; set; } = null!;
    public string Specification { get; set; } = null!;
    public string Comments { get; set; } = null!;
    public string Reviews { get; set; } = null!;
    public int CategoryId { get; set; }

}