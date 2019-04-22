using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GraphQL.Relay.Contract;
using GraphQL.Types.Relay;

namespace GraphQL.Relay.Types
{
    public abstract class NodeGraphType<T, TOut> : NodeGraphTypeBase<T>, IRelayNode<TOut>
    {
        public static Type Edge => typeof(EdgeType<NodeGraphType<T, TOut>>);

        public static Type Connection => typeof(ConnectionType<NodeGraphType<T, TOut>>);

        public abstract TOut GetById(string id);

    }

    public abstract class NodeGraphType<TSource> : NodeGraphType<TSource, TSource>
    {
    }

    public abstract class NodeGraphType : NodeGraphType<object>
    {
    }

    public abstract class AsyncNodeGraphType<T> : NodeGraphType<T, Task<T>>
    {
    }
    
    public class DefaultNodeGraphType<TSource, TOut> : NodeGraphType<TSource, TOut>
    {
        private readonly Func<string, TOut> _getById;

        public DefaultNodeGraphType(Func<string, TOut> getById)
        {
            _getById = getById;
        }

        public override TOut GetById(string id)
        {
            return _getById(id);
        }
    }
}
