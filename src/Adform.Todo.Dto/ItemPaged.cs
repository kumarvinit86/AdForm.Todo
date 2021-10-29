using Adform.Todo.Model.Models;
using System.Collections.Generic;

namespace Adform.Todo.Dto
{
    public class ItemPaged<TDto>
    {
        public List<TDto> data { get; set; }
        public PagingDataResponse pagingData { get; set; }
    }
}
