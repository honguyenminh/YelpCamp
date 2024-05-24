using Microsoft.Extensions.Options;
using MongoDB.Driver;
using YelpCamp.Models;

namespace YelpCamp.Service;

public class CampgroundsService
{
    private readonly IMongoCollection<Campground> _booksCollection;
    
    public CampgroundsService(IOptions<MongoDbSetting> setting, ILogger<CampgroundsService> logger, MongoDbService db)
    {
        // TODO: add additional logging here
        _booksCollection = db.Database.GetCollection<Campground>(setting.Value.CollectionName);
    }
    
    public async Task<List<Campground>> GetAsync()
    {
        var res = await _booksCollection.FindAsync(_ => true);
        return await res.ToListAsync();
    }
    public async Task<Campground?> GetAsync(string id)
    {
        var res = await _booksCollection.FindAsync(x => x.Id == id);
        return await res.FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Campground newBook)
        => await _booksCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, Campground updated)
        => await _booksCollection.ReplaceOneAsync(x => x.Id == id, updated);

    public async Task RemoveAsync(string id)
        => await _booksCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<long> CountAsync(string id)
        => await _booksCollection.CountDocumentsAsync(x => x.Id == id);
}