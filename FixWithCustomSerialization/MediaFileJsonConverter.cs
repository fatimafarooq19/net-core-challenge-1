using FixWithCustomSerialization.Controllers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FixWithCustomSerialization
{
    public class MediaFileJsonConverter : JsonConverter<MediaFile>
    {       
        public override MediaFile Read(
           ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            var getBase64 = ""; 
            string? propertyName = "";
            var fileName = "";
            MediaFile file = new MediaFile();
            while (reader.Read())
            {               

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    reader.Read();
                    if(propertyName == "ImageDataB64")
                    {
                        getBase64 = reader.GetString();
                    }
                    if (propertyName == "Name")
                    {
                        fileName = reader.GetString();
                    }                   
                }
                
            }
            file.Name = getBase64.SaveImage(fileName);
            return file;
        }

        public override void Write(
             Utf8JsonWriter writer, MediaFile media, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (media is not null)
            {
                writer.WriteString("Name",  media.Name);
                writer.WriteString("PublicUrl", Extension.ApiUrl + media.Name);
            } 
            writer.WriteEndObject();
        }
    }
}
