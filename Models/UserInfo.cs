using MongoDB.Bson.Serialization.Attributes;

namespace forum.Models
{
    public class UserInfo
    {

        public UserInfo()
        {
        }

        public string? Base64String { get; set; } = "";
        public string Name { set; get; }
        public string Email { set; get; } = "";
        public DateTime? Birthdate { set; get; }
        public string Telephone { set; get; } = "";
        public string Address { set; get; } = "";
        public DateTime Last_update { set; get; } = DateTime.Now;
        public DateTime Create_date { get; set; } = DateTime.Now;

        public void Update()
        {
            Last_update = DateTime.Now;
        }

        //private User? owner;
        //public User? User { init { owner = value; ID = owner?.ID ; } get { return owner; } }

    }
}
