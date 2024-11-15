namespace WebApp.Models;

public class InvoiceDetail
{
    public string InvoiceId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal InvoicePrice { get; set; }
    public string Name { get; set; }
    public decimal ProductPrice { get; set; }
    public string ImageURL { get; set; }
}
