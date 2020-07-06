using Data.Models;
using Org.BouncyCastle.Asn1.Crmf;
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

        PersonEntities Entities = new PersonEntities();

        public PersonRepository RepositoryPerson;
        public AddressRepository RepositoryAddress;
        public DocumentRepository RepositoryDocument;
        public AddressPersonRepository RepositoryAddressPerson;
        public CourseRepository RepositoryCourse;
        Controller controller = new Controller();
        List<Person> peopleList;

        public Datahandling()
        {
            RepositoryPerson = new PersonRepository(Entities);
            RepositoryAddress = new AddressRepository(Entities);
            RepositoryDocument = new DocumentRepository(Entities);
            RepositoryAddressPerson = new AddressPersonRepository(Entities);
            RepositoryCourse = new CourseRepository(Entities);
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
            return peopleList.FirstOrDefault(x => x.id == id); //Persons.FirstOrDefault(x => x.id == id);
        }
        /// <summary>
        /// Returns basic data form ALl Persons
        /// </summary>
        /// <returns></returns>
        public List<BasePerson> FindAllPersonsBasicData()
        {
            Update();
            return peopleList.ConvertAll(c => CreateBasePerson(c));//Persons.ToList<BasePerson>();//.ConvertAll(x => (BasePerson)x);
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
            List<Person> per = RepositoryPerson.FindAll();
            Dictionary<int, List<Document>>
                doc = RepositoryDocument.GetDocuments<Person>();
            var result = RepositoryCourse.CompletedCourses<int, List<Course>>();           
            if (peopleList != null)
            {
                peopleList.Clear();

            }         
                peopleList = controller.GetPeople(per, doc,result.Item1,result.Item2);
            

        }
        /// <summary>
        /// address id is by getting object id from person
        /// </summary>
        /// <param name="address"></param>
        public void AddAddress(Address address)
        {
            int personId = address.id;
            address.id = 0;
            int addressId = RepositoryAddress.Create(address);
            RepositoryAddressPerson.Create(new AddressPerson()
            {
                addressId = addressId,
                personId = personId
            });
            Entities.SaveChanges();

        }


    }
}