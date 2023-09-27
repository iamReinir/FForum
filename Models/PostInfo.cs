namespace forum.Models
{
    public class PostInfo
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public DateTime Last_update { get; } = DateTime.Now;        

        public PostInfo(int id, string content) {
            ID = id;
            Content = content;
        }
    }
}
