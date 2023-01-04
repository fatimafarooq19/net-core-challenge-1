using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FixWithCustomSerialization.Controllers;

[ApiController]
[Route("[controller]")]
public class MediaController : ControllerBase
{
    private readonly ILogger<MediaController> _logger;
    private readonly IDbService _db;
    public MediaController(ILogger<MediaController> logger, IDbService db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpPost("update")]
    public MediaFile UpdateMedia([FromBody] MediaFile media)
    {

        var getMedia = _db.SaveMedia(media);
        return getMedia;
        /* todo: Implement the method
         *          
         * 1. media will be contained in the ImageDataB64 prop. It should have been already saved to the webroots folder by the "read" method of MediaFileJsonConverter
         * 2. Save the media object in Mongo db
         * 3. return the MediaFile object
         * 4. We expect the "write" method of MediaFileJsonConverter to create the public URL
         * 
         * */
    }

    [HttpGet("{Name}")]
    public MediaFile GetMedia(string name)
    {
        var getMedia = _db.GetMedia(name);
        return getMedia;
        /*
         * return _mongoDb.getcollection<MediaFile>.Find(m=>m.Name==Name).SingleAsync();
         * 
         * 1. We expect the "write" method of MediaFileJsonConverter to create the public URL
         * 
         */
    }
}

/// <summary>
/// todo: implement MediaFileJsonConverter  
[JsonConverter(typeof(MediaFileJsonConverter))]
/// </summary>
public class MediaFile
{
    /// <summary>
    /// todo: We want media name to be Unique, Please enforce using MongoDb Unique Index
    /// </summary>
    public ObjectId _id { get; set; }
    public string Name { get; set; } = "";

    /// <summary>
    /// todo: donot Save in database
    /// </summary>

    public string ImageDataB64 { get; set; } = "";

    /// <summary>
    /// todo: donot Save in database, Generate dynamically using 
    /// </summary>   
    public string PublicUrl { get; set; } = "";


}



