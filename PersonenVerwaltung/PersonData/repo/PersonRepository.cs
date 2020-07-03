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

        public PersonRepository(PersonEntities entities)
        {
            this.entities = entities;
        }

        public int Create(Person person)
        {
            entities.person.Add(person);
            return entities.SaveChanges();
        }

        public void DeleteOne(Person person)
        {
            entities.person.Remove(person);
            entities.SaveChanges();
        }

        public List<Person> FindAll()
        {            
         return entities.person
               .Include(x =>
                   x.addresses)
               .ThenInclude(x => x.address)
               .Include(x =>
                   x.contacts)
               .Include(x =>
                   x.comments).AsNoTracking()
               .ToList();
        }

        public Person FindOne(int id)
        {
            return entities.person.FirstOrDefault(x => x.id == id);
        }

        public void Update(Person entity)
        {
            entity.modifyAt = DateTime.Now; // Todoo: find out what was changed

            entities.Update(entity);
            entities.SaveChanges();
        }

       


        //public void GetPersons()
        //{
        //    //this methode must be called for entities fill the child list / parent class
        //    var x = entities.person
        //       .Include(x =>
        //           x.addresses)
        //       .ThenInclude(x => x.address)
        //       .Include(x =>
        //           x.contacts)
        //       .Include(x =>
        //           x.comments)
        //       .ToList();
        //    Console.WriteLine();
        //}
    }
}
