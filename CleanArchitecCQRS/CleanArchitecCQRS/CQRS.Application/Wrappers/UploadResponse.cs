namespace EsuhaiHRM.Application.Wrappers
{
    public class UploadResponse
    {
        public string FullFilePath { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public UploadResponse(string fullFilePath, string message)
        {
            Succeeded = message == null ? true : false;
            Message = message;
            FullFilePath = fullFilePath;
        }
    }
}
