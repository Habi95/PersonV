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

        [HttpGet]
        public List<BasePerson> getAllPersonsBasicData()
        {
            return datahandling.findAllPersonsBasicData();
        }

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

        //[HttpPut("{id}")]
        //public HttpStatusCode updatePerson(Person person)
        //{
        //    datahandling.update(person);
        //    return HttpStatusCode.Created;
        //}

        [HttpPost]
        public IActionResult Create(Person person)
        {
            person.createdAt = DateTime.Now;
            person.modifyAt = DateTime.Now;
            datahandling.AddPerson(person);
            return Accepted();
        }
    }
}
