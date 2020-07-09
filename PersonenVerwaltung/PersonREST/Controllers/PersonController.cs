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
            Person person = new Person();
            person.sv_nr = 1951030189;
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
            SocialSecurityNumberClaculator socialSecurity = new SocialSecurityNumberClaculator();
            if (entities.person.FirstOrDefault(x => x.Id == person.Id) == null) //check person id is no existing
            {
                //if (person.sv_nr.HasValue)
                //{
                //    if (person.IsValidSvnr)
                //    {
                //        CreatePerson(person);
                //    }
                //    else
                //    {
                //        Response.StatusCode = 500;
                //    }
                //}
                //else
                //{
                CreatePerson(person);
                //}
            }
            else
            {
                Response.StatusCode = 409;
                Response.WriteAsync("Person ID incorrect!");
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
        /// </summary>
        /// <param name="id">Person ID</param>
        /// <param name="address">Address Json with ID=0</param>
        /// <returns></returns>
        [HttpPost("address/{id}")]
        public void CreateAddress(int id, Address address)
        {
            if (entities.address.FirstOrDefault(x => x.Id == address.Id) == null) // make new  // addressPerson
            {
                try
                {
                    address.CreatedAt = DateTime.Now; // sollte vom Web schon mitkommen!!!
                    address.ModifyAt = DateTime.Now; // sollte vom Web schon mitkommen!!!
                    datahandling.AddAddress(id, address);
                    Response.StatusCode = 201;
                }
                catch (Exception)
                {
                    Response.StatusCode = 500;
                    throw;
                }
            }
            else if (true)
            {
            }
            // Response.StatusCode = 409;
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
    }
}