using Adform.Todo.Dto;
using HotChocolate.Types;

namespace Adform.Todo.Api.GraphQl.Model
{
    /// <summary>
    /// TodoItem dto for GraphQL
    /// </summary>
    class ToDoItemType : ObjectType<Item>
    {
        protected override void Configure(IObjectTypeDescriptor<Item> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.Name).Type<StringType>();
            descriptor.Field(a => a.LabelName).Type<StringType>();

        }
    }
}
