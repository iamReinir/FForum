using MongoDB.Bson.Serialization.Attributes;

namespace forum.Models
{
    public class Comment
    {
        [BsonElement("_id")]
        public int Id { get; set; }
        public User User { get; set; } 
        public Post Post { get; set; }
        public string Content { get; set; }
        public DateTime Create_date { get; set; } = DateTime.Now;
        public DateTime Last_update { get; set; } = DateTime.Now;
        public bool Is_hidden { get; set; } = false;

    }
}
