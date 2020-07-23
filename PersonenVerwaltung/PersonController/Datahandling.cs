using Data.Models;
using PersonData;
using PersonData.model;
using PersonData.model.person;
using PersonData.repo;
using SecurityController;
using System;
using System.Collections.Generic;
using System.Linq;
using SecurityData.model;

namespace PersonController
{
    /// <summary>
    /// This class manages the handling of the repositories. Person, Address, Contact and Comment
    /// </summary>
    public class Datahandling
    {
        public PersonEntities Entities = new PersonEntities();
        public SecurityData.repo.Entities Sentities = new SecurityData.repo.Entities();

        public PersonRepository RepositoryPerson;
        public AddressRepository RepositoryAddress;
        public DocumentRepository RepositoryDocument;
        public AddressPersonRepository RepositoryAddressPerson;
        public CourseRepository RepositoryCourse;
        public ContactRepository RepositoryContact;
        public CommentRepository RepositoryComment;
        public CommunicationRepository RepositoryCommunication;
        public UserRepository UserRepository;

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
            UserRepository = new UserRepository(Entities);
        }

        /// <summary>
        /// Add a new Person to the DB
        /// </summary>
        /// <param name="person"></param>
        public void AddPerson(PersonData.Person person)
        {
            if (RepositoryPerson.checkPerson(person, RepositoryContact))
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
        public void UpdatePerson(PersonData.Person person)
        {
            if (person.user != null)
            {
                person.user.password = UserRepository.GeneratePassword();
                var x = person.user.password;
                var y = person.user.security_word;
                person.user.person = person;
                UserRepository.CreateFor(person.user);
                var c = Entities.contact.Where(x => x.person_id == person.Id).FirstOrDefault(k => k.contact_value.Contains("@"));
                EmailController.SendEmail(new SecurityData.model.User(x, y) { admin = person.user.admin, authentication = person.user.authentication }, c.contact_value, Sentities);
            }
            RepositoryPerson.Update(person);
        }

        /// <summary>
        /// Returns one Person with the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The found Person</returns>
        public PersonData.Person FindPerson(int id)
        {
            try
            {
                var tempPerson = RepositoryPerson.FindAll().FirstOrDefault(x => x.Id == id);

                if (tempPerson != null)
                {
                    var result = RepositoryCourse.CompletedCourses<Course>(id);
                    tempPerson.CompletedCourse = result.Item1;
                    tempPerson.NotCompletedCourse = result.Item2;
                    tempPerson.documents = RepositoryDocument.GetDocuments<PersonData.Person>(id);
                    tempPerson.Communications = RepositoryCommunication.GetCommunications<PersonData.Person>(id);

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
        public List<PersonData.model.BasePerson> FindAllPersonsBasicData()
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
        /// Add's a new address to DB id == Person ID
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

        /// <summary>
        /// Add's a new Contact to DB
        /// </summary>
        /// <param name="contactInfo"></param>
        public void AddContact(PersonData.Contact contactInfo)
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

        /// <summary>
        /// Add's a relation between PersonID and AddressID to the DB
        /// </summary>
        /// <param name="PersonId"></param>
        /// <param name="AddressId"></param>
        /// <param name="billingAddress"></param>
        /// <param name="contactType"></param>
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

        /// <summary>
        /// Add's a new Comment to DB
        /// </summary>
        /// <param name="comment"></param>
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
    }
}