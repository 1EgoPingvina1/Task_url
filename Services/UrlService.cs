using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Interfaces;

namespace WebApplication1.Services;

//Этот сервис можно заменить если нам вдруг нужен будет Mediatr
public class UrlService
{
    private readonly IUrlRepository _repository;
    public UrlService(IUrlRepository repository) => _repository = repository;
    public async Task CreateAsync(string originalUrl)
    {
        var shortUrl = GenerateShortUrl(originalUrl);
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
            urlEntity.ShortUrl = GenerateShortUrl(urlEntity.OriginalUrl);
            urlEntity.CreatedAt = DateTime.Now;
            await _repository.UpdateAsync(urlEntity);
        }
    }

    public async Task DeleteAsync(int? id) => await _repository.DeleteAsync(id);
    
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

    private static string GenerateShortUrl(string originalUrl)
    {
        var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(originalUrl);

        var hashBytes = sha256.ComputeHash(bytes);

        var base64String = Convert.ToBase64String(hashBytes);

        var shortCode = new string(base64String.Where(c => char.IsLetterOrDigit(c)).Take(6).ToArray());

        return shortCode;   
    }

    public async Task<IActionResult> RedirectToOriginalUrl(string shortUrl)
    {
        var url = await _repository.GetUrlByShortUrlAsync(shortUrl);
        if (url != null)
        {
            url.ClickCount++;
            await _repository.UpdateAsync(url);
            return new RedirectResult(url.OriginalUrl);
        }
        return new NotFoundResult();
    }
}