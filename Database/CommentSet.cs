using MongoDB.Driver;
using forum.Models;
using System.Collections.ObjectModel;

namespace forum.Database
{
    /*
    public class CommentSet
    {
        private IMongoDatabase database = MongoDBConst.database;        

        public CommentSet(IMongoDatabase db) => database = db;

        private int idCount = 1;
        private int UsableID(IMongoCollection<Comment> coll)
        {
            coll.Find(all=>true)
                .ToList().ForEach(c => idCount = Math.Max(idCount, c.Id));
            return idCount + 1;
        }        
        public Comment? NewComment(User user, Post onpost)
        {
            var collection = database.GetCollection<Comment>(MongoDBConst.COMMENT_TABLE);
            var comment = new Comment
            {
                Id = UsableID(collection),
                User_id = user.ID,
                Post_id = onpost.ID,
                Content = ""                
            };            
            collection.InsertOne(comment);                
            return comment;
        }       
    }
    */
}
