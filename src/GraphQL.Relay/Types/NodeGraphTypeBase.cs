using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using GraphQL.Types;
using Panic.StringUtils;

namespace GraphQL.Relay.Types
{
    public abstract class NodeGraphTypeBase<T> : ObjectGraphType<T>
    {
        protected NodeGraphTypeBase()
        {
            Interface<NodeInterface>();
        }

        public FieldType Id<TReturnType>(Expression<Func<T, TReturnType>> expression)
        {
            string name = null;
            try
            {
                name = StringUtils.ToCamelCase(expression.NameOf());
            }
            catch
            {
            }

            return Id(name, expression);
        }

        public FieldType Id<TReturnType>(
            string name,
            Expression<Func<T, TReturnType>> expression)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                // if there is a field called "ID" on the object, namespace it to "contactId"
                if (name.ToLower() == "id")
                {
                    if (string.IsNullOrWhiteSpace(Name))
                        throw new InvalidOperationException(
                            "The parent GraphQL type must define a Name before declaring the Id field " +
                            "in order to properly prefix the local id field");

                    name = StringUtils.ToCamelCase(Name + "Id");
                }

                Field(name, expression)
                    .Description($"The Id of the {Name ?? "node"}")
                    .FieldType.Metadata["RelayLocalIdField"] = true;
            }

            var field = Field(
                name: "id",
                description: $"The Global Id of the {Name ?? "node"}",
                type: typeof(NonNullGraphType<IdGraphType>),
                resolve: context => Node.ToGlobalId(
                    context.ParentType.Name,
                    expression.Compile()(context.Source)
                )
            );

            field.Metadata["RelayGlobalIdField"] = true;

            if (!string.IsNullOrWhiteSpace(name))
                field.Metadata["RelayRelatedLocalIdField"] = name;

            return field;
        }
    }
}