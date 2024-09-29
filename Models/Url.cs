using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Url
{
    [Key]
    public int Id { get; set; }
    
    [RegularExpression(@"^(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]*[-A-Za-z0-9+&@#/%=~_|]$", ErrorMessage = "Invalid URL")]
    public string OriginalUrl { get; set; }
    public string ShortUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int ClickCount { get; set; }
}