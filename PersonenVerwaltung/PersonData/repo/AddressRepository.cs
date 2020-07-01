using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    class AddressRepository : Repository<Address>
    {
        PersonEntities entities = new PersonEntities();

        public int create(Address address)
        {
            entities.address.Add(address);
            return entities.SaveChanges();
        }

        public void deleteOne(Address address)
        {
            entities.address.Remove(address);
            entities.SaveChanges();
        }

        public List<Address> findAll()
        {
            return entities.address.ToList();
        }

        public Address findOne(int id)
        {
            return entities.address.FirstOrDefault(x => x.id == id);
        }
    }
}
