using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonController;
using PersonData;
using PersonData.model;
using PersonData.model.person;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonREST.Controllers

/*
 * TODO Delete Contact
 * TODO Delete Person?  when person delet deleted all but not documents and communication sender
 * TODO Delete Address => delted rel person address
 * TODO Refact Repository generic
 */
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private Datahandling datahandling = new Datahandling();
        private PersonEntities entities = new PersonEntities();

        /// <summary>
        /// base.url/Person Lists all Base Persons
        /// </summary>
        /// <returns>A list of all Base Person Objects from the DB</returns>
        [HttpGet]
        public List<BasePerson> getAllPersonsBasicData()
        {
            //1951030189
            //Person person = new Person();
            //person.sv_nr = 1951030189;
            try
            {
                var personList = datahandling.FindAllPersonsBasicData();
                Response.StatusCode = 200;
                return personList;
            }
            catch (Exception) // general Exception
            {
                Response.StatusCode = 500;
                throw;
            }
        }

        /// <summary>
        /// base.url/Person/{id} returns one Person
        /// </summary>
        /// <param name="id">Person id</param>
        /// <returns>Object of Person</returns>
        [HttpGet("{id}")]
        public Person GetPerson(int id)
        {
            try
            {
                var person = datahandling.FindPerson(id);
                Response.StatusCode = 200;
                return person;
            }
            catch (PersonException ex) // if the person doesn't exists
            {
                Response.StatusCode = 500;
                Response.WriteAsync(ex.Message);
                throw;
            }
            catch (Exception) // general Exception
            {
                Response.StatusCode = 500;
                throw;
            }
        }

        /// <summary>
        /// base.url/Person updates one Person
        /// </summary>
        /// <param name="person">Object of Person with changed parameters</param>
        /// <returns>HttpStatusCode</returns>
        [HttpPut]
        public void UpdatePerson(Person person)
        {
            if (entities.person.FirstOrDefault(x => x.Id == person.Id) != null) // check person is not null
            {
                try
                {
                    person.ModifyAt = DateTime.Now; // sollte vom Web schon mitkommen!!! weil wir nicht wissen was geändert wurde.
                    datahandling.UpdatePerson(person);
                }
                catch (PersonException ex) // if the person doesn't exists
                {
                    Response.StatusCode = 500;
                    Response.WriteAsync(ex.Message);
                    throw;
                }
                catch (Exception) // general Exception
                {
                    Response.StatusCode = 500;
                    throw;
                }

                Response.StatusCode = 201;
            }
            else
            {
                Response.StatusCode = 409;
                Response.WriteAsync("Person ID incorrect!");
            }
        }

        [HttpPost]
        public void Create(Person person)
        {
            try
            {
                SocialSecurityNumberClaculator socialSecurity = new SocialSecurityNumberClaculator();
                if (person.sv_nr.HasValue)
                {
                    if (person.Id > 0)
                    {
                        Response.WriteAsync("Bitte machen Sie ein Personen Update das hier ist für neue");
                    }
                    else
                    {
                        if (entities.person.FirstOrDefault(x => x.sv_nr == person.sv_nr) == null)
                        {
                            CreatePerson(person);
                        }
                        else
                        {
                            Response.WriteAsync("Person mit dieser Sozialversicherungs-Nummer bereits eingetragen");
                        }
                    }
                }
                else
                {
                    CreatePerson(person);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 409;
                Response.WriteAsync(ex.Message);
            }
        }

        private void CreatePerson(Person person)
        {
            try
            {
                person.CreatedAt = DateTime.Now; // sollte vom Web schon mitkommen!!!
                person.ModifyAt = DateTime.Now; // sollte vom Web schon mitkommen!!!
                datahandling.AddPerson(person);
                Response.StatusCode = 201;
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                throw;
            }
        }

        /// <summary>
        /// Creat's a new Address in DB if the address don't exists
        /// https://localhost:44303/person/address/1/true/Privat
        /// </summary>
        /// <param name="id">Person ID</param>
        /// <param name="address">Address Json with ID=0</param>
        /// <returns></returns>
        [HttpPost("address/{id}/{billingAddress}/{contactType}")]
        public void CreateAddress(int id, Address address, bool billingAddress, EContactType contactType)
        {
            if (datahandling.RepositoryAddress.checkAddress(address) == null) // make new  // addressPerson
            {
                try
                {
                    address.CreatedAt = DateTime.Now;
                    address.ModifyAt = DateTime.Now;
                    datahandling.AddAddress(address);
                    CreateAddressPerson(id, datahandling.RepositoryAddress.checkAddress(address).Id, billingAddress, contactType);

                    if (countBillingAddress(id) == 1)
                    {
                        Response.StatusCode = 201;
                    }
                    else
                    {
                        Response.StatusCode = 201;
                        Response.WriteAsync($"Es besteht auf der Person mit der ID {id} gesamt {countBillingAddress(id)} Rechnugsaddressen");
                    }
                }
                catch (Exception)
                {
                    Response.StatusCode = 500;
                    throw;
                }
            }
            else if (entities.addressperson.FirstOrDefault(x => x.addressId == datahandling.RepositoryAddress.checkAddress(address).Id) == null)
            {
                try
                {
                    if (countBillingAddress(id) == 1)
                    {
                        CreateAddressPerson(id, address.Id, billingAddress, contactType);
                    }
                    else
                    {
                        CreateAddressPerson(id, address.Id, billingAddress, contactType);
                        Response.WriteAsync($"Es besteht auf der Person mit der ID {id} gesamt {countBillingAddress(id)} Rechnugsaddressen");
                    }
                }
                catch (Exception)
                {
                    Response.StatusCode = 500;
                    throw;
                }
            }
            else
            {
                Response.StatusCode = 201;
                Response.WriteAsync($"Addresse bereits eingetragen");
            }
        }

        [HttpDelete("address")]
        public void DeleteAddress(Address address)
        {
            if (datahandling.RepositoryAddress.checkAddress(address) != null)
            {
                datahandling.Delete<Address>(datahandling.RepositoryAddress.checkAddress(address));
                Response.StatusCode = 201;
                Response.WriteAsync("Erfolgreich gelöscht");
            }
            else
            {
                Response.StatusCode = 500;
                Response.WriteAsync($"Die Addresse mit der ID: {address.Id} gibt es nicht in der Datenbank");
            }
        }

        /// <summary>
        /// Creat's a new Contact in DB
        /// </summary>
        /// <param name="contact">Contact Json with ID=0</param>
        [HttpPost("contact")]
        public void CreateContact(Contact contact)
        {
            if (contact.Id == 0 && contact.person_id != 0)
            {
                try
                {
                    contact.ModifyDate = DateTime.Now; // sollte vom Web schon mitkommen!!!
                    datahandling.AddContact(contact);
                    Response.StatusCode = 201;
                }
                catch (Exception)
                {
                    Response.StatusCode = 500;
                    throw;
                }
            }
            else
            {
                Response.StatusCode = 409;
            }
        }

        /// <summary>
        /// Creat's a new Comment in DB
        /// </summary>
        /// <param name="comment">Comment Json with ID=0</param>
        [HttpPost("comment")]
        public void CreateComment(Comment comment)
        {
            if (comment.Id == 0 && comment.person_id != 0)
            {
                try
                {
                    comment.ModifyDate = DateTime.Now; // sollte vom Web schon mitkommen!!!
                    datahandling.AddComment(comment);
                    Response.StatusCode = 201;
                }
                catch (Exception)
                {
                    Response.StatusCode = 500;
                    throw;
                }
            }
            else
            {
                Response.StatusCode = 409;
            }
        }

        private int countBillingAddress(int PersonId)
        {
            var count = 0;
            var x = entities.addressperson.Where(x => x.personId == PersonId).ToList();
            entities.addressperson.Where(x => x.personId == PersonId).ToList().ForEach(x =>
           {
               if (x.billing_address == true)
               {
                   count++;
               }
           });
            return count;
        }

        //private Address checkAddress(Address address)
        //{
        //    return entities.address.FirstOrDefault(x =>
        //     x.street == address.street &&
        //     x.place == address.place &&
        //     x.zip == address.zip &&
        //     x.country == address.country
        //     );
        //}

        private void CreateAddressPerson(int PersonId, int AddressId, bool billingAddress, EContactType contactType)
        {
            datahandling.AddAddressPerson(PersonId, AddressId, billingAddress, contactType);
        }
    }
}