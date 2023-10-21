using forum.Models;
using MongoDB.Driver;
using System.Runtime.ConstrainedExecution;

namespace forum.Database
{
    public class UserSet
    {

        public User? Login(string username, string password)
        {
            var thisUser = Builders<User>.Filter.Eq("Username", username);
            var user = MongoDBConst.database
                .GetCollection<User>(MongoDBConst.USER_TABLE)
                .Find(thisUser)
                .ToList().SingleOrDefault();
            if (user == null) return null;
            if(Authenticator.Verify(user.Password, password))
            {
                return user;
            }
            return null;
        }
        /// <summary>
        /// Create a new user and save to the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns> An Integer which is the ID of the user. 
        /// Can be used to get UserInfo. Will return -1 if an error happen.</returns>
		public User? Register(string username, string password)
		{
			try
			{
                if(MongoDBConst.database
                    .GetCollection<User>(MongoDBConst.USER_TABLE)
                    .CountDocuments(user => user.Username == username) > 0)
                    return null;
                var salt = Authenticator.GenerateSalt();
				User user = new User();
                user.Username = username;
                var hashedPass = Authenticator.Hash(password, salt);
                user.Password = hashedPass;
				MongoDBConst.database
                    .GetCollection<User>(MongoDBConst.USER_TABLE)
                    .InsertOneAsync(user)
                    .Wait();
				return user;
			}
			catch (Exception)
			{
				return null;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns> Return a User object with this <code>username</code>. 
        /// Return null if cannot find user. </returns>
		public User? GetUser(string? username)
        {
            var thisUser = Builders<User>.Filter.Eq("Username", username);
            if (username == null)
                return null;
            return
                MongoDBConst.database
                .GetCollection<User>(MongoDBConst.USER_TABLE)
                .Find(thisUser)
                .ToList().SingleOrDefault();                    
        }

        public bool ResetPassword(string username, string newPassword)
        {
            var thisUser = Builders<User>.Filter.Eq("Username", username);
            User? cur = GetUser(username);
            if(cur == null) return false;            
            var salt = Authenticator.GenerateSalt();
            cur.Password = Authenticator.Hash(newPassword,salt);
            MongoDBConst.database
                .GetCollection<User>(MongoDBConst.USER_TABLE)
                .ReplaceOne(thisUser, cur);            
            return true;
        }

        public ICollection<User> GetUserList()
        {            
            return MongoDBConst.database
                .GetCollection<User>(MongoDBConst.USER_TABLE)
                .Find(Builders<User>.Filter.Empty)
                .ToList();
        }
        public void UpdateUser(User user)
        {
            var thisUser = Builders<User>.Filter.Eq("Username", user.Username);
            MongoDBConst.database.GetCollection<User>(MongoDBConst.USER_TABLE).ReplaceOne(thisUser, user);
        }

        public ICollection<User> FindUser(string? search_string)
        {
            var has_str = Builders<User>.Filter.Regex("Username", "/search_string/");
            return MongoDBConst.database
                .GetCollection<User>(MongoDBConst.USER_TABLE)
                .Find(has_str)
                .ToList();
            
        }
        public ICollection<User> FindUsers(string? search_string)
        {
            if (search_string == null)
                return GetUserList();
            return MongoDBConst.database
                .GetCollection<User>(MongoDBConst.USER_TABLE)
                .Find(user => user.Username.Contains(search_string))
                .ToList();
        }
    }
}
