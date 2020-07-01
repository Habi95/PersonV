using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    public class PersonRepository : Repository<Person>
    {
        PersonEntities entities;
        public PersonRepository(PersonEntities entities)
        {
            this.entities = entities;
        }
        public void GetPersons()
        {
             entities.person
                .Include(x =>
                    x.addresses)
                .Include(x =>
                    x.contacts)
                .Include(x =>
                    x.comments)
                .ToList();
            entities.address
                .Include(x =>
                    x.persons)
                .ToList();
            
        }

        //public List<Address> GetAddresses()
        //{
        //    return 
        //}
    }
}
