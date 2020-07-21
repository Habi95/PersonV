using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityController;
using SecurityData.model;

namespace TokenRest.Controllers
{
    /// <summary>
    /// Actuallly NOT in use
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private Datahandling datahandling = new Datahandling();

        //History for user what he doing about
        [HttpGet("{email}/{password}")]
        public string Login(string email, string password)

        {
            try
            {
                var user = datahandling.RepositoryContact.checkContact(new Contact() { contact_value = email });

                string x = datahandling.UserRepository.Hash(password, user.person_id);
                if (x.Equals(user.person.user.password))
                {
                    return JwtHandler.GenerateToken(user.person.user, email);
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

        [HttpGet("user")]
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

        [HttpPut("user")]
        public User UpdateUser([FromHeader] string Authorization, User user)
        {
            if (Token(Authorization).admin)
            {
                try
                {
                    if (datahandling.Entities.user.FirstOrDefault(x => x.Id == user.Id) != null)
                    {
                        if (user.Delete)
                        {
                            datahandling.UserRepository.Delete(user);
                            Response.StatusCode = 200;
                            Response.WriteAsync("Erfolgreich gelöscht");
                            return null;
                        }
                        else
                        {
                            if (EqualsHash(user))
                            {
                                datahandling.UserRepository.Update(user);
                                return user;
                            }
                            else
                            {
                                throw new Exception($"Es sind keine Änderungen am Passwort oder am Sicherheitswort gestattet");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"Den User mit der Id: {user.Id} exestiert nicht");
                    }
                }
                catch (Exception)
                {
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

        [HttpPut("setpassword/{email}/{securityWord}/{newPassword}")]
        public User ChangePassword(string email, string securityWord, string newPassword)
        {
            try
            {
                var user = datahandling.RepositoryContact.checkContact(new Contact() { contact_value = email }).person.user;
                var sw = datahandling.UserRepository.Hash(securityWord, user.person.Id);
                if (user != null)
                {
                    if (sw.Equals(user.security_word))
                    {
                        user.password = datahandling.UserRepository.Hash(newPassword, user.person.Id);
                        return user;
                    }
                    else
                    {
                        Response.StatusCode = 403;
                        Response.WriteAsync($"Falsches Sicherheitswort");
                    }
                }
                else
                {
                    Response.StatusCode = 403;
                    Response.WriteAsync($"Kein User gefunden");
                    return null;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
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
                    Response.WriteAsync($"Auf der Email: {email} besteht bereits ein Account");
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

        public bool EqualsHash(User user)
        {
            var x = datahandling.UserRepository.FindOne(user.Id);
            if (x.password.Equals(user.password) && x.security_word.Equals(user.security_word))
            {
                return true;
            }
            return false;
        }
    }
}