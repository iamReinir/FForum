using forum.Models;
using MongoDB.Driver;

namespace forum.Database
{
    /*
    public class LikeSet
    {
        private IMongoDatabase database = MongoDBConst.database;

        public LikeSet(IMongoDatabase db) => database = db;

        public LikeSet()
        {
            database = MongoDBConst.database;
        }

        private int idCount = 1;       
        public Like? NewLike(User user, Post onpost)
        {
            var collection = database.GetCollection<Like>(MongoDBConst.LIKE_TABLE);
            var like = new Like
            {
                User_id = user.ID,
                Post_id = onpost.ID,
            };
            var check = collection
                .Find(like => like.User_id == user.ID && like.Post_id == onpost.ID)
                .ToList().SingleOrDefault();
            if (check == null)
                collection.InsertOne(like);
            return like;
        }

        public Like? NewLike(int user_id, int post_id)
        {
            var collection = database.GetCollection<Like>(MongoDBConst.LIKE_TABLE);
            var like = new Like
            {
                User_id = user_id,
                Post_id = post_id
            };
            var check = collection
                .Find(like => like.User_id == user_id && like.Post_id == post_id)
                .ToList().SingleOrDefault();
            if (check == null)
                collection.InsertOne(like);
            return like;
        }
    }
    */
}
