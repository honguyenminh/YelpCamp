namespace YelpCamp.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Campground
{
    [BsonId, BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string Title { get; set; }

    public int Price { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }
}