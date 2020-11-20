using FinancistoCloneWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancistoCloneWeb.Repositories
{
    public interface IUserRepository
    {
        public User FindUser(string username, string password);
        User FindByUsername(string username);
        void Create(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly FinancistoContext context;

        public UserRepository(FinancistoContext context)
        {
            this.context = context;
        }

        public void Create(User user)
        {
            context.Add(user);
            context.SaveChanges();
        }

        public User FindByUsername(string username)
        {
            return context.Users.Where(o => o.Username == username).FirstOrDefault();
        }

        public User FindUser(string username, string password)
        {
            return context.Users
               .Where(o => o.Username == username && o.Password == password)
               .FirstOrDefault();
        }
    }
}
