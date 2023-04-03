

namespace Crud.Example.Domain.Events
{
    public static class DomainEvent
    {
        [ThreadStatic]
        private static List<Delegate>? _actions;

        public static IServiceProvider? serviceProvider { get; set; }


        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            _actions ??= new List<Delegate>();
            _actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            _actions = null;
        }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            if (serviceProvider != null)
            {
                var handlerEvents = serviceProvider
                    .GetService(typeof(IEnumerable<IDomainHandler<T>>)) as IEnumerable<IDomainHandler<T>>;
                if (handlerEvents != null)
                {
                    //Fetch all handler of this type from the IoC container and invoke their handle method.
                    foreach (IDomainHandler<T>? handler in handlerEvents!)
                    {
                        handler.Handle(args);
                    }
                }
            }

            if (_actions != null)
            {
                foreach (var action in _actions)
                {
                    if (action is not Action<T>)
                    {
                        continue;
                    }
                    ((Action<T>)action)(args);
                }
            }
        }
    }
}