using Adform.Todo.Dto;
using HotChocolate.Types;

namespace Adform.Todo.GraphQl.Model
{
    /// <summary>
    /// Todolist dto for  GraphQL
    /// </summary>
    public class ToDoListType : ObjectType<ItemList>
    {
    }
}
