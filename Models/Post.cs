using MongoDB.Bson.Serialization.Attributes;

namespace forum.Models
{
    public class Post
    {
        [BsonElement("_id")]
        public int ID { get; set; }
        public int Poster_id { get; set; }
        public bool Is_hidden {  get; set; } = false;
        
        public DateTime Last_update { get; set; } = DateTime.Now;
        public DateTime Create_date { get; set; } = DateTime.Now;

        public Post() { }
        public Post(int id, int poster_id)
        {
            ID = id;
            Poster_id = poster_id;
        }

    }
}
