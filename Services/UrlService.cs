using WebApplication1.Models;
using WebApplication1.Interfaces;

namespace WebApplication1.Services;

public class UrlService
{
    private readonly IUrlRepository _repository;

    public UrlService(IUrlRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(string originalUrl)
    {
        var shortUrl = GenerateShortUrl();
        var urlEntity = new Url { OriginalUrl = originalUrl, ShortUrl = shortUrl };
        await _repository.CreateAsync(urlEntity);
    }

    public async Task<Url> GetAsync(int id)
    {
        var urlEntity = await _repository.GetAsync(id);
        return urlEntity != null ? new Url { Id = urlEntity.Id, OriginalUrl = urlEntity.OriginalUrl, ShortUrl = urlEntity.ShortUrl } : null;
    }

    public async Task UpdateAsync(Url url)
    {
        var urlEntity = await _repository.GetAsync(url.Id);
        if (urlEntity != null)
        {
            urlEntity.OriginalUrl = url.OriginalUrl;
            urlEntity.ShortUrl = GenerateShortUrl();
            urlEntity.CreatedAt = DateTime.Now;
            await _repository.UpdateAsync(urlEntity);
        }
    }

    public async Task DeleteAsync(int? id)
    {
        await _repository.DeleteAsync(id);
    }
    
    public async Task<IEnumerable<Url>> GetAllAsync()
    {
        var urlEntities = await _repository.GetAllUrlsAsync();
        return urlEntities.Select(urlEntity => new Url
        {
            Id = urlEntity.Id,
            OriginalUrl = urlEntity.OriginalUrl,
            ShortUrl = urlEntity.ShortUrl,
            CreatedAt = urlEntity.CreatedAt,
            ClickCount = urlEntity.ClickCount
        });
    }

    private string GenerateShortUrl()
    {
        var chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        var random = new Random();
        var shortCode = new char[6];
        for (var i = 0; i < 6; i++)
        {
            shortCode[i] = chars[random.Next(chars.Length)];
        }
        return new string(shortCode);    }
}