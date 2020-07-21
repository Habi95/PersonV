using Microsoft.EntityFrameworkCore;
using SecurityData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityData.repo
{
    public class PersonRepository : BaseRepository<Person>
    {
        public PersonRepository(Entities entities) : base(entities)
        {
        }

        public override List<Person> FindAll()
        {
            return entities.person

              .Include(x =>
                    x.contacts)
              .Include(x => x.user)
              .AsNoTracking()
              .ToList();
        }
    }
}