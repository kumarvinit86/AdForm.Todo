namespace Adform.Todo.Model.Models
{
    public class PagingDataRequest
    {
        const int MAXPAGESIZE = 20;
        public int pageNumber { get; set; } = 1;

        private int _pageSize { get; set; } = 1;

        public int pageSize
        {

            get { return _pageSize; }
            set
            {
                _pageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : value;
            }
        }
    }
}
