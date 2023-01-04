namespace FixWithCustomSerialization
{
    public static class ConfigurationHelper
    {
        public static IConfiguration config;
        public static void Initialize(IConfiguration Configuration)
        {
            config = Configuration;
        }

    }
    public static class Extension
    {
        public static string SaveImage(this string base64img, string outputImgFilename )
        {
            var fileName = outputImgFilename.AppendTimeStamp();
            var folderPath = Path.Combine(WebRootPath, "");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            File.WriteAllBytes(Path.Combine(folderPath, fileName), Convert.FromBase64String(base64img));
            return fileName;
        }
        
        public static string WebRootPath
        {
            get
            {
                return Directory.GetCurrentDirectory() + "\\wwwroot";
            }
        }
        public static string ApiUrl { get { return ConfigurationHelper.config.GetSection("MongoDB:ApiUrl").Value; } }

        public static string AppendTimeStamp(this string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Path.GetExtension(fileName)
                );
        }
    }
}
