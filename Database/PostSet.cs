using forum.Models;
using MongoDB.Driver;

namespace forum.Database
{
    public class PostSet
    {
        private int usableID()
        {
            int res = 0;
            MongoDB.database
                .GetCollection<Post>(MongoDB.POST_TABLE)
                .Find(all => true)
                .ToList()
                .ForEach((post) => { res = Math.Max(res, post.ID); });
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
            var np = new Post(newid, user.ID);
            var pinfo = new PostInfo(newid, "");
            MongoDB.database
                .GetCollection<Post>(MongoDB.POST_TABLE)
                .InsertOne(np);
            MongoDB.database
                .GetCollection<PostInfo>(MongoDB.POST_INFO_TABLE)
                .InsertOne(pinfo);
            return np;
        }

        /// <summary>
        /// Get all posts owned by this user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A List of Post.</returns>
        public ICollection<Post> GetPosts()
        {
            var result = new List<Post>();
            result = MongoDB.database
                .GetCollection<Post>(MongoDB.POST_TABLE)
                .Find(all => true).ToList();                
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search_string"></param>
        /// <returns>A List of Post relevant to the search string</returns>
        public ICollection<Post> FindPost(string search_string)
        {                           
            var have_search_string = Builders<PostInfo>.Filter.AnyStringIn(search_string);
            List<int> result_id = new();
            MongoDB.database
                .GetCollection<PostInfo>(MongoDB.POST_INFO_TABLE)
                .Find(have_search_string)
                .ToList()
                .ForEach((info) =>
                {
                    result_id.Add(info.ID);
                });
            return MongoDB.database
                .GetCollection<Post>(MongoDB.POST_TABLE)
                .Find((post) => result_id.Contains(post.ID))
                .ToList();
        }


        /// <summary>
        /// Hide the post. Equivalent to delete (minus the actual deletion)
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public bool HidePost(Post post)
        {
            post.Is_hidden = true;
            MongoDB.database.GetCollection<Post>(MongoDB.POST_TABLE)
                .ReplaceOne(cur => post.ID == cur.ID, post);
            return true;
        }

        /// <summary>
        /// Save the Post to the database. 
        /// Only effective if there is a Post with the same ID in the database.
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public bool UpdatePost(Post post)
        {
            MongoDB.database.GetCollection<Post>(MongoDB.POST_TABLE)
                .ReplaceOne(cur => post.ID == cur.ID, post);
            return true;
        }

        /// <summary>
        /// Save the PostInfo to the database.
        /// Only effective if there is a PostInfo with the same ID in the database.
        /// </summary>
        /// <param name="postInfo"></param>
        /// <returns></returns>
        public bool UpdatePostInfo(PostInfo postInfo)
        {
            MongoDB.database
                .GetCollection<PostInfo>(MongoDB.POST_INFO_TABLE)
                .ReplaceOne((pinfo) => pinfo.ID == postInfo.ID, postInfo);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        /// <returns>The PostInfo owned by this post</returns>
        public PostInfo? GetPostInfo(Post post)
        {
            var result = MongoDB.database
                .GetCollection<PostInfo>(MongoDB.POST_INFO_TABLE)
                .Find(postinfo => post.ID == postinfo.ID).ToList();
            if (result.Count != 1)
                return null;
            return result[0];
        }
    }
}
