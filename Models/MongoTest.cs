using MongoDB.Driver;
using System;

namespace forum.Models
{
    public class MongoTest
    {
        public static void act()
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://group3:group3@cluster0.1rrhgmx.mongodb.net/?retryWrites=true&w=majority");
            var dbList = dbClient.ListDatabases().ToList();
            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }
        }
    }
}
