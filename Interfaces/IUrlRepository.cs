using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IUrlRepository
{
    Task<List<Url>> GetAllUrlsAsync();
    Task<Url?> GetAsync(int? id);
    Task<Url?> GetUrlByShortUrlAsync(string shortUrl);
    Task CreateAsync(Url url);
    Task UpdateAsync(Url url);
    Task DeleteAsync(int? id);
}