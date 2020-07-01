using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost("create")]
        public IActionResult Create(Person person)
        {
            datahandling.AddPerson(person);
            return Accepted();
        }

        [HttpGet]
        public List<BasePerson> getAllPersonsBasicData()
        {
            return datahandling.findAllPersonsBasicData();
        }

        //[HttpGet("address/{id}")]
        //public List<Address> getAddress(int id)
        //{
        //    return datahandling.FindAddress(id);
        //}
        //
        //[HttpGet("contact/{id}")]
        //public Person get(int id)
        //{
        //    return datahandling.FindPerson(id);
        //}
    }
}
