using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PrimeDating.BusinessLayer;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.Models;
using PrimeDatingSaver.Filters;

namespace PrimeDatingSaver.Controllers
{
    /// <summary>
    /// AuthenticationController
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/authentication")]
    [BasicAuthenticationFilter]
    public class AuthenticationController : ApiController
    {
        private readonly ILogger _logger;

        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="authenticationService">The authentication service.</param>
        public AuthenticationController(ILogger logger, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            _logger = logger;
        }

        /// <summary>
        /// Block or unlock user.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("BlockUnlock/{login}")]
        public HttpResponseMessage BlockUnlockUser(string login)
        {
            _logger.Debug($"AuthenticationController.BlockUnlockUser [Login: {login}]");

            try
            {
                _authenticationService.InvertUserLock(login);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="PrimeDatingException">
        /// User model is null
        /// or
        /// User login is null or empty
        /// or
        /// User login can't be bigger than 50 symbols
        /// or
        /// User password is null or empty
        /// or
        /// User password can't be bigger than 50 symbols
        /// or
        /// User description is null or empty
        /// or
        /// User description can't be bigger than 500 symbols
        /// </exception>
        [HttpPost]
        [Route("CreateUser")]
        public HttpResponseMessage CreateUser(UserDto user)
        {
            _logger.Debug($"AuthenticationController.CreateUser [Login: {user?.Login}]");

            try
            {
                if (user == null)
                {
                    throw new PrimeDatingException("User model is null");
                }

                if (string.IsNullOrWhiteSpace(user.Login))
                {
                    throw new PrimeDatingException("User login is null or empty");
                }

                if (user.Login.Length > 50)
                {
                    throw new PrimeDatingException("User login can't be bigger than 50 symbols");
                }

                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    throw new PrimeDatingException("User password is null or empty");
                }

                if (user.Password.Length > 50)
                {
                    throw new PrimeDatingException("User password can't be bigger than 50 symbols");
                }

                if (string.IsNullOrWhiteSpace(user.Description))
                {
                    throw new PrimeDatingException("User description is null or empty");
                }

                if (user.Description.Length > 500)
                {
                    throw new PrimeDatingException("User description can't be bigger than 500 symbols");
                }

                _authenticationService.CreateUser(user.Login, user.Password, user.Description);

                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllUsers")]
        public HttpResponseMessage GetAllUsers()
        {
            _logger.Debug($"AuthenticationController.GetAllUsers");

            try
            {
                var result = _authenticationService.GetAllUsers();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }

        /// <summary>
        /// Generates the random password.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GenerateRandomPassword/{length}")]
        public HttpResponseMessage GenerateRandomPassword(int length)
        {
            _logger.Debug("AuthenticationController.GenerateRandomPassword");

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, SecurityHelper.GeneratePassword(length));
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="PrimeDatingException">
        /// User model is null
        /// or
        /// User login is null or empty
        /// or
        /// User password is null or empty
        /// or
        /// User password can't be bigger than 50 symbols
        /// or
        /// User description is null or empty
        /// or
        /// User description can't be bigger than 500 symbols
        /// </exception>
        [HttpPut]
        [Route("UpdateUser")]
        public HttpResponseMessage UpdateUser(UserDto user)
        {
            _logger.Debug($"AuthenticationController.UpdateUser [Login: {user?.Login}]");

            try
            {
                if (user == null)
                {
                    throw new PrimeDatingException("User model is null");
                }

                if (string.IsNullOrWhiteSpace(user.Login))
                {
                    throw new PrimeDatingException("User login is null or empty");
                }

                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    throw new PrimeDatingException("User password is null or empty");
                }

                if (user.Password.Length > 50)
                {
                    throw new PrimeDatingException("User password can't be bigger than 50 symbols");
                }

                if (string.IsNullOrWhiteSpace(user.Description))
                {
                    throw new PrimeDatingException("User description is null or empty");
                }

                if (user.Description.Length > 500)
                {
                    throw new PrimeDatingException("User description can't be bigger than 500 symbols");
                }

                _authenticationService.UpdateUser(user.Login, user.Password, user.Description);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.ErrorException(ex.Message, ex);

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.GetErrorMessage());
            }
        }
    }
}
