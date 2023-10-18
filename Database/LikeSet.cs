using forum.Models;
using MongoDB.Driver;

namespace forum.Database
{
    public class LikeSet
    {
        private IMongoDatabase database = MongoDBConst.database;

        public LikeSet(IMongoDatabase db) => database = db;

        public LikeSet() { }
        public Like? ToggleLike(string username, int post_id)
        {
            var collection = database.GetCollection<Like>(MongoDBConst.LIKE_TABLE);
            var thisLike = Builders<Like>.Filter
                .Where(like => like.Username == username && like.Post_id == post_id);
            var like = new Like
            {
                Username = username,
                Post_id = post_id
            };

            var check = collection
                .Find(thisLike)
                .ToList().FirstOrDefault();
            if (check == null)
                collection.InsertOne(like);
            if (check != null)
                collection.DeleteMany(thisLike); // Race condition
            return like;
        }

        public bool HasLiked(string username, int post_id)
        {
            var thisLike = Builders<Like>
                .Filter.Where(like => like.Username == username && like.Post_id == post_id);
            return database.GetCollection<Like>(MongoDBConst.LIKE_TABLE)
                .Find(thisLike).FirstOrDefault() != null;
        }

        public List<(Post, bool)> FilterLiked(string? username, List<Post> post_list) {
            var result = new List<(Post, bool)>();

            if (username == null)
            {
                post_list.ForEach(post => result.Add((post, false)));
                return result;
            }
            
            
            var post_ids = new List<int>();
            post_list.ForEach(post => { post_ids.Add(post.Id); });
            var inList = Builders<Like>.Filter.In("Post_id", post_ids)
                | Builders<Like>.Filter.Eq("Username", username);
            var liked_posts = database.GetCollection<Like>(MongoDBConst.LIKE_TABLE).Find(inList)
                .ToList();
            Comparer<Post> x;
            for (int ilist = 0, iliked = 0;
                ilist < post_list.Count || iliked < liked_posts.Count;
                ++ilist)
            {
                bool liked = false;
                if (post_list[ilist].Id == liked_posts[iliked].Post_id)
                {
                    liked = true;
                    ++iliked;
                }
                result.Add((post_list[ilist],liked));
            }
            return result;
        }

        /*
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
        }*/
    }

}
