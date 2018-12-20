using System;
using HotChocolate.Utilities;

namespace HotChocolate.Execution
{
    public static class ClassMiddlewareFactory
    {
        internal static QueryMiddleware Create<TMiddleware>()
            where TMiddleware : class
        {
            return next =>
            {
                var factory = MiddlewareActivator
                    .CompileFactory<TMiddleware, QueryDelegate>();

                return CreateDelegate<TMiddleware>(
                    (s, n) => factory(s, n),
                    next);
            };
        }

        internal static QueryMiddleware Create<TMiddleware>(
            Func<IServiceProvider, QueryDelegate, TMiddleware> factory)
            where TMiddleware : class
        {
            return next => CreateDelegate<TMiddleware>(factory, next);
        }

        internal static QueryDelegate CreateDelegate<TMiddleware>(
            Func<IServiceProvider, QueryDelegate, TMiddleware> factory,
            QueryDelegate next)
            where TMiddleware : class
        {
            object sync = new object();
            TMiddleware middleware = null;

            var compiled = MiddlewareActivator
                .CompileMiddleware<TMiddleware, IQueryContext>();

            return context =>
            {
                if (middleware == null)
                {
                    lock (sync)
                    {
                        if (middleware == null)
                        {
                            middleware = factory(context.Services, next);
                        }
                    }
                }

                return compiled(context, context.Services, middleware);
            };
        }
    }
}
