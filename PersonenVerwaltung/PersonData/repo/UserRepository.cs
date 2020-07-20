using PersonData.model.person;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PersonData.repo
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(PersonEntities Entities) : base(Entities)
        {
        }

        public string Hash(string x, int id)
        {
            using (SHA256 hA256 = SHA256.Create())
            {
                return Encoding.ASCII.GetString(hA256.ComputeHash(Encoding.ASCII.GetBytes(x + GetSalt(id))));
            }
        }

        public void CreateFor(User toAdd)
        {
            toAdd.password = Hash(toAdd.password, toAdd.person.Id);
            toAdd.security_word = Hash(toAdd.security_word, toAdd.person.Id);
            toAdd.person = null;
        }

        public override void Create(User toAdd)
        {
            CreateFor(toAdd);
            entities.user.Add(toAdd);
            entities.SaveChanges();
        }
    }
}