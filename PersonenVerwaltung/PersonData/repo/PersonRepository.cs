using Microsoft.EntityFrameworkCore;
using PersonData.model;
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
            return entities.person.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Person entity)
        {
            entities.Update(entity);
            entities.SaveChanges();
        }

        /// <summary>
        /// Converts Person to Base Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static BasePerson CreateBasePerson(Person person)
        {
            var basePerson = new BasePerson()
            {
                Id = person.Id,
                name1 = person.name1,
                name2 = person.name2,
                date = person.date,
                CreatedAt = person.CreatedAt,
                ModifyAt = person.ModifyAt,
                ModifyDate = person.ModifyDate
            };
            return basePerson;
        }
    }
}