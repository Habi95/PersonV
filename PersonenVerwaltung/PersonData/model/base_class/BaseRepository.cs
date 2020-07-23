using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    /// <summary>
    /// This Repository is used as a Base Class for all other Repositorys
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> where T : BaseClassCreatedModify
    {
        public PersonEntities entities = new PersonEntities();

        public BaseRepository(PersonEntities Entities)
        {
            this.entities = Entities;
        }

        public virtual void Create(T toAdd)
        {
            entities.Set<T>().Add(toAdd);
            entities.SaveChanges();
        }

        public virtual void Delete(T toDelete)
        {
            entities.Set<T>().Remove(toDelete);
            entities.SaveChanges();
        }

        public virtual List<T> FindAll()
        {
            return entities.Set<T>().ToList();
        }

        public T FindOne(int id)
        {
            return entities.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public void Update(T toUpdate)
        {
            entities.Set<T>().Update(toUpdate);
            entities.SaveChanges();
        }

        public string GetSalt(int id)
        {
            var x = entities.person.AsNoTracking().FirstOrDefault(x => x.Id == id);
            string z = x.name1 + x.name2 + x.date;
            return z;
        }
    }
}