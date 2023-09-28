using MongoDB.Bson.Serialization.Attributes;

namespace forum.Models
{
    public class PostInfo
    {
        [BsonElement("_id")]
        public int ID { get; set; }

        [BsonElement("Content")]
        private string? content;
        public string Content { 
            get {
                return content;
            } 
            set {
                Last_update = DateTime.Now;
                content = value;
            } 
        }
        public DateTime Last_update { get; set; } = DateTime.Now;        

        public PostInfo() { }
        public PostInfo(int id, string content) {
            ID = id;
            Content = content;
        }
    }
}
