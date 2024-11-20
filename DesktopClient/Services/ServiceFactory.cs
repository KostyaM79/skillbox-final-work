using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Services
{
    public static class ServiceFactory
    {
        private static readonly Dictionary<Type, Func<object>> services = new();

        public static void Add<T, TOut>() where TOut: T, new()
        {
            services.Add(typeof(T), () => new TOut());
        }

        public static T GetService<T>() where T: class
        {
            return services[typeof(T)].Invoke() as T;
        }
    }
}
