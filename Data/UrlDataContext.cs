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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("Server=localhost,3306;Database=shorter_url;User=myuser;Password=mypassword;", new MariaDbServerVersion(new Version(10, 3)));
    }
}