using FixWithCustomSerialization.Controllers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FixWithCustomSerialization
{

    public interface IDbService
    {
        MediaFile GetMedia(string name);
        MediaFile SaveMedia(MediaFile media);
    }
    public class DbService : IDbService
    {
        private readonly IMongoService _context;
        public DbService(IMongoService context)
        {
            _context = context;
        }
        public MediaFile GetMedia(string name)
        {
            return _context.Medias.AsQueryable().Single(m => m.Name == name);            
        }
        public MediaFile SaveMedia(MediaFile media)
        {
            try
            {
                _context.Medias.Insert(media);
            }
            catch(MongoDuplicateKeyException ex)
            {
                throw new ApplicationException("Duplicates are not allowed");
            }
            
            return media;
        }
    }
    
}
