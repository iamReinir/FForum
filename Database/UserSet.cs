using forum.Models;
using MongoDB.Driver;

namespace forum.Database
{
    static public class MongoDB
    {
        public const string connection_string = "mongodb+srv://group3:group3@cluster0.1rrhgmx.mongodb.net/?retryWrites=true&w=majority";
        public static MongoClient dbClient = new MongoClient(connection_string);
        public const string dbName = "fforum_db";
    }
    public class UserSet
    {
        public ICollection<User> Userlist { get; private set; } = new List<User>();

        public UserSet() {
            Initiation();
        }
        public void Initiation()
        {
            var db = MongoDB.dbClient.GetDatabase(MongoDB.dbName);
            var collection = db.GetCollection<User>("user");
            var filter = Builders<User>.Filter.Empty;
            Userlist = collection.Find(filter).ToListAsync().Result;
        }

        public void Save()
        {
            
        }

        public bool Register(string username, string password, UserInfo info)
        {
            try
            {
                
                User user = new User(Userlist.Count, username, password, info);
                Userlist.Add(user);

                var db = MongoDB.dbClient.GetDatabase(MongoDB.dbName);
                db.GetCollection<User>("user").InsertOneAsync(user);
                return true;
            }
            catch(Exception){
                return false;
            }
        }
        public User? GetUser(string username)
        {
            foreach (var user in Userlist)
            {
                if(user.Username == username) return user;
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
