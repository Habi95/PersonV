using PersonData;
using System;
using System.Collections.Generic;

namespace PersonController
{
    public class Datahandling
    {
        List<Person> Persons = new List<Person>();

        public void AddPerson(Person Person)
        {
            
        } 

        /// <summary>
        /// Returns one Person with the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The found Person</returns>
        public Person FindPerson(int id)
        {
            return new Person();  // PersonRepository.create();
        }

        /// <summary>
        /// Returns basic data form ALl Persons
        /// </summary>
        /// <returns></returns>
        public List<Person> findAllPersonsBasicData()
        {
            return new List<Person>(); // PersonRepository.findOne(int id);
        }


        /// <summary>
        /// Find all Persons from Mysql DB
        /// </summary>
        private void updatePersons()
        {
            Persons = new List<Person>(); // PersonRepository.findAll();
        }
    }
}
