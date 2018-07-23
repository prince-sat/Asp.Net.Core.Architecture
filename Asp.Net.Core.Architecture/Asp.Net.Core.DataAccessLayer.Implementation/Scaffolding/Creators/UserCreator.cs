using Asp.Net.Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net.Core.DataAccessLayer.Implementation.Scaffolding.Creators
{
    /// <summary>
    /// Classe de création d'une entité User
    /// </summary>
    internal class UserCreator : BaseCreator
    {
        public List<User> Users { get; set; }
        public UserCreator(DbInitializerContext context) : base(context)
        {
            Users = new List<User>();
        }

        List<User> _users = new List<User>
        {
            new User() {
                 Email = "chsakells.blog@gmail.com",
                    Username = "chsakell",
                    HashedPassword = "9wsmLgYM5Gu4zA/BSpxK2GIBEWzqMPKs8wl2WDBzH/4=",
                    Salt = "GTtKxJA6xJuj3ifJtTXn9Q==",
                    IsLocked = false,
                    DateCreated = DateTime.Now
            }

        };
        public override void Create()
        {
            foreach (User user in _users)
            {
                CreateUser(user);
            }
        }

        public User CreateUser(User user)
        {
            User result = Context.UnitOfWork.UserRepository.FindBy(x => x.Username == user.Username).FirstOrDefault();
            if (result == null)
            {
                result = new User
                {
                    Email = "chsakells.blog@gmail.com",
                    Username = "chsakell",
                    HashedPassword = "9wsmLgYM5Gu4zA/BSpxK2GIBEWzqMPKs8wl2WDBzH/4=",
                    Salt = "GTtKxJA6xJuj3ifJtTXn9Q==",
                    IsLocked = false,
                    DateCreated = DateTime.Now
                };
                Context.UnitOfWork.UserRepository.Add(result);
                Context.UnitOfWork.Save();
                Context.Logger.Information("User {Username} ajouté", user.Username);

            }
            Users.Add(result);
            return result;
        }
    }
}
