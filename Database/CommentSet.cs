using MongoDB.Driver;
using forum.Models;
using System.Collections.ObjectModel;

namespace forum.Database
{

    public class CommentSet
    {
        private int usableID()
        {
            var all = Builders<Comment>.Filter.Empty;
            int res = 0;
            res = (int)MongoDBConst.database
                .GetCollection<Comment>(MongoDBConst.COMMENT_TABLE)
                .CountDocuments(all);
            return res + 1;
        }


        public Comment NewComment(User username, int postid)
        {
            int newid = usableID();            
            var comment = new Comment(newid, username, postid);
            MongoDBConst.database
                .GetCollection<Comment>(MongoDBConst.COMMENT_TABLE)
                .InsertOne(comment);
            return comment;
        }
        public ICollection<Comment> GetComments(int postid)
        {
           
            var filter = Builders<Comment>.Filter.Eq("PostId",postid);
            var result = new List<Comment>();
            var collection = MongoDBConst.database
                .GetCollection<Comment>(MongoDBConst.COMMENT_TABLE);
            result = collection
                .Find(filter).ToList();
            return result;
        }

        public void UpdateComment(Comment comment)
        {
            var collection = MongoDBConst.database
                .GetCollection<Comment>(MongoDBConst.COMMENT_TABLE);
            var thisCmt = Builders<Comment>.Filter.Eq("_id", comment.Id);
            collection.ReplaceOne(thisCmt, comment);
        }
    }
}
    /* {
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
                User = user,
                PostId = onpost.ID,
                Content = ""                
            };            
            collection.InsertOne(comment);                
            return comment;
        }       
    }
    */

