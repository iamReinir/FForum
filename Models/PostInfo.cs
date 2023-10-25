using MongoDB.Bson.Serialization.Attributes;

namespace forum.Models
{
    public class PostInfo
    {
        public string? Content { get; set; } = "";        
        public DateTime Last_update { get; set; } = DateTime.Now;

        public string? Base64String { get; set; } = "";

        public PostInfo() { }
        public PostInfo(string content) {  
            Content = content;
        }
    }
}
