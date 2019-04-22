using System;
using System.Linq;
using Panic.StringUtils;

namespace GraphQL.Relay.Types
{
    public static class Node
    {
        public static NodeGraphType<TSource, TOut> For<TSource, TOut>(Func<string, TOut> getById)
        {
            var type = new DefaultNodeGraphType<TSource, TOut>(getById);
            return type;
        }

        public static string ToGlobalId(string name, object id)
        {
            return StringUtils.Base64Encode("{0}:{1}".ToFormat(name, id));
        }

        public static GlobalId FromGlobalId(string globalId)
        {
            var parts = StringUtils.Base64Decode(globalId).Split(':');
            return new GlobalId {
                Type = parts[0],
                Id = string.Join(":", parts.Skip(count: 1)),
            };
        }
    }
}