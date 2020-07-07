using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonController;
using PersonData;
using PersonData.model;

namespace PersonREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        Datahandling datahandling = new Datahandling();

        /// <summary>
        /// base.url/Person Lists all Base Persons 
        /// </summary>
        /// <returns>A list of all Base Person Objects from the DB</returns>
        [HttpGet]
        public List<BasePerson> getAllPersonsBasicData()
        {
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
            if (person.id != 0) // ID has to be greater than 0
            {
                try
                {
                    person.modifyAt = DateTime.Now; // sollte vom Web schon mitkommen!!! weil wir nicht wissen was geändert wurde.
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
            if (person.id == 0)
            {
                try
                {
                    person.createdAt = DateTime.Now; // sollte vom Web schon mitkommen!!!
                    person.modifyAt = DateTime.Now; // sollte vom Web schon mitkommen!!!
                    datahandling.AddPerson(person);
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
                Response.WriteAsync("Person ID incorrect!");
            }
        }

        /// <summary>
        /// Creat's a new Address in DB if the address don't exists
        /// </summary>
        /// <param name="id">Person ID</param>
        /// <param name="address">Address Object</param>
        /// <returns></returns>
        [HttpPost("address/{id}")]
        public void CreateAddress(int id, Address address)
        {
            if (address.id == 0 && id != 0)
            {
                try
                {
                    address.createdAt = DateTime.Now; // sollte vom Web schon mitkommen!!!
                    address.modifyAt = DateTime.Now; // sollte vom Web schon mitkommen!!!
                    datahandling.AddAddress(id, address);
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

        [HttpPost("contact")]
        public void CreateContact(Contact contact)
        {
            if (contact.id == 0 && contact.person_id != 0)
            {
                try
                {
                    contact.modifyDate = DateTime.Now; // sollte vom Web schon mitkommen!!!
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

        [HttpPost("comment")]
        public void CreateComment(Comment comment)
        {
            if (comment.id == 0 && comment.person_id != 0)
            {
                try
                {
                    comment.modifyDate = DateTime.Now; // sollte vom Web schon mitkommen!!!
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
