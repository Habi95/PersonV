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
            List<BasePerson> output = new List<BasePerson>();

            //Persons.ToList<BasePerson>().ForEach(item =>
            //{
            //    output.Add(new BasePerson() { id = item.id, name1 = item.name1, name2 = item.name2, date = item.date, createdAt = item.createdAt, modifyAt = item.modifyAt, modifyDate = item.modifyDate }); ;
            //});

            return Persons.ConvertAll(c => CreateBasePerson(c));//Persons.ToList<BasePerson>();//.ConvertAll(x => (BasePerson)x);
        }


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
        /// Find all Persons from Mysql DB
        /// </summary>
        private void updatePersons()
        {
            Persons = repository.findAll();
        }
    }
}