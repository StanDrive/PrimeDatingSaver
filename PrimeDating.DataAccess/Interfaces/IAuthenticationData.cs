using System.Collections.Generic;

namespace PrimeDating.DataAccess.Interfaces
{
    public interface IAuthenticationData
    {
        /// <summary>
        /// Logins the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <returns></returns>
        bool Login(string login, string encryptedPassword);

        /// <summary>
        /// Updates the login date.
        /// </summary>
        /// <param name="login">The login.</param>
        void UpdateLoginDate(string login);

        /// <summary>
        /// Inverts the user lock.
        /// </summary>
        /// <param name="login">The login.</param>
        void InvertUserLock(string login);

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <param name="description">The description.</param>
        void CreateUser(string login, string encryptedPassword, string description);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        List<string> GetAllUsers();

        /// <summary>
        /// Updates the password.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="encryptedPassword">The encrypted password.</param>
        /// <param name="description">The description.</param>
        void UpdateUser(string login, string encryptedPassword, string description);
    }
}
