using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            //var e = entities.person;
            //e.Include(x => x.addresses).ThenInclude(x => x.address);
            //e.Include(x =>
            //        x.contacts);
            //e.Include(x =>
            //        x.comments);
            //e.Include(x =>
            //        x.courseParticipants)
            //            .ThenInclude(x =>
            //                x.Course);
            //e.Include(x =>
            //        x.courseTrainers)
            //            .ThenInclude(x =>
            //                x.Course);
            //e.Include(x =>
            //        x.book);
            //e.Include(x =>
            //        x.notebook);
            //e.Include(x =>
            //        x.equipment);

            //return e.ToList();

            return entities.person
              .Include(x =>
                    x.addresses)
                        .ThenInclude(x =>
                            x.address)
              .Include(x =>
                    x.contacts)
              .Include(x =>
                    x.comments)
              //.Include(x =>
              //      x.courseParticipants)
              //          .ThenInclude(x =>
              //              x.Course)
              //.Include(x =>
              //      x.courseTrainers)
              //          .ThenInclude(x =>
              //              x.Course)
              .Include(x =>
                    x.book)
              .Include(x =>
                    x.notebook)
              .Include(x =>
                    x.equipment)
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