using Adform.Todo.Dto;
using HotChocolate.Types;

namespace Adform.Todo.GraphQl.Model
{
    /// <summary>
    /// TodoItem dto for GraphQL
    /// </summary>
    class ToDoItemType : ObjectType<Item>
    {
    }
}
