using PersonData.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonData.repo
{
    public class AddressPersonRepository : IRepository<AddressPerson>
    {
        private PersonEntities entities;

        public AddressPersonRepository(PersonEntities entities)
        {
            this.entities = entities;
        }

        public int Create(AddressPerson addressperson)
        {
            entities.addressperson.Add(addressperson);
            return entities.SaveChanges();
        }

        public void DeleteOne(AddressPerson addressPerson)
        {
            entities.addressperson.Remove(addressPerson);
            entities.SaveChanges();
        }

        public List<AddressPerson> FindAll()
        {
            return entities.addressperson.ToList();
        }

        public AddressPerson FindOne(int id)
        {
            return entities.addressperson.FirstOrDefault(x => x.id == id);
        }

        public void Update(AddressPerson entity)
        {
            throw new NotImplementedException();
        }
    }
}