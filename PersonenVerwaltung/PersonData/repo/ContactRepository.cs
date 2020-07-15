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
    }
}