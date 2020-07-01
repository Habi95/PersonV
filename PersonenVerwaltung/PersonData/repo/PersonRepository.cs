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
        }

        public Person findOne(int id)
        {
            return entities.person.FirstOrDefault(x => x.id == id);
        }
    }
}
