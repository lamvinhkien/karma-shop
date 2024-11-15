namespace WebApp.Models;

public class Invoice
{
    public string CartCode { get; set; } = null!;
    public decimal Amount { get; set; }
    public string InvoiceId { get; set; } = null!;
    public DateTime InvoiceDate { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Note { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PostCode { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
}