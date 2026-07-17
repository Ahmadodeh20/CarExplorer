namespace CarExplorer.Models.DTOs
{
    public class ApiResponse<T>
    {
        public int Count { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<T> Results { get; set; } = [];
    }
}
