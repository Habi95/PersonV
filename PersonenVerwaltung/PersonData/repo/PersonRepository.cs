using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    public class PersonRepository : Repository<Person>
    {
        PersonEntities entities;
        public PersonRepository(PersonEntities entities)
        {
            this.entities = entities;
        }
        public List<Person> GetPersons()
        {

            return entities.person.Include(x => x.addresses).ToList();
        }
    }
}
