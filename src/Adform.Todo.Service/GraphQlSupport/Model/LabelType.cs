using Adform.Todo.Dto;
using HotChocolate.Types;

namespace Adform.Todo.Service.GraphQlSupport.Model
{
    /// <summary>
    /// Label dto for  GraphQL
    /// </summary>
    public class LabelType : ObjectType<Lable>
    {
        protected override void Configure(IObjectTypeDescriptor<Lable> label)
        {
            base.Configure(label);
            label.Field(a => a.Id).Type<IdType>();
            label.Field(a => a.Name).Type<StringType>();
        }
    }
}
