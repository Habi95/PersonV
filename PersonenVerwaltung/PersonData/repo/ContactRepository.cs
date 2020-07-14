using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonData.repo
{
    public class ContactRepository : BaseRepository<Contact>
    {
        public ContactRepository(PersonEntities entities) : base(entities)
        {
        }

        public Contact checkContact(Contact contact)
        {
            var k = entities.contact.FirstOrDefault(x =>

            x.contact_value == contact.contact_value

            );
            return k;
        }

        public List<Contact> check(List<Contact> contacts)
        {
            var list = new List<Contact>();
            foreach (var item in contacts)
            {
                var h = checkContact(item);
                list.Add(h);
            }
            return list;
        }

        public int checkContactList(List<Contact> db, List<Contact> tocheck)
        {
            int count = 0;
            foreach (var item in db)
            {
                foreach (var item2 in tocheck)
                {
                    if (checkContact(item).Id == checkContact(item2).Id)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        //public int Create(Contact contact)
        //{
        //    entities.contact.Add(contact);
        //    return entities.SaveChanges();
        //}

        //public void DeleteOne(Contact contact)
        //{
        //    entities.contact.Remove(contact);
        //    entities.SaveChanges();
        //}

        //public List<Contact> FindAll()
        //{
        //    return entities.contact.ToList();
        //}

        //public Contact FindOne(int id)
        //{
        //    return entities.contact.FirstOrDefault(x => x.Id == id);
        //}

        //public void Update(Contact entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}