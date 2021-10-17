using Adform.Todo.Dto;
using HotChocolate.Types;

namespace Adform.Todo.Api.GraphQl.Model
{
    /// <summary>
    /// Todolist dto for  GraphQL
    /// </summary>
    public class ToDoListType : ObjectType<ItemList>
    {
        protected override void Configure(IObjectTypeDescriptor<ItemList> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.Name).Type<StringType>();
            descriptor.Field(a => a.LabelName).Type<StringType>();
        }
    }
}
