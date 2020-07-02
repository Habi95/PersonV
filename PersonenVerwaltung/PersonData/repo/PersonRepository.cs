using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    public class PersonRepository : Repository<Person>
    {
        PersonEntities entities = new PersonEntities();

        public int create(Person person)
        {
            entities.person.Add(person);
            return entities.SaveChanges();
        }

        public void deleteOne(Person person)
        {
            entities.person.Remove(person);
            entities.SaveChanges();
        }

        public List<Person> findAll()
        {
            return entities.person.ToList();
            //return entities.person.Include(x => x.addresses).Include(x => x.contacts).Include(x => x.comments).ToList();
        }

        public Person findOne(int id)
        {
            return entities.person.FirstOrDefault(x => x.id == id);
        }

        public int update(Person person)
        {
            entities.person.Update(person);
            return entities.SaveChanges();
        }
    }
}
