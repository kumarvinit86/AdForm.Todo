using Adform.Todo.Dto;
using HotChocolate.Types;

namespace Adform.Todo.Api.GraphQl.Model
{
    /// <summary>
    /// TodoItem dto for GraphQL
    /// </summary>
    class ToDoItemType : ObjectType<Item>
    {
        protected override void Configure(IObjectTypeDescriptor<Item> item)
        {
            item.Field(a => a.Id).Type<IdType>();
            item.Field(a => a.Name).Type<StringType>();
            item.Field(a => a.LabelName).Type<StringType>();

        }
    }
}
