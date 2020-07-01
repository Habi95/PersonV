using PersonData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    class AddresPersonRepository : Repository<AddressPerson>
    {
        PersonEntities entities = new PersonEntities();

        public int create(AddressPerson addressperson)
        {
            entities.addressperson.Add(addressperson);
            return entities.SaveChanges();
        }

        public void deleteOne(AddressPerson addressPerson)
        {
            entities.addressperson.Remove(addressPerson);
            entities.SaveChanges();
        }

        public List<AddressPerson> findAll()
        {
            return entities.addressperson.ToList();
        }

        public AddressPerson findOne(int id)
        {
            return entities.addressperson.FirstOrDefault(x => x.id == id);
        }
    }
}
