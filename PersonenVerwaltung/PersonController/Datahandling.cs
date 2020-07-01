using PersonData;
using System;
using System.Collections.Generic;

namespace PersonController
{
    public class Datahandling
    {
        List<master_file> persons = new List<master_file>();

        public void AddPerson(master_file person)
        {
            
        } 

        /// <summary>
        /// Returns one Person with the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The found Person</returns>
        public master_file FindPerson(int id)
        {
            return new master_file();  // PersonRepository.create();
        }

        /// <summary>
        /// Returns basic data form ALl Persons
        /// </summary>
        /// <returns></returns>
        public List<master_file> findAllPersonsBasicData()
        {
            return new List<master_file>(); // PersonRepository.findOne(int id);
        }


        /// <summary>
        /// Find all Persons from Mysql DB
        /// </summary>
        private void updatePersons()
        {
            persons = new List<master_file>(); // PersonRepository.findAll();
        }
    }
}
