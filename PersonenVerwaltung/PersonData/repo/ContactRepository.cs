using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    class ContactRepository : Repository<Contact>
    {
        PersonEntities entities = new PersonEntities();

        public int create(Contact contact)
        {
            entities.contact.Add(contact);
            return entities.SaveChanges();
        }

        public void deleteOne(Contact contact)
        {
            entities.contact.Remove(contact);
            entities.SaveChanges();
        }

        public List<Contact> findAll()
        {
            return entities.contact.ToList();
        }

        public Contact findOne(int id)
        {
            return entities.contact.FirstOrDefault(x => x.id == id);
        }
    }
}
