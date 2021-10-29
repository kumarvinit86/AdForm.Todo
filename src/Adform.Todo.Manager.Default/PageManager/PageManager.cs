using Adform.Todo.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adform.Todo.Manager.Default.PageManager
{
    public class PageManager<Tmodel>
    {
        public PagingDataResponse pagingResponse { get; set; }
        public PagingDataRequest pagingRequest { get; set; }

        public List<Tmodel> Paging(List<Tmodel> data)
        {
            pagingResponse = new PagingDataResponse();
            pagingResponse.TotalCount = data.Count;
            int PageSize = pagingRequest.PageSize;
            pagingResponse.TotalPages = (int)Math.Ceiling(data.Count / (double)pagingRequest.PageSize);
            List<Tmodel> pagedData = data.Skip((pagingRequest.PageNumber - 1) * PageSize).Take(PageSize).ToList();
            pagingResponse.PreviousPage = pagingRequest.PageNumber > 1 ? "Yes" : "No";
            pagingResponse.NextPage = pagingRequest.PageNumber < pagingResponse.TotalPages ? "Yes" : "No";
            return pagedData;
        }
    }
}
