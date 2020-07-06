using PersonData;
using PersonData.model;
using PersonData.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
            try
            {
                if (Persons.FirstOrDefault(x => x.id == person.id).id != null)
                {
                    RepositoryPerson.Update(person);
                    Update();
                }
            }
            catch (Exception ex)
            {
                throw new PersonException($"Person with ID {person.id} does not exist!");
            }
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
            return Persons.ConvertAll(c => CreateBasePerson(c));
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

        public void AddAddress(int id, Address address)
        {
            var personId = id;
            var addressId = RepositoryAddress.Create(address);
            var AddressPerson = new AddressPerson() { addressId = addressId, personId = personId };
            RepositoryAddressPerson.Create(AddressPerson);
        }
    }
}