namespace Adform.Todo.Model.Models
{
    public class PagingDataResponse
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string PreviousPage { get; set; }
        public string NextPage { get; set; }

    }
}
