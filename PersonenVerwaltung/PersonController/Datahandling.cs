using PersonData;
using PersonData.model;
using PersonData.repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonController
{
    public class Datahandling
    {
        List<Person> Persons = new List<Person>();
        PersonEntities Entities = new PersonEntities();

        private PersonRepository RepositoryPerson;
        private AddressRepository RepositoryAddress;
        private AddresPersonRepository RepositoryAddressPerson;


        public Datahandling()
        {
            RepositoryPerson = new PersonRepository(Entities);
            RepositoryAddress = new AddressRepository(Entities);
            RepositoryAddressPerson = new AddresPersonRepository(Entities);
            Update();
        }

        /// <summary>
        /// Add a new Person to the DB
        /// </summary>
        /// <param name="person"></param>
        public void AddPerson(Person person)
        {
            RepositoryPerson.Create(person);
            Update();
        }

        /// <summary>
        /// Updates a Person in DB
        /// </summary>
        /// <param name="person"></param>
        public void UpdatePerson(Person person)
        {
            RepositoryPerson.Update(person);
            Update();
        }

        /// <summary>
        /// Returns one Person with the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The found Person</returns>
        public Person FindPerson(int id)
        {
            return Persons.FirstOrDefault(x => x.id == id);
        }

        /// <summary>
        /// Returns basic data form ALl Persons
        /// </summary>
        /// <returns></returns>
        public List<BasePerson> FindAllPersonsBasicData()
        {
            List<BasePerson> output = new List<BasePerson>();
            return Persons.ConvertAll(c => CreateBasePerson(c));//Persons.ToList<BasePerson>();//.ConvertAll(x => (BasePerson)x);
        }

        /// <summary>
        /// Converts Person to Base Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        private BasePerson CreateBasePerson(Person person)
        {
            var basePerson = new BasePerson()
            {
                id = person.id,
                name1 = person.name1,
                name2 = person.name2,
                date = person.date,
                createdAt = person.createdAt,
                modifyAt = person.modifyAt,
                modifyDate = person.modifyDate
            };
            return basePerson;
        }

        /// <summary>
        /// Gets all Data from Mysql DB
        /// </summary>
        private void Update()
        {
            Persons = RepositoryPerson.FindAll();
        }

        public void AddAddress(Address address)
        {
            var personId = address.id;
            address.id = 0;
            var addressId = RepositoryAddress.Create(address);
            var AddressPerson = new AddressPerson() { addressId = addressId, personId = personId };
            RepositoryAddressPerson.Create(AddressPerson);
        }
    }
}