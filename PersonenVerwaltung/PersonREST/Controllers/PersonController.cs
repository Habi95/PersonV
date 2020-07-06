using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        /// base.url/Person 
        /// </summary>
        /// <returns>A list of all Base Person Objects from the DB</returns>
        [HttpGet]
        public ActionResult<List<BasePerson>> getAllPersonsBasicData()
        {
            var personList = datahandling.FindAllPersonsBasicData();
            if (personList == null)
            {
                return NotFound();
            }
            return personList;
        }

        /// <summary>
        /// base.url/Person/{id}
        /// </summary>
        /// <param name="id">Person id</param>
        /// <returns>Object of Person</returns>
        [HttpGet("{id}")] 
        public ActionResult<Person> GetPerson(int id)
        {
            var person = datahandling.FindPerson(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        /// <summary>
        /// base.url/Person
        /// </summary>
        /// <param name="person">Object of Person with changed parameters</param>
        /// <returns>HttpStatusCode</returns>
        [HttpPut]
        public void UpdatePerson(Person person)
        {
            if (person.id != 0)
            {
                try
                {
                    datahandling.UpdatePerson(person);
                }
                catch (PersonException ex)
                {
                    Response.StatusCode = 500;
                    Response.WriteAsync(ex.Message);
                    throw;
                }
                catch (Exception)
                {
                    Response.StatusCode = 500;
                    throw;
                }

                Response.StatusCode = 201;
            }
            else
            {
                Response.StatusCode = 409;
                Response.WriteAsync("PersonID incorrect!");
            }
        }

        [HttpPost]
        public void Create(Person person)
        {
            if (person.id == 0)
            {
                try
                {
                    person.createdAt = DateTime.Now;
                    person.modifyAt = DateTime.Now;
                    datahandling.AddPerson(person);
                    Response.StatusCode = 201;
                }
                catch (Exception ex)
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

        [HttpPost("address/{id}")]
        public HttpStatusCode CreateAddress(int id, Address address)
        {
            if (address.id == 0 && id != 0)
            {
                address.createdAt = DateTime.Now;
                address.modifyAt = DateTime.Now;
                datahandling.AddAddress(id, address);
                return HttpStatusCode.Created;
            }
            else
            {
                return HttpStatusCode.Conflict;
            }
        }
    }
}
