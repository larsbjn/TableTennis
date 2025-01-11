namespace ByteArray;
public class FileUploadModel
{
    public byte[] Data { get; set; }  // Base64 string will be automatically converted to byte[] by ASP.NET Core
    public string FileName { get; set; }
    public string Description { get; set; }
}
