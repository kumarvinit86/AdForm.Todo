using Adform.Todo.Dto;
using HotChocolate.Types;

namespace Adform.Todo.Api.GraphQl.Model
{
    /// <summary>
    /// Todolist dto for  GraphQL
    /// </summary>
    public class ToDoListType : ObjectType<ItemList>
    {
        protected override void Configure(IObjectTypeDescriptor<ItemList> item)
        {
            item.Field(a => a.Id).Type<IdType>();
            item.Field(a => a.Name).Type<StringType>();
            item.Field(a => a.LabelName).Type<StringType>();
        }
    }
}
