using GraphQL.Types;

namespace GraphQL.Relay.Contract
{
    internal interface IRelayNodeObject<out T>
    {
        T GetById(string partsId, ResolveFieldContext<object> context);
    }
}