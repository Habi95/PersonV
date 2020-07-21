using Microsoft.EntityFrameworkCore;
using SecurityData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityData.repo
{
    public class ContactRepository : BaseRepository<Contact>
    {
        public ContactRepository(Entities entities) : base(entities)
        {
        }

        public Contact checkContact(Contact contact)
        {
            return entities.contact.Include(x => x.person).ThenInclude(x => x.user).AsNoTracking().FirstOrDefault(x =>

            x.contact_value == contact.contact_value

            );
        }

        internal int checkContactList(List<Contact> contacts, object p)
        {
            throw new NotImplementedException();
        }
    }
}