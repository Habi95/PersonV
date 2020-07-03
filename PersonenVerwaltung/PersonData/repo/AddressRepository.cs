using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    public class AddressRepository : Repository<Address>
    {
        PersonEntities entities = new PersonEntities();

        public AddressRepository(PersonEntities entities)
        {
            this.entities = entities;
        }

        public int Create(Address address)
        {
            entities.address.Add(address);
            return entities.SaveChanges();
        }

        public void DeleteOne(Address address)
        {
            entities.address.Remove(address);
            entities.SaveChanges();
        }

        public List<Address> FindAll()
        {
            return entities.address.ToList();
        }

        public Address FindOne(int id)
        {
            return entities.address.FirstOrDefault(x => x.id == id);
        }

        public void Update(Address entity)
        {
            throw new NotImplementedException();
        }
    }
}
