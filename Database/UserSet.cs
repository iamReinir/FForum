using forum.Models;
using MongoDB.Driver;

namespace forum.Database
{
    static public class MongoDB
    {
        public const string connection_string = "mongodb+srv://group3:group3@cluster0.1rrhgmx.mongodb.net/?retryWrites=true&w=majority";
        public static MongoClient dbClient = new MongoClient(connection_string);
        public const string dbName = "fforum_db";
        public static IMongoDatabase database = dbClient.GetDatabase(MongoDB.dbName);
	}
    public class UserSet
    {
        public ICollection<User> Userlist { get; private set; } = new List<User>();
        public ICollection<UserInfo> UserInfos { get; private set; } = new List<UserInfo>();

        public UserSet() {
            Initiation();
        }
        public void Initiation()
        {            
            var collection = MongoDB.database.GetCollection<User>("user");
            var collection2 = MongoDB.database.GetCollection<UserInfo>("user_info");
			var filter = Builders<User>.Filter.Empty;
			var filter2 = Builders<UserInfo>.Filter.Empty;
            Userlist = collection.Find(filter).ToList();
            UserInfos = collection2.Find(filter2).ToList();
        }

        // Not used now. Might be used in the future.
        public void Save()
        {
            
        }


        // This function is unusable
        /*
        public bool Register(string username, string password, UserInfo info)
        {
            try
            {                
                User user = new User(Userlist.Count, username, password, info);
                Userlist.Add(user); 
                MongoDB.database.GetCollection<User>("user").InsertOneAsync(user);
                return true;
            }
            catch(Exception){
                return false;
            }
        }
        */

        /// <summary>
        /// Create a new user and save to the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns> An Integer which is the ID of the user. 
        /// Can be used to get UserInfo. Will return -1 if an error happen.</returns>
		public int Register(string username, string password)
		{
			try
			{
                int newId = Userlist.Count;
				User user = new User(newId, username, password);
                UserInfo uinfo = new UserInfo(newId, username);
				Userlist.Add(user);
                UserInfos.Add(uinfo);				
				MongoDB.database.GetCollection<UserInfo>("user_info").InsertOneAsync(uinfo).Wait();
				MongoDB.database.GetCollection<User>("user").InsertOneAsync(user).Wait();
				return newId;
			}
			catch (Exception)
			{
				return -1;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns> Return a User object with this <code>username</code>. 
        /// Return null if cannot find user. </returns>
		public User? GetUser(string username)
        {
            foreach (var user in Userlist)
            {
                if(user.Username == username) return user;
            }
            return null;
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <returns> Return a User object with this <code>id</code>. 
		/// Return null if cannot find user. </returns>
		public User? GetUser(int id)
        {
            foreach (var user in Userlist)
            {
                if (user.ID == id) return user;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A UserInfo object associate with this <code>user</code> </returns>
        public UserInfo? GetUserInfo(User user)
        {
            foreach (var userInfo in UserInfos)
            {
                if(userInfo.ID == user.ID) return userInfo;
            }
            return null;
        }

        public bool ResetPassword(string username, string newPassword)
        {
            User? cur = GetUser(username);
            if(cur == null) return false;
            cur.Password = newPassword;
            return true;
        }

    }
}
