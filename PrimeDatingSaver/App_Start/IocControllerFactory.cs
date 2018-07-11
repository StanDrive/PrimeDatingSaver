using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;

namespace PrimeDatingSaver
{
    /// <summary>
    /// IocControllerFactory
    /// </summary>
    /// <seealso cref="System.Web.Mvc.DefaultControllerFactory" />
    public class IocControllerFactory : DefaultControllerFactory
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="IocControllerFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public IocControllerFactory(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Извлекает экземпляр контроллера для заданного контекста запроса и типа контроллера.
        /// </summary>
        /// <param name="requestContext">Контекст HTTP-запроса, включающий в себя контекст HTTP и данные маршрута.</param>
        /// <param name="controllerType">Тип контроллера.</param>
        /// <returns>
        /// Экземпляр контроллера.
        /// </returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
            {
                return _container.Resolve(controllerType) as IController;
            }

            try
            {
                return base.GetControllerInstance(requestContext, null);
            }
            catch (HttpException)
            {
                return null;
            }
        }
    }
}