using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonController;
using PersonData;
using PersonData.model.person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecurityController : ControllerBase
    {
        public Datahandling datahandling = new Datahandling();

        [HttpGet("{email}/{password}")]
        public string Login(string email, string password)
        {
            try
            {
                var user = datahandling.RepositoryContact.checkContact(new Contact() { contact_value = email });

                string x = datahandling.UserRepository.Hash(password, user.person_id);
                if (x.Equals(user.person.user.password))
                {
                    return JwtHandler.GenerateToken(user.person.user.authentication);
                }
                else
                {
                    Response.StatusCode = 403;
                    Response.WriteAsync($"Password or Email are wrong");
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}