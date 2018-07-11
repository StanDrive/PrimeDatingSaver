using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace PrimeDatingSaver
{
    /// <summary>
    /// UnityResolver
    /// </summary>
    /// <seealso cref="System.Web.Http.Dependencies.IDependencyResolver" />
    public class UnityResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityResolver"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <exception cref="ArgumentNullException">container</exception>
        public UnityResolver(IUnityContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        /// <summary>
        /// Открывает область разрешения.
        /// </summary>
        /// <returns>
        /// Область зависимостей.
        /// </returns>
        public IDependencyScope BeginScope()
        {
            var childContainer = _container.CreateChildContainer();

            return new UnityResolver(childContainer);
        }

        /// <summary>
        /// Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов.
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
        }

        /// <summary>
        /// Извлекает службу из области.
        /// </summary>
        /// <param name="serviceType">Извлекаемая служба.</param>
        /// <returns>
        /// Извлеченная служба.
        /// </returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        /// Извлекает коллекцию служб из области.
        /// </summary>
        /// <param name="serviceType">Коллекция извлекаемых служб.</param>
        /// <returns>
        /// Коллекция извлеченных служб.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }
    }
}