using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace ROP.Mongo
{
    public abstract class RepositoryBase
    {
        private static readonly string _sequenceCollectionName = "counters";
        static RepositoryBase()
        {
            var pack = new ConventionPack();
            pack.Add(new CamelCaseElementNameConvention());

            ConventionRegistry.Register("CamelCaseConventions", pack, x => true);
        }
        public RepositoryBase(IMongoDatabaseSettings settings)
        {
            Settings = settings;

            var client = new MongoClient(settings.ConnectionString);
            Database = client.GetDatabase(settings.DatabaseName);

            SequenceCollection = Database.GetCollection<BsonDocument>(_sequenceCollectionName);
        }
        protected IMongoDatabaseSettings Settings { get; set; }
        protected IMongoDatabase Database { get; set; }
        protected IMongoCollection<BsonDocument> SequenceCollection { get; set; }
    }
    public abstract class RepositoryBase<TModel, TKey> : RepositoryBase, ROP.Services.Repositories.IRepository<TModel, TKey>
        where TModel : Models.IModel<TKey>
    {
        public RepositoryBase(IMongoDatabaseSettings settings)
            : base(settings)
        {
            Collection = Database.GetCollection<TModel>(CollectionName);
        }

        protected abstract string CollectionName { get; }
        protected IMongoCollection<TModel> Collection { get; set; }

        public async Task<List<TModel>> GetAll()
        {
            var cursor = await Collection.FindAsync(FilterDefinition<TModel>.Empty);
            var list = await cursor.ToListAsync();
            return list;
        }
        public async Task<TKey> GetNextKey()
        {
            if (IsKeyNumericType())
            {
                var item = await SequenceCollection.FindOneAndUpdateAsync(
                      Builders<BsonDocument>.Filter.Eq("_id", CollectionName),
                      Builders<BsonDocument>.Update.Inc("value", 1),
                      new FindOneAndUpdateOptions<BsonDocument, BsonDocument>()
                      {
                          IsUpsert = true,
                          ReturnDocument = ReturnDocument.After,
                          Projection = Builders<BsonDocument>.Projection.Include("value")
                      }
                  );
                return (TKey)Convert.ChangeType(item.GetValue("value").ToInt64(), typeof(TKey));
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public async Task<TModel> GetByKey(TKey key)
        {
            var cursor = await Collection.FindAsync(GetKeyFilterDefinition(key));
            var item = await cursor.SingleOrDefaultAsync();
            return item;
        }
        public Task Create(TModel model)
        {
            return Collection.InsertOneAsync(model);
        }
        public Task Modify(TModel model)
        {
            return Collection.ReplaceOneAsync(GetKeyFilterDefinition(model.Id), model, new ReplaceOptions
            {
                IsUpsert = false
            });
        }
        public Task CreateOrModify(TModel model)
        {
            return Collection.ReplaceOneAsync(GetKeyFilterDefinition(model.Id), model, new ReplaceOptions
            {
                IsUpsert = true
            });
        }

        private FilterDefinition<TModel> GetKeyFilterDefinition(TKey key)
        {
            var builder = Builders<TModel>.Filter;
            var filter = builder.Eq(x => x.Id, key);
            return filter;
        }

        private static bool IsKeyNumericType()
        {
            var type = typeof(TKey);
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}