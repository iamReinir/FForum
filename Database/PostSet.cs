using forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace forum.Database
{
    public class PostSet
    {
        private int usableID()
        {
            var all = Builders<Post>.Filter.Empty;
            int res = 0;
            res = (int)MongoDBConst.database
                .GetCollection<Post>(MongoDBConst.POST_TABLE)
                .CountDocuments(all);
            return res + 1;
        }

        /// <summary>
        /// Create a new post own by this user. Content inside PostInfo need to be change.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>the Post just created.</returns>
        public Post NewPost(User user)
        {
            int newid = usableID();
            var np = new Post(newid, user);
            MongoDBConst.database
                .GetCollection<Post>(MongoDBConst.POST_TABLE)
                .InsertOne(np);                
            return np;
        }

        /// <summary>
        /// Get all posts owned by this user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A List of Post.</returns>
        public ICollection<Post> GetPosts()
        {
            var all = Builders<Post>.Filter.Empty;
            var result = new List<Post>();
            var collection = MongoDBConst.database
                .GetCollection<Post>(MongoDBConst.POST_TABLE);
            result = collection
                .Find(all).ToList();                
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search_string"></param>
        /// <returns>A List of Post relevant to the search string</returns>
        public ICollection<Post> FindPost(string search_string)
        { 
            var posts = GetPosts();
            var res = new List<Post>();
            foreach(var post in posts)
            {
                if(post.Info.Content.IndexOf(search_string,StringComparison.OrdinalIgnoreCase) >= 0
                    && !post.Is_hidden)   
                    res.Add(post);
            }
            return res;
        }

        /// <summary>
        /// Each element is a tuple contain the post 
        /// and a boolean indicates whether the user liked this post
        /// </summary>
        /// <param name="search_string"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public ICollection<(Post,bool)> FindPost(string search_string, string? username)
        {
            var result = new List<(Post, bool)>();
            if (username==null)
            {
                foreach (var item in FindPost(search_string))            
                    result.Add((item, false));                
                return result;
            }
            
            var thisUser = Builders<Like>.Filter.Eq("Username", username);
            Func<int,FilterDefinition<Like>> likePost = 
                (post_id) => Builders<Like>.Filter.Eq("Post_id", post_id);  
            var like_collection = MongoDBConst.database.GetCollection<Like>(MongoDBConst.LIKE_TABLE);
            var allPost = FindPost(search_string);
            Func<int, bool> liked = (post_id) 
                => like_collection.Find(thisUser & likePost(post_id)).ToList()
                    .SingleOrDefault() != null; 
            foreach (var item in allPost)
            {
                if (liked(item.Id))                
                    result.Add((item, true));
                else result.Add((item, false));
            }
            return result;
        }

        public Post? FindPost(int id)
        {
            var thisID = Builders<Post>.Filter.Eq("_id", id);
            var result =  MongoDBConst.database
                .GetCollection<Post>(MongoDBConst.POST_TABLE)
                .Find(thisID).ToList().SingleOrDefault();
            return result;
        }


        /// <summary>
        /// Hide the post. Equivalent to delete (minus the actual deletion)
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public bool HidePost(Post post)
        {           
            post.Is_hidden = true;
            return UpdatePost(post);
        }

        /// <summary>
        /// Save the Post to the database. 
        /// Only effective if there is a Post with the same ID in the database.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public bool UpdatePost(Post post)
        {
            var thisPost = Builders<Post>.Filter.Eq("_id", post.Id);
            MongoDBConst.database.GetCollection<Post>(MongoDBConst.POST_TABLE)
                .ReplaceOne(thisPost, post);
            return true;
        }

        public void UpdateAllPostOfUser(User user)
        {
            var thisPost = Builders<Post>.Filter.Eq("Poster.Username", user.Username);
            var update = Builders<Post>.Update.Set("Poster", user);
            MongoDBConst.database
                .GetCollection<Post>(MongoDBConst.POST_TABLE).UpdateMany(thisPost,update);
        }
    }
}
