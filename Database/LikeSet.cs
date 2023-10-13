using forum.Models;
using MongoDB.Driver;

namespace forum.Database
{
    public class LikeSet
    {
        private IMongoDatabase database = MongoDBConst.database;

        public LikeSet(IMongoDatabase db) => database = db;

        private int idCount = 1;       
        public Like? NewLike(User user, Post onpost)
        {
            var collection = database.GetCollection<Like>(MongoDBConst.COMMENT_TABLE);
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
    }
}
}
