using Adform.Todo.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adform.Todo.Manager.PageManager
{
    public interface IPageManager<Tmodel>
    {
        PagingDataResponse pagingResponse { get; set; }
        PagingDataRequest pagingRequest { get; set; }
        List<Tmodel> Paging(List<Tmodel> data);
    }
}
