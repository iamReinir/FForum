namespace forum.Models
{
    public class Post
    {
        public int ID { get; set; }
        public User Poster { get; }
        public bool Is_hidden {  get; set; } = false;

        public PostInfo info { get; }
        public DateTime Last_update { get; } = DateTime.Now;

        public DateTime Create_date { get; } = DateTime.Now;

    }
}
