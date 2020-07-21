using SecurityData.repo;
using System;

namespace SecurityController
{
    public class Datahandling
    {
        public Entities Entities = new Entities();

        public PersonRepository RepositoryPerson;
        public ContactRepository RepositoryContact;
        public UserRepository UserRepository;

        public Datahandling()
        {
            RepositoryPerson = new PersonRepository(Entities);
            RepositoryContact = new ContactRepository(Entities);
            UserRepository = new UserRepository(Entities);
        }
    }
}