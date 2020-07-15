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

        public int countBillingAddress(int PersonId)
        {
            var count = 0;
            var x = entities.addressperson.AsNoTracking().Where(x => x.personId == PersonId).ToList();
            entities.addressperson.Where(x => x.personId == PersonId).ToList().ForEach(x =>
            {
                if (x.billing_address == true)
                {
                    count++;
                }
            });
            return count;
        }

        public int checkAddresses(List<AddressPerson> db, List<AddressPerson> tocheck)
        {
            int count = 0;
            foreach (var item in db)
            {
                foreach (var item2 in tocheck)
                {
                    if (checkAddress(item.address) != checkAddress(item2.address))
                    {
                    }
                    else
                    {
                        count++;
                    }
                }
            }
            return count;
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