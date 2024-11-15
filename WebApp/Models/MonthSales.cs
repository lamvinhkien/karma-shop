namespace WebApp.Models;

public class MonthSales
{
    public int Month { get; set; }
    public string MonthName { get; set; } = null!;
    public decimal Sales { get; set; }
}