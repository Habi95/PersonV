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
        Controller controller = new Controller();

        List<Person> peopleList;
        List<Address> Addresses = new List<Address>();

        public Datahandling()
        {
            RepositoryPerson = new PersonRepository(Entities);
            RepositoryAddress = new AddressRepository(Entities);
            RepositoryDocument = new DocumentRepository(Entities);
            RepositoryAddressPerson = new AddressPersonRepository(Entities);
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
                if (peopleList.FirstOrDefault(x => x.id == person.id) != null)
                {
                    RepositoryPerson.Update(person);
                    Update();
                }
            }
            catch (NullReferenceException)
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
            try
            {
                var tempPerson = peopleList.FirstOrDefault(x => x.id == id);
                if (tempPerson != null)
                {
                    return tempPerson;
                }
                else
                {
                    throw new PersonException($"Person with ID {id} does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Returns basic data form ALl Persons
        /// </summary>
        /// <returns></returns>
        public List<BasePerson> FindAllPersonsBasicData()
        {
            Update(); 
            try
            {
                return peopleList.ConvertAll(c => CreateBasePerson(c));
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            if (peopleList != null)
            {
                peopleList.Clear();
            }

            peopleList = controller.GetPeople(per, doc);
            Addresses = RepositoryAddress.FindAll();
        }
        /// <summary>
        /// id == Person ID
        /// </summary>
        /// <param name="address"></param>
        public void AddAddress(int id, Address address)
        {
            var personId = id;
            var addressId = 0;

            try
            {
                if (Addresses.FirstOrDefault(x => x.street == address.street) == null)
                {
                    addressId = RepositoryAddress.Create(address);
                }
                else if (Addresses.Where(x => x.street == address.street).ToList().FirstOrDefault(x => x.zip == address.zip) == null)
                {
                    addressId = RepositoryAddress.Create(address);
                }
                else
                {
                    addressId = Addresses.Where(x => x.street == address.street).ToList().FirstOrDefault(x => x.zip == address.zip).id;
                }

                var AddressPerson = new AddressPerson() { addressId = addressId, personId = personId };
                RepositoryAddressPerson.Create(AddressPerson);
                Update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}