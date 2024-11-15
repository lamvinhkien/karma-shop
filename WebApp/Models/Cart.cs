namespace WebApp.Models;

public class Cart
{
    public string CartCode { get; set; } = null!;
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public short Quantity { get; set; }
    public decimal Price { get; set; }

}