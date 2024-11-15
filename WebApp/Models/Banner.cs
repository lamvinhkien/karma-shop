namespace WebApp.Models;

public class Banner
{
    public int BannerId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageURL { get; set; } = null!;
}