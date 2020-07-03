using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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

        //public IList GetDocuments<T>(int id)
        //{
        //    $"select * from documents where class = {typeof(T).Name} and class_id = {id}";
        //}

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
                    x.documents)
               //.ThenInclude(x =>
               //     x.documentClass)
               //.ThenInclude(x =>
               //     x.documents)
               .AsNoTracking()
               .ToList();
        }

        public Person FindOne(int id)
        {
            return entities.person.FirstOrDefault(x => x.id == id);
        }

        public void Update(Person entity)
        {
            //entities.person.Update(entity);

            var exist = entities.person.Find(entity.id);

            for (int i = 0; i < exist.addresses.Count(); i++)
            {
                if (exist.addresses[i] != entity.addresses[i])
                {
                    //entities.person.Find(exist.id).addresses[i].address = entity.addresses[i].address;
                    exist.addresses[i].address = entity.addresses[i].address;
                }
            }

            //entities.Entry(exist).CurrentValues.SetValues(entity);
            entities.Update(exist);
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
