using Data.Models;
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
        public PersonEntities Entities = new PersonEntities();

        public PersonRepository RepositoryPerson;
        public AddressRepository RepositoryAddress;
        public DocumentRepository RepositoryDocument;
        public AddressPersonRepository RepositoryAddressPerson;
        public CourseRepository RepositoryCourse;
        public ContactRepository RepositoryContact;
        public CommentRepository RepositoryComment;
        public CommunicationRepository RepositoryCommunication;

        public Datahandling()
        {
            RepositoryPerson = new PersonRepository(Entities);
            RepositoryAddress = new AddressRepository(Entities);
            RepositoryDocument = new DocumentRepository(Entities);
            RepositoryAddressPerson = new AddressPersonRepository(Entities);
            RepositoryCourse = new CourseRepository(Entities);
            RepositoryContact = new ContactRepository(Entities);
            RepositoryComment = new CommentRepository(Entities);
            RepositoryCommunication = new CommunicationRepository(Entities);
        }

        /// <summary>
        /// Add a new Person to the DB
        /// </summary>
        /// <param name="person"></param>
        public void AddPerson(Person person)
        {
            if (RepositoryPerson.checkPerson(person, RepositoryAddress, RepositoryContact))
            {
                RepositoryPerson.Create(person);
            }
            else
            {
                throw new PersonException($"Person bereits vorhanden");
            }
        }

        /// <summary>
        /// Updates a Person in DB
        /// </summary>
        /// <param name="person"></param>
        public void UpdatePerson(Person person)
        {
            RepositoryPerson.Update(person);
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
                var tempPerson = RepositoryPerson.FindAll().FirstOrDefault(x => x.Id == id);

                if (tempPerson != null)
                {
                    var result = RepositoryCourse.CompletedCourses<Course>(id);
                    tempPerson.CompletedCourse = result.Item1;
                    tempPerson.NotCompletedCourse = result.Item2;
                    tempPerson.documents = RepositoryDocument.GetDocuments<Person>(id);
                    tempPerson.Communications = RepositoryCommunication.GetCommunications<Person>(id);

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
            try
            {
                return Entities.person.ToList().ConvertAll(c => PersonRepository.CreateBasePerson(c));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// id == Person ID
        /// </summary>
        /// <param name="address"></param>
        public void AddAddress(Address address)
        {
            try
            {
                RepositoryAddress.Create(address);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddContact(Contact contactInfo)
        {
            try
            {
                RepositoryContact.Create(contactInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddAddressPerson(int PersonId, int AddressId, bool billingAddress, EContactType contactType)
        {
            RepositoryAddressPerson.Create(new AddressPerson()
            {
                addressId = AddressId,
                personId = PersonId,
                billing_address = billingAddress,
                contact_type = contactType
            }); ;
        }

        public void AddComment(Comment comment)
        {
            try
            {
                RepositoryComment.Create(comment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete<T>(T toDelete) where T : BaseClassCreatedModify
        {
            var x = Entities.Set<T>();
            x.Remove(toDelete);
            Entities.SaveChanges();
        }
    }
}