using forum.Database;
using forum.Models;
using MongoDB.Driver;

namespace forum.DataDecorator
{
    /*
    public class DecoratedPost : DecoratedEntity
    {
        
        public Post? Core { get; set; }
        private PostInfo? info {  get; set; }
        private User? poster = null;
        public DecoratedPost() {
            _database = MongoDBConst.database;
        }

        public DecoratedPost(Post core)
        {
            _database = MongoDBConst.database;
            Core = core;            
        }

        public override async void Save()
        {
            try
            {
                FilterDefinition<Post> exactID = Builders<Post>.Filter.Eq("_id", Core.ID);
                await _database.GetCollection<Post>(MongoDBConst.POST_TABLE)
                    .ReplaceOneAsync(exactID, Core);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public override void Retrieve()
        {
            try
            {
                FilterDefinition<Post> exactID = Builders<Post>.Filter.Eq("_id", Core.ID);
                Core = _database.GetCollection<Post>(MongoDBConst.POST_TABLE).Find(exactID)
                    .ToList().SingleOrDefault();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
        }

        public PostInfo? GetPostInfo()
        {
            if (Core == null) return null;
            if(info != null)
                return info;
            info = _database.GetCollection<PostInfo>(MongoDBConst.POST_INFO_TABLE)
                .Find(pi=>pi.ID == Core.ID).ToList().SingleOrDefault();
            return info;
        }

        public User? GetUser()
        {
            if (poster != null)
                return poster;
            try
            {
                poster = _database.GetCollection<User>(MongoDBConst.USER_TABLE)
                    .Find(user => user.ID == Core.Poster_id)
                    .ToList().SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return poster;
        }

        public List<Comment> GetComments()
        {
            try
            {
                return _database.GetCollection<Comment>(MongoDBConst.COMMENT_TABLE)
                    .Find(comment => comment.Post_id == Core.ID)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }            
        }

        public void Hide()
        {
            if(Core != null)
                Core.Is_hidden = true;
        }

        public void LikeBy(User user)
        {
            if (Core == null) return;          
        }
        public int LikeCount()
        {
            FilterDefinition<Like> thisComment = Builders<Like>.Filter.Eq("post_id", Core.Id);
            return (int)(_database.GetCollection<Like>(MongoDBConst.LIKE_TABLE).CountDocuments(thisComment));
        }
    }
        */
}
