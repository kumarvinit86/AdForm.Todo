using Adform.Todo.Dto;
using HotChocolate.Types;

namespace Adform.Todo.Api.GraphQl.Model
{
    /// <summary>
    /// Label dto for  GraphQL
    /// </summary>
    public class LabelType : ObjectType<Label>
    {
        protected override void Configure(IObjectTypeDescriptor<Label> label)
        {
            base.Configure(label);
            label.Field(a => a.Id).Type<IdType>();
            label.Field(a => a.Name).Type<StringType>();
        }
    }
}
