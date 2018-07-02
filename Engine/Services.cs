using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Engine
{
    /// A global toolbox of services and variables.
    public static class Services
    {
        static List<object> services = new List<object>();

        static Services()
        {
            AddDefaultServices();
        }

        public static T Get<T>() where T : class
        {
            return services
                .Select(s => s as T)
                .Where(s => s != null)
                .FirstOrDefault();
        }

        public static void Add<T>(T service)
        {
            Assert.That(services, Has.No.AssignableTo<T>(), $"Services already contains a {typeof(T)}");

            services.Add(service);
        }

        public static void Remove<T>(T service) where T : class
        {
            Assert.That(service, Has.Member(service));

            services.Remove(service);
        }

        private static void AddDefaultServices()
        {
            Add(new Random());
        }
    }
}
