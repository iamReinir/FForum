using MongoDB.Driver;

namespace forum.DataDecorator
{
    public abstract class DecoratedEntity
    {
        protected IMongoDatabase _database;
        public abstract void Save();
        public abstract void Retrieve();
    }
}
