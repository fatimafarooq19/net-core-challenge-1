using FixWithCustomSerialization.Controllers;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace FixWithCustomSerialization
{
    public interface IMongoService
    {
        MongoCollection<MediaFile> Medias { get; }
    }
    public class MongoService : IMongoService
    {
        private readonly MongoDatabase _db;
        public MongoService(MongoDBConfig config)
        {
            //"mongodb://localhost:27017"
            var client = new MongoClient(config.ConnectionString);
            MongoServer server = client.GetServer();
            _db = server.GetDatabase("MediaFileDb");
            Medias = _db.GetCollection<MediaFile>("MediaFile");
            Medias.CreateIndex(new IndexKeysBuilder()
                .Ascending("Name"), IndexOptions.SetUnique(true));
        }
        public MongoCollection<MediaFile> Medias { get; set; }
    }
    public class ServerConfig
    {
        public MongoDBConfig MongoDB { get; set; } = new MongoDBConfig();
    }
    public class MongoDBConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                    return $@"mongodb://{Host}:{Port}";
                return $@"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
    }
}
