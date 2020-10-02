using FinancistoCloneWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancistoCloneWeb.Repositories
{
    public interface IPersonRepository
    {
        public List<Person> GetAll();
    }

    public class PersonRepository : IPersonRepository
    {
        private readonly FinancistoContext context;

        public PersonRepository(FinancistoContext context)
        {
            this.context = context;
        }

        public List<Person> GetAll()
        {
            return context.People.ToList();
        }
    }
}
