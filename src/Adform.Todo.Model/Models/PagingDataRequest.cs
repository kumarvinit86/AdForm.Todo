namespace Adform.Todo.Model.Models
{
    public class PagingDataRequest
    {
        const int MAXPAGESIZE = 20;
        public int PageNumber { get; set; } = 1;

        private int pageSize { get; set; } = 1;

        public int PageSize
        {

            get { return pageSize; }
            set
            {
                pageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : value;
            }
        }
    }
}
