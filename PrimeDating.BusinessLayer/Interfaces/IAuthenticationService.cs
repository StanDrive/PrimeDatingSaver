using System.Collections.Generic;

namespace PrimeDating.BusinessLayer.Interfaces
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Logins the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        bool Login(string token);

        /// <summary>
        /// Inverts the user lock.
        /// </summary>
        /// <param name="login">The login.</param>
        void InvertUserLock(string login);

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <param name="description">The description.</param>
        void CreateUser(string login, string password, string description);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        List<string> GetAllUsers();

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <param name="description">The description.</param>
        void UpdateUser(string login, string password, string description);
    }
}
