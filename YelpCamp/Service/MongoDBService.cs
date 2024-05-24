using Microsoft.Extensions.Options;
using MongoDB.Driver;
using YelpCamp.Models;

namespace YelpCamp.Service;

public class MongoDbService
{
    public MongoClient Client { get; }
    public IMongoDatabase Database { get; }

    public MongoDbService(IOptions<MongoDbSetting> setting, IConfiguration config, ILogger<CampgroundsService> logger)
    {
        string? connectionString = config["CampgroundDatabase:ConnectionString"];

        if (connectionString is null)
        {
            logger.LogCritical("Missing connection string for MongoDB. " +
                               "Either add a connection string to UserSecrets if on development or " +
                               "add an environment variable if on production. " +
                               "Read the README.md file for more info");
            throw new InvalidOperationException("No connection string for MongoDB found");
        }
        // TODO: add readme :)
        Client = new MongoClient(connectionString);
        logger.Log(LogLevel.Information, "Connected to MongoDB");
        // TODO: add additional logging here
        Database = Client.GetDatabase(setting.Value.DatabaseName);
    }
}