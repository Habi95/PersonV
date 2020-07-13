using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
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

        public virtual T FindOne(int id)
        {
            return entities.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public virtual void Update(T toUpdate)
        {
            entities.Set<T>().Update(toUpdate);
            entities.SaveChanges();
        }
    }
}