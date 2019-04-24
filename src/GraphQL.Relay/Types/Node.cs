using System;
using System.Linq;
using System.Text.RegularExpressions;
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

        public static bool TryParseFromGlobalId(string globalIdString, out GlobalId globalId)
        {
            globalId = null;

            if (IsBase64String(globalIdString))
            {
                var parts = StringUtils.Base64Decode(globalIdString).Split(':');
                if (parts.Length > 1)
                {
                    globalId = new GlobalId()
                    {
                        Type = parts[0],
                        Id = parts[1]
                    };
                }
            }

            return globalId != null;
        }

        private static bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        public static GlobalId FromGlobalId(string globalId)
        {
            var parts = StringUtils.Base64Decode(globalId).Split(':');
            return new GlobalId
            {
                Type = parts[0],
                Id = string.Join(":", parts.Skip(count: 1)),
            };
        }
    }
}