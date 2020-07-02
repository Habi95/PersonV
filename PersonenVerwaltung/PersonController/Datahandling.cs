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
        PersonEntities entities = new PersonEntities();
        private PersonRepository repository;

        List<Person> Persons = new List<Person>();

        public Datahandling()
        {
            repository = new PersonRepository(entities);
            updatePersons();
        }

        public void AddPerson(Person person)
        {
            repository.create(person);
            updatePersons();
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
        public List<BasePerson> findAllPersonsBasicData()
        {
            return Persons.ToList<BasePerson>();//.ConvertAll(x => (BasePerson)x);
        }

        /// <summary>
        /// Find all Persons from Mysql DB
        /// </summary>
        private void updatePersons()
        {
            Persons = repository.findAll();
        }
    }
}
