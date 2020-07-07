using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    class ContactRepository : IRepository<Contact>
    {
        PersonEntities entities = new PersonEntities();

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
            return entities.contact.FirstOrDefault(x => x.id == id);
        }

        public void Update(Contact entity)
        {
            throw new NotImplementedException();
        }
    }
}
