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
        public const string USER_TABLE = "user";
        public const string USER_INFO_TABLE = "user_info";
        public const string POST_TABLE = "post";
        public const string POST_INFO_TABLE = "post_info";
    }
}
