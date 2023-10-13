namespace forum.Models
{
    public class Like
    {
        public int User_id { get; set; }
        public int Post_id { get; set; }
        public DateTime Create_date { get; set; } = DateTime.Now;
        
    }
}
