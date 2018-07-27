using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using CommonServiceLocator;
using PrimeDating.BusinessLayer.Interfaces;

namespace PrimeDatingSaver.Filters
{
    /// <summary>
    /// BasicAuthenticationFilter
    /// </summary>
    /// <seealso cref="System.Web.Http.Filters.AuthorizationFilterAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthenticationFilter : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Override to Web API filter method to handle Basic Auth check
        /// </summary>
        /// <param name="actionContext">Контекст действия, инкапсулирующий сведения для использования объекта <see cref="T:System.Web.Http.Filters.AuthorizationFilterAttribute" />.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authenticationService = ServiceLocator.Current.GetInstance<IAuthenticationService>();

            var result = false;

            if (actionContext.Request.Headers.Authorization != null &&
                actionContext.Request.Headers.Authorization.Scheme == "Basic")
            {
                result = authenticationService.Login(actionContext.Request.Headers.Authorization.Parameter);
            }

            if (!result)
            {
                Challenge(actionContext);

                return;
            }

            base.OnAuthorization(actionContext);
        }

        /// <summary>
        /// Send the Authentication Challenge request
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        private static void Challenge(HttpActionContext actionContext)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

            actionContext.Response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{host}\"");
        }
    }
}