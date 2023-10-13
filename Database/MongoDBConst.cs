using forum.Models;
using MongoDB.Driver;

namespace forum.Database
{
    static public class MongoDBConst
    {
        public const string connection_string = "mongodb+srv://group3:group3@cluster0.1rrhgmx.mongodb.net/?retryWrites=true&w=majority";
        public static MongoClient dbClient;
        public const string dbName = "fforum_db";
        public static IMongoDatabase database;
        public const string USER_TABLE = "user";
        public const string USER_INFO_TABLE = "user_info";
        public const string POST_TABLE = "post";
        public const string POST_INFO_TABLE = "post_info";
        public const string LIKE_TABLE = "like";
        public const string COMMENT_TABLE = "comment";
        static MongoDBConst()
        {
            Console.WriteLine("Connect to database...");
            while (database == null)
            { 
                try {
                    dbClient = new MongoClient(connection_string);
                    database = dbClient.GetDatabase(dbName);
                }
                catch { Console.WriteLine("Connect failed. Retry..."); }
            }
            Console.WriteLine("Connect success!");
        }


    }
}
