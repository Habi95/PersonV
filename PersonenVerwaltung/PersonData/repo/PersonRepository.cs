using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    public class PersonRepository : IRepository<Person>
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
                   .ThenInclude(x =>
                       x.address)
              .Include(x =>
                  x.contacts)
              .Include(x =>
                  x.comments)
             .Include(x =>
                  x.courseParticipants)
                   .ThenInclude(x =>
                       x.Course)
              .Include(x =>
                   x.courseTrainers)
                       .ThenInclude(x =>
                           x.Course)
              .AsNoTracking()
              .ToList();           
        }

        public Person FindOne(int id)
        {
            return entities.person.FirstOrDefault(x => x.id == id);
        }

        public void Update(Person entity)
        {
            entities.Update(entity);
            entities.SaveChanges();           
        }

       
    }
}
