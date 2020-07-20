using PersonData.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonData.repo
{
    public class AddressPersonRepository : BaseRepository<AddressPerson>
    {
        public AddressPersonRepository(PersonEntities entities) : base(entities)
        {
            this.entities = entities;
        }
    }
}