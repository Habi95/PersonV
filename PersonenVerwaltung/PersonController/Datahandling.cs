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
        PersonEntities entities = new PersonEntities();

        public PersonRepository repository;

        public Datahandling()
        {
            repository = new PersonRepository(entities);
            UpdatePersons();
        }

        /// <summary>
        /// Add a new Person to the DB
        /// </summary>
        /// <param name="person"></param>
        public void AddPerson(Person person)
        {
            repository.Create(person);
            UpdatePersons();
        }

        /// <summary>
        /// Updates a Person in DB
        /// </summary>
        /// <param name="person"></param>
        public void UpdatePerson(Person person)
        {
            repository.Update(person);
            UpdatePersons();
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
        /// Finds all Persons from Mysql DB
        /// </summary>
        private void UpdatePersons()
        {
            Persons = repository.FindAll();
        }
    }
}