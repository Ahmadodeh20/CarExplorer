namespace CarExplorer.Models.DTOs
{
    public class PaginationResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
    }
}
