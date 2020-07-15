using Microsoft.EntityFrameworkCore;
using PersonData.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonData.repo
{
    public class AddressRepository : BaseRepository<Address>
    {
        public AddressRepository(PersonEntities entities) : base(entities)
        {
        }

        public Address checkAddress(Address address)
        {
            return entities.address.FirstOrDefault(x =>
            x.street == address.street &&
            x.place == address.place &&
            x.zip == address.zip &&
            x.country == address.country
             );
        }

        public bool IsAddressExist(Address address)
        {
            if (checkAddress(address) == null)
            {
                address.CreatedAt = DateTime.Now;
                return true;
            }

            return false;
        }
    }
}