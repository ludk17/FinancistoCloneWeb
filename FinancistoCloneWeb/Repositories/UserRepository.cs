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
    }

    public class UserRepository : IUserRepository
    {
        private readonly FinancistoContext context;

        public UserRepository(FinancistoContext context)
        {
            this.context = context;
        }

        public User FindUser(string username, string password)
        {
            return context.Users
               .Where(o => o.Username == username && o.Password == password)
               .FirstOrDefault();
        }
    }
}
