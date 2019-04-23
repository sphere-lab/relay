using GraphQL.Types;

namespace GraphQL.Relay.Contract
{
    public interface IRelayNodeObject<out T>
    {
        T GetById(string partsId, ResolveFieldContext<object> context);
    }
}