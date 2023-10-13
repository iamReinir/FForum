using forum.Database;
using forum.Models;
using MongoDB.Driver;

namespace forum.DataDecorator
{
    public class DecoratedComment : DecoratedEntity
    {
        public Comment? Core { get; set; }
        public DecoratedComment(Comment core)
        {
            Core=core;
            this._database = MongoDBConst.database;            
        }
        public DecoratedComment()
        {
            this._database = MongoDBConst.database;
        }
        public User? GetUser()
        {
            try
            {
                var collection = _database
                    .GetCollection<User>(MongoDBConst.USER_TABLE)
                    .Find(user => user.ID == Core.User_id)
                    .ToList();
                return collection.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }        


        public Post? GetPost()
        {
            try
            {
                var queryResult = _database
                    .GetCollection<Post>(MongoDBConst.POST_TABLE)
                    .Find(post => post.ID == Core.Post_id)
                    .ToList();
                return queryResult.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
        public override void Retrieve()
        {
            try
            {    
                var queryResult = _database
                    .GetCollection<Comment>(MongoDBConst.COMMENT_TABLE).Find(comment => comment.Id == Core.Id)
                    .ToList();
                Core = queryResult.FirstOrDefault();           
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public override void Save()
        {
            try
            {
                var collection = _database
                    .GetCollection<Comment>(MongoDBConst.COMMENT_TABLE);
                collection?.ReplaceOne(comment => comment.Id == Core.Id, Core);                                    
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Delete()
        {
            try
            {
                Core.Is_hidden = true;
                Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
