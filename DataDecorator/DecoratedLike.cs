using forum.Models;
using forum.Database;
using MongoDB.Driver;

namespace forum.DataDecorator
{
    public class DecoratedLike : DecoratedEntity
    {
        public Like Core { get; set; }
        public DecoratedLike(Like core) { 
            Core = core;
            this._database = MongoDBConst.database;
        }

        public DecoratedLike(Like code, IMongoDatabase database)
        {
            this.Core = code;
            this._database = database;
        }

        public User? GetUser()
        {
            try
            {
                var database = this._database;
                var result = database.GetCollection<User>(MongoDBConst.USER_TABLE)
                    .Find((user) => user.ID == Core.User_id)
                    .ToList();
                if (result.Count != 1)
                    return null;
                return result[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
        }
        public override async void Save()
        {            
            var collection = this._database.GetCollection<Like>(MongoDBConst.LIKE_TABLE);
            var queryResult = 
                collection
                    .Find(like => like.User_id == Core.User_id && like.Post_id == Core.Post_id)
                    .ToList();
            if (queryResult.Count == 0)
                await collection.InsertOneAsync(Core);            
        }

        public override void Retrieve()
        {
            try
            {
                var collection = this._database.GetCollection<Like>(MongoDBConst.LIKE_TABLE);
                var queryResult = collection
                    .Find(like => like.User_id == Core.User_id && like.Post_id == Core.Post_id)
                    .ToList();
                if (queryResult.Count != 1)
                    throw new Exception("No Object found on database");
                Core = queryResult[0];
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
