using System;
using GraphQL.Relay.Contract;
using GraphQL.Types;
using GraphQL.Types.Relay;

namespace GraphQL.Relay.Types
{
    public abstract class NodeObjectGraphType<T, TOut> : NodeGraphTypeBase<T>, IRelayNodeObject<TOut>
    {
        public static Type Edge => typeof(EdgeType<NodeObjectGraphType<T, TOut>>);

        public static Type Connection => typeof(ConnectionType<NodeObjectGraphType<T, TOut>>);

        public abstract TOut GetById(string partsId, ResolveFieldContext<object> context);
    }
}