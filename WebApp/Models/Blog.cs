namespace WebApp.Models;
public class Blog
{
    public int BlogId { get; set; }
    public string ImageURL { get; set; } = null!;
    public string BlogName { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string DatePost { get; set; } = null!;
    public string Views { get; set; } = null!;
    public int Comments { get; set; }
    public string Tag { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string DetailDescription { get; set; } = null!;
    public string DetailComments { get; set; } = null!;

}