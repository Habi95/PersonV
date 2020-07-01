using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonController;
using PersonData;

namespace PersonREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        Datahandling datahandling = new Datahandling();

        [HttpGet] 
        public Person GetPerson(int id)
        {
            return datahandling.FindPerson(id);
        }

        [HttpGet]
        public List<Person> GetPersonsBasic(int id)
        {
            return datahandling.findAllPersonsBasicData();
        }

    }
}
