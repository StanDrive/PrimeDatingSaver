using System;
using System.Collections.Generic;
using System.Text;
using PrimeDating.BusinessLayer.Interfaces;
using PrimeDating.DataAccess.Interfaces;

namespace PrimeDating.BusinessLayer
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationData _authenticationData;

        private readonly ILogger _logger;

        public AuthenticationService(IAuthenticationData authenticationData, ILogger logger)
        {
            _authenticationData = authenticationData;

            _logger = logger;
        }

        public bool Login(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            var credentials = DecodeCredentials(token);

            var result = _authenticationData.Login(credentials.Login, EncryptPassword(credentials.Password));

            if (result)
            {
                _authenticationData.UpdateLoginDate(credentials.Login);
            }

            _logger.Trace($"AuthenticationService.Login [Login: {credentials.Login}, Result: {result}]");

            return result;
        }

        public void InvertUserLock(string login)
        {
            _authenticationData.InvertUserLock(login);
        }

        public void CreateUser(string login, string password, string description)
        {
            _authenticationData.CreateUser(login, EncryptPassword(password), description);
        }

        public List<string> GetAllUsers()
        {
            return _authenticationData.GetAllUsers();
        }

        public void UpdateUser(string login, string password, string description)
        {
            _authenticationData.UpdateUser(login, EncryptPassword(password), description);
        }

        private static string EncryptPassword(string credentialsPassword)
        {
            return new HashEncryptor().EncryptWithSHA1(credentialsPassword);
        }

        private Credentials DecodeCredentials(string token)
        {
            var data = Convert.FromBase64String(token);

            var decodedString = Encoding.UTF8.GetString(data);

            if (!decodedString.Contains(":"))
            {
                return null;
            }

            var login = decodedString.Substring(0,
                decodedString.IndexOf(":", StringComparison.InvariantCultureIgnoreCase)).ToLower();

            var password = decodedString.Replace(login + ":", string.Empty);

            return new Credentials {Login = login, Password = password};
        }
    }
}
