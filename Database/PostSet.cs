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

        public ICollection<Post> GetPosts(User user)
        {
            var result = new List<Post>();
            result = MongoDB.database
                .GetCollection<Post>(MongoDB.POST_TABLE)
                .Find(all => true).ToList();                
            return result;
        }
        
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

        public bool HidePost(Post post)
        {
            post.Is_hidden = true;
            MongoDB.database.GetCollection<Post>(MongoDB.POST_TABLE)
                .ReplaceOne(cur => post.ID == cur.ID, post);
            return true;
        }

        public bool UpdatePost(Post post)
        {
            MongoDB.database.GetCollection<Post>(MongoDB.POST_TABLE)
                .ReplaceOne(cur => post.ID == cur.ID, post);
            return true;
        }

        public bool UpdatePostInfo(PostInfo postInfo)
        {
            MongoDB.database
                .GetCollection<PostInfo>(MongoDB.POST_INFO_TABLE)
                .ReplaceOne((pinfo) => pinfo.ID == postInfo.ID, postInfo);
            return true;
        }

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
