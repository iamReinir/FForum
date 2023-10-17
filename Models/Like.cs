using MongoDB.Bson.Serialization.Attributes;

namespace forum.Models
{
    [BsonIgnoreExtraElements]
    public class Like
    {
        public string Username { get; set; }
        public int Post_id { get; set; }
        public DateTime Create_date { get; set; } = DateTime.Now;
        
    }
}
