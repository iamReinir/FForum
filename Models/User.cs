using MongoDB.Bson.Serialization.Attributes;

namespace forum.Models
{
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
        
        // Same key as UserInfo
        [BsonElement("_id")]
        public int ID { get; }

        private string username;
        public string Username { 
            get {
                return username;
            } 
            set {
                string oldName = username;
                username = value;
                if (!username.Equals(oldName))
                    Update();
            } 
        }

        private string password;
        public string Password {
            get {
                return password;
            } set {
                password = value;
                Update();
            } 
        }

        private int role = ROLE.POSTER;
        public int Role { 
            get {
                return role;
            } set { 
                int oldRole = role;
                role = value;
                if(role != oldRole)
                    Update();
            } 
        }

        private bool is_banned = false;
        public bool Is_banned { 
            get { return is_banned; } 
            set { if (is_banned != value)
                    Update();
                is_banned = value;
            } }
        public DateTime Last_update { get; private set; } = DateTime.Now;
        private void Update()
        {
            Last_update = DateTime.Now;
        }

        // For some reason User_Info and Posts are not saved into the database, will be fixed later
        // Will not save composite oject into the database. Using keys instead.
        //public UserInfo User_Info { get; }

        //public ICollection<Post> Posts {get; } = new List<Post>();

        // This is usuable
        public User(int id, string username, string password, UserInfo info) {
            this.ID = id;
            this.Username = username;
            this.Password = password;
            //this.User_Info = info;
        }

        public User(int id, string username, string password)
        {
			this.ID = id;
			this.Username = username;
			this.Password = password;
		}
    }
}
