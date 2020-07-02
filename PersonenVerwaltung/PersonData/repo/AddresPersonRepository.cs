﻿using PersonData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonData.repo
{
    class AddresPersonRepository : Repository<AddressPerson>
    {
        PersonEntities entities = new PersonEntities();

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
