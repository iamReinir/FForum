using MongoDB.Bson.Serialization.Attributes;

namespace forum.Models
{
    public class UserInfo
    {
        public UserInfo(int id, string displayName) {
            ID = id;
            Name = displayName;
        }

        [BsonElement("_id")]
        public int ID { init; get; }
        public string Name { set; get; }
        public string Email { set; get; } = "";
        public DateTime? Birthdate { set; get; }
        public string Telephone { set; get; } = "";
        public string Address { set; get; } = "";
        public DateTime Last_update { set; get; } = DateTime.Now;

        //private User? owner;
        //public User? User { init { owner = value; ID = owner?.ID ; } get { return owner; } }

    }
}
