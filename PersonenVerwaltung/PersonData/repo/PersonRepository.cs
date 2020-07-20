using Microsoft.EntityFrameworkCore;
using PersonData.model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PersonData.repo
{
    public class PersonRepository : BaseRepository<Person>
    {
        public PersonRepository(PersonEntities entities) : base(entities)
        {
        }

        public override List<Person> FindAll()
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
              .Include(x => x.user)
              .AsNoTracking()
              .ToList();
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

        public bool checkPerson(Person person, ContactRepository contactRepository)
        {
            var p = entities.person.Include(x => x.contacts).FirstOrDefault(x => x.date == person.date && x.name1 == person.name1 && x.name2 == person.name2);

            if (p == null)
            {
                return true;
            }
            else if (contactRepository.checkContactList(p.contacts, contactRepository.check(person.contacts)) == 0)
            {
                return true;
            }

            return false;
        }
    }
}