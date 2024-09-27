using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Models;

public class Seed
{
    public static async Task SeedData(UrlDataContext context)
    {
        if (await context.Urls.AnyAsync()) return;

        var userDate = await File.ReadAllTextAsync("Data/Seed/SeedData.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        };

        var urls = JsonSerializer.Deserialize<List<Url>>(userDate);
        
        foreach ( var url in urls ) 
        {
            context.Urls.Add(url);
            await context.SaveChangesAsync();
        }
    }
}