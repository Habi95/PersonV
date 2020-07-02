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
        private PersonRepository repository = new PersonRepository();

        List<Person> Persons = new List<Person>();

        public Datahandling()
        {
            UpdatePersons();
        }

        public void AddPerson(Person person)
        {
            repository.create(person);
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
        public List<BasePerson> findAllPersonsBasicData()
        {
            return Persons.ToList<BasePerson>();//.ConvertAll(x => (BasePerson)x);
        }

        public int update(Person person)
        {
            return repository.update(person);
        }

        /// <summary>
        /// Find all Persons from Mysql DB
        /// </summary>
        private void UpdatePersons()
        {
            Persons = repository.findAll();
        }
    }
}
