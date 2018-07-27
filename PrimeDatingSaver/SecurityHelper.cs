using System.Text.RegularExpressions;

namespace PrimeDatingSaver
{
    /// <summary>
    /// SecurityHelper
    /// </summary>
    public class SecurityHelper
    {
        /// <summary>
        /// The password container pattern
        /// </summary>
        public const string PasswordPrefix = "Password:";
        
        private const int PasswordNonAlphaNumericSymbolsCount = 4;

        /// <summary>
        /// Generates the password.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>System.String.</returns>
        public static string GeneratePassword(int length)
        {
            return System.Web.Security.Membership.GeneratePassword(length, PasswordNonAlphaNumericSymbolsCount);
        }

        /// <summary>
        /// Gets the password with prefix.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string GetPasswordWithPrefix(string password) => $"{PasswordPrefix}{password}";
    }
}
