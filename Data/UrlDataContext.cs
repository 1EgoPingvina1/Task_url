using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class UrlDataContext : DbContext
{
    public UrlDataContext(DbContextOptions<UrlDataContext> options) : base(options)
    {
        
    }

    public UrlDataContext()
    {
        
    }
    public DbSet<Url> Urls { get; set; }


}