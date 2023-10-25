using forum.Models;
using MongoDB.Driver;
using SixLabors.ImageSharp.Formats.Jpeg;

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

        public static async Task<byte[]> ResizeImageTo10MBAsync(IFormFile avatar)
        {
            // Threshold is 10MB
            const long desiredSize = 2L * 1024 * 1024;

            using var memoryStream = new MemoryStream();
            await avatar.CopyToAsync(memoryStream);

            // If the image is already less than 10MB, return it as is
            if (memoryStream.Length < desiredSize)
            {
                return memoryStream.ToArray();
            }

            // Else, use ImageSharp to resize the image
            using var image = Image.Load(memoryStream.ToArray());
            var resizeFactor = Math.Sqrt((double)desiredSize / memoryStream.Length);
            var newWidth = (int)(image.Width * resizeFactor);
            var newHeight = (int)(image.Height * resizeFactor);

            // Resize the image
            image.Mutate(x => x.Resize(newWidth, newHeight));

            using var resultStream = new MemoryStream();
            await image.SaveAsync(resultStream, new JpegEncoder());

            return resultStream.ToArray();
        }
    }
}
