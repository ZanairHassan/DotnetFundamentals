namespace RepositoryPattern.ViewModels
{
    public class ErrorModel
    {
        public string? RequestId { get; set; }
        public string Path { get; set; }

        public int StatusCode { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public bool ShowPath => !string.IsNullOrEmpty(Path);
    }
}
