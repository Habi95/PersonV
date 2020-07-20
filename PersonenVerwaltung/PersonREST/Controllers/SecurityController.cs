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

        [HttpGet]
        public List<User> GetUsers([FromHeader] string Authorization)
        {
            if (Token(Authorization).admin)
            {
                try
                {
                    Response.StatusCode = 200;
                    return datahandling.UserRepository.FindAll();
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
                return null;
            }
        }

        [HttpPost("{email}/{password}/{secureWord}")]
        public User CreateUser([FromHeader] string Authorization, string email, string password, string secureWord)
        {
            try
            {
                var person = datahandling.RepositoryContact.checkContact(new Contact() { contact_value = email }).person;
                if (person.user == null)
                {
                    User user = new User(password, secureWord);
                    user.person = person;
                    datahandling.UserRepository.Create(user);
                    return user;
                }
                else
                {
                    Response.StatusCode = 403;
                    Response.WriteAsync($"Auf der Email: {email} besteht bereits ein A");
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Token Token(string token)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    return JwtHandler.ReadToken(token);
                }
                else
                {
                    throw new Exception("Bitte neu Anmelden Session Abgelaufen");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}