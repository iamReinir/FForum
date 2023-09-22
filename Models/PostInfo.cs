namespace forum.Models
{
    public class PostInfo
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime Last_update { get; } = DateTime.Now;
        public Post Post { get; set; }
    }
}
