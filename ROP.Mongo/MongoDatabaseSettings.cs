namespace ROP.Mongo
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}