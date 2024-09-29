using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly UrlDataContext _dbContext;
    public UrlRepository(UrlDataContext dbContext) => _dbContext = dbContext;
    
    
    public async Task<Url?> GetAsync(int? id) => await _dbContext.Urls.FirstOrDefaultAsync(x => x.Id == id);
    
    public async Task<Url?> GetUrlByShortUrlAsync(string shortUrl) => await _dbContext.Urls.SingleOrDefaultAsync(x => x.ShortUrl == shortUrl); 

    public async Task CreateAsync(Url url)
    {
        await _dbContext.Urls.AddAsync(url);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Url url)
    {
        url.ClickCount = 0;
        _dbContext.Urls.Update(url);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<List<Url>> GetAllUrlsAsync() => await _dbContext.Urls.ToListAsync();
    
    public async Task DeleteAsync(int? id)
    {
        var url = await GetAsync(id);
        if (url != null)
        {
            _dbContext.Urls.Remove(url);
            await _dbContext.SaveChangesAsync();
        }
    }
}