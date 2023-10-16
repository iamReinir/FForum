using MongoDB.Bson.Serialization.Attributes;

namespace forum.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {

        public static class ROLE
        {
            public const int ADMIN = 0;
            public const int MODERATOR = 1;
            public const int POSTER = 2;
            public const int GUESS = 3;
            public const int ROLE_COUNT = 4;
        }
       
        public string Username { get; set; }

        public string Password { get;set;}

        public int Role
        {
            get; set;
        } = ROLE.POSTER;
        public bool Is_banned { get; set; } = false;
        public DateTime Create_Date { get; set; } = DateTime.Now;
        public DateTime Last_update { get; set; } = DateTime.Now;
        public UserInfo UserInfo { get; set; } = new UserInfo();
        public void Update()
        {
            Last_update = DateTime.Now;
        }

        // For some reason User_Info and Posts are not saved into the database, will be fixed later
        // Will not save composite oject into the database. Using keys instead.
        //public UserInfo User_Info { get; }

        //public ICollection<Post> Posts {get; } = new List<Post>();

        // This is usuable
        /*
        public User(int id, string username, string password, UserInfo info) {
            this.ID = id;
            this.Username = username;
            this.Password = password;
            //this.User_Info = info;
        }
        */

        public User() { }
        public User(string username, string password)
        {
			this.Username = username;
			this.Password = password;
		}
    }
}
