using System;
using System.Collections.Generic;
using System.Linq;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Models.Database;

namespace PrimeDating.DataAccess
{
    internal class AuthenticationData : IAuthenticationData
    {
        public bool Login(string login, string encryptedPassword)
        {
            using (var context = new PrimeDatingContext())
            {
                return context.Users.Any(t => t.Login == login && t.Password == encryptedPassword && !t.IsBlock);
            }
        }

        public void UpdateLoginDate(string login)
        {
            using (var context = new PrimeDatingContext())
            {
                var user = context.Users.FirstOrDefault(t => t.Login == login);

                if (user == null)
                {
                    return;
                }

                user.LastLogin = DateTime.Now;

                context.SaveChanges();
            }
        }

        public void InvertUserLock(string login)
        {
            using (var context = new PrimeDatingContext())
            {
                var user = context.Users.FirstOrDefault(t => t.Login == login);

                if (user == null)
                {
                    return;
                }

                user.IsBlock = !user.IsBlock;

                context.SaveChanges();
            }
        }

        public void CreateUser(string login, string encryptedPassword, string description)
        {
            using (var context = new PrimeDatingContext())
            {
                var user = context.Users.FirstOrDefault(t => t.Login == login);

                if (user != null)
                {
                    throw new ApplicationException($"User '{login}' already exist");
                }

                var newUser = new Users
                {
                    Description = description,
                    IsBlock = false,
                    Login = login,
                    Password = encryptedPassword
                };

                context.Users.Add(newUser);

                context.SaveChanges();
            }
        }

        public List<string> GetAllUsers()
        {
            using (var context = new PrimeDatingContext())
            {
                return context.Users.Select(t => t.Login).OrderBy(t => t).ToList();
            }
        }

        public void UpdateUser(string login, string encryptedPassword, string description)
        {
            using (var context = new PrimeDatingContext())
            {
                var user = context.Users.FirstOrDefault(t => t.Login == login);

                if (user == null)
                {
                    return;
                }

                user.Password = encryptedPassword;

                user.Description = description;

                context.SaveChanges();
            }
        }
    }
}
