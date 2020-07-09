using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonData.repo
{
    public class ContactRepository : IRepository<Contact>
    {
        private PersonEntities entities = new PersonEntities();

        public ContactRepository(PersonEntities entities)
        {
            this.entities = entities;
        }

        public int Create(Contact contact)
        {
            entities.contact.Add(contact);
            return entities.SaveChanges();
        }

        public void DeleteOne(Contact contact)
        {
            entities.contact.Remove(contact);
            entities.SaveChanges();
        }

        public List<Contact> FindAll()
        {
            return entities.contact.ToList();
        }

        public Contact FindOne(int id)
        {
            return entities.contact.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Contact entity)
        {
            throw new NotImplementedException();
        }
    }
}