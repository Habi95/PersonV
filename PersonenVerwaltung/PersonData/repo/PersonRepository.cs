using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    public class PersonRepository : Repository<Person>
    {
       
        private PersonEntities entities;

        public PersonRepository(PersonEntities entities1)
        {
            this.entities = entities1;
        }

        public int create(Person person)
        {
            entities.person.Add(person);
            return entities.SaveChanges();
        }

        public void GetPersons()
        {
            //this methode must be called for entities fill the child list / parent class
            entities.person
               .Include(x =>
                   x.addresses)
               .ThenInclude(x => x.address)
               .Include(x =>
                   x.contacts)
               .Include(x =>
                   x.comments)
               .ToList();
            //entities.address
            //    .Include(x =>
            //        x.,)
            //    .ToList();

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
