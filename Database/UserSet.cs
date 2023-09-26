﻿using forum.Models;
using MongoDB.Driver;

namespace forum.Database
{
    static public class MongoDB
    {
        public const string connection_string = "mongodb+srv://group3:group3@cluster0.1rrhgmx.mongodb.net/?retryWrites=true&w=majority";
        public static MongoClient dbClient = new MongoClient(connection_string);
        public const string dbName = "fforum_db";
        public static IMongoDatabase database = dbClient.GetDatabase(MongoDB.dbName);
        public const string USER_TABLE = "user";
        public const string USER_INFO_TABLE = "user_info";
	}
    public class UserSet
    {
        public static ICollection<User> Userlist { get; private set; } = new List<User>();
        public static ICollection<UserInfo> UserInfos { get; private set; } = new List<UserInfo>();

        private bool init = false;
        public UserSet() {
            if(!init)
                Initiation();
            init = true;
        }
        public void Initiation()
        {            
   //         var collection = MongoDB.database.GetCollection<User>(MongoDB.USER_TABLE);
   //         var collection2 = MongoDB.database.GetCollection<UserInfo>(MongoDB.USER_INFO_TABLE);
			//var filter = Builders<User>.Filter.Empty;
			//var filter2 = Builders<UserInfo>.Filter.Empty;
   //         Userlist = collection.Find(filter).ToList();
   //         UserInfos = collection2.Find(filter2).ToList();
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
				MongoDB.database
                    .GetCollection<UserInfo>(MongoDB.USER_INFO_TABLE)
                    .InsertOneAsync(uinfo)
                    .Wait();
				MongoDB.database
                    .GetCollection<User>(MongoDB.USER_TABLE)
                    .InsertOneAsync(user)
                    .Wait();
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
            var result =
                MongoDB.database
                .GetCollection<User>(MongoDB.USER_TABLE)
                .Find(User => username == User.Username)
                .ToList();
            if(result.Count != 1)
                return null;
            return result[0];
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <returns> Return a User object with this <code>id</code>. 
		/// Return null if cannot find user. </returns>
		public User? GetUser(int id)
        {
            var result =
                MongoDB.database
                .GetCollection<User>(MongoDB.USER_TABLE)
                .Find(User => id == User.ID)
                .ToList();
            if (result.Count != 1)
                return null;
            return result[0];
        }

        public bool UpdateUserInfo(UserInfo userInfo)
        {
            var uinfos = MongoDB.database.GetCollection<UserInfo>(MongoDB.USER_INFO_TABLE);
            uinfos.ReplaceOne(info => info.ID == userInfo.ID, userInfo);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A UserInfo object associate with this <code>user</code> </returns>
        public UserInfo? GetUserInfo(User user)
        {
            var result =
                MongoDB.database
                    .GetCollection<UserInfo>(MongoDB.USER_INFO_TABLE)
                    .Find(info => info.ID == user.ID).ToList();
            if(result.Count != 1)
                return null;
            return result[0];
        }

        public bool ResetPassword(string username, string newPassword)
        {
            User? cur = GetUser(username);
            if(cur == null) return false;
            cur.Password = newPassword;
            MongoDB.database
                .GetCollection<User>(MongoDB.USER_TABLE)
                .ReplaceOne(user => user.Username == username, cur);
            return true;
        }

    }
}
