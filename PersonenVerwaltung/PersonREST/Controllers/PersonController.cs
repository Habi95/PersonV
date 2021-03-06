﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PersonController;
using PersonData;
using PersonData.model;
using PersonData.model.person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace PersonREST.Controllers

/*
 *TODO picture ? how we get from fronted............
 *TODO svn number check ..
 *
 */
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : TokenRest.Controllers.SecurityController
    {
        private Datahandling datahandling = new Datahandling();

        /// <summary>
        /// base.url/Person Lists all Base Persons
        /// </summary>
        /// <returns>A list of all Base Person Objects from the DB</returns>
        [HttpGet]
        public List<BasePerson> getAllPersonsBasicData()
        {
            //Token(Authorization);

            //Environment.u
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
                Response.StatusCode = 201;
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
        public Person UpdatePerson([FromHeader] string Authorization, Person person)
        {
            //if (Token(Authorization).authentication)
            //{
            if (datahandling.Entities.person.AsNoTracking().FirstOrDefault(x => x.Id == person.Id) != null) // check person is not null
            {
                try
                {
                    if (person.addresses != null)
                    {
                        person.addresses.ForEach(x =>
                        {
                            Address address = datahandling.RepositoryAddress.checkAddress(x.address);
                            if (address != null)
                            {
                                if (x.address.Delete)
                                {
                                    datahandling.RepositoryAddressPerson.Delete(datahandling.Entities.addressperson.AsNoTracking().FirstOrDefault(x => x.addressId == address.Id && x.personId == person.Id));
                                }
                                else
                                {
                                    x.address = null;
                                    x.addressId = address.Id;
                                }
                            }
                        });
                    }
                    else if (person.contacts != null)
                    {
                        for (int i = 0; i < person.contacts.Count; i++)

                        {
                            Contact contact = datahandling.RepositoryContact.checkContact(person.contacts[i]);
                            if (contact != null)
                            {
                                if (person.contacts[i].Delete)
                                {
                                    datahandling.RepositoryContact.Delete(person.contacts[i]);
                                }
                                else if (person.contacts[i].contact_value == contact.contact_value)
                                {
                                    person.contacts.RemoveAt(i);
                                }
                            }
                        }
                    }

                    person.ModifyAt = DateTime.Now;
                    datahandling.UpdatePerson(person);
                    Response.StatusCode = 200;
                    return datahandling.FindPerson(person.Id);
                }
                catch (Exception) // general Exception
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
            //}
            //else
            //{
            //    Response.StatusCode = 403;
            //    Response.WriteAsync("Keine Rechte");
            //    return null;
            //}
        }

        [HttpPost]
        public Person Create([FromHeader] string Authorization, Person person)
        {
            if (Token(Authorization).authentication)
            {
                try
                {
                    if (person.Id > 0)
                    {
                        Response.WriteAsync("Bitte machen Sie ein Personen Update das hier ist für neue");
                        return null;
                    }
                    else
                    {
                        if (person.addresses != null)
                        {
                            person.addresses.ForEach(x =>
                            {
                                Address address = datahandling.RepositoryAddress.checkAddress(x.address);
                                if (address != null)
                                {
                                    x.address = null;
                                    x.addressId = address.Id;
                                }
                            });
                        }
                        CreatePerson(person);
                        return datahandling.FindPerson(person.Id);
                    }
                }
                catch (Exception ex)
                {
                    Response.StatusCode = 409;
                    Response.WriteAsync(ex.Message);
                    return null;
                }
            }
            else
            {
                Response.StatusCode = 403;
                Response.WriteAsync("Keine Rechte");
                return null;
            }
        }

        /// <summary>
        /// Delete's the Person with ID
        /// </summary>
        /// <param name="PersonId">PersonID to delete</param>
        [HttpDelete("{PersonId}")]
        public void DeletePerson([FromHeader] string Authorization, int PersonId)
        {
            if (Token(Authorization).authentication)
            {
                try
                {
                    var toDelete = datahandling.Entities.person.FirstOrDefault(x => x.Id == PersonId);
                    if (toDelete != null)
                    {
                        toDelete.documents.ForEach(x => { datahandling.RepositoryDocument.DeleteById(x.Id); });
                        datahandling.RepositoryPerson.Delete(toDelete);
                        Response.StatusCode = 201;
                        Response.WriteAsync("Erfolgreich gelöscht");
                    }
                    else
                    {
                        Response.StatusCode = 500;
                        Response.WriteAsync($"Die Person mit der ID: {PersonId} Existiert in der DB nicht");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                Response.StatusCode = 403;
                Response.WriteAsync("Keine Rechte");
            }
        }

        /// <summary>
        /// Creat's a new Address in DB if the address don't exists
        /// https://localhost:44303/person/address
        /// </summary>
        /// <param name="id">Person ID</param>
        /// <param name="address">Address Json with ID=0</param>
        /// <returns></returns>
        [HttpPost("address")]
        public void CreateAddress([FromHeader] string Authorization, Address address)
        {
            //if (Token(Authorization).authentication)
            //{
            try
            {
                if (!datahandling.RepositoryAddress.IsAddressExist(address))
                {
                    datahandling.AddAddress(address);
                    Response.StatusCode = 201;
                }
                else
                {
                    throw new PersonException($"");
                }
            }
            catch (Exception)
            {
                var existAddress = datahandling.RepositoryAddress.checkAddress(address);
                Response.StatusCode = 403;
                Response.WriteAsync($"Die Adresse:\n{existAddress.street}\n{existAddress.place} - {existAddress.zip}\n{existAddress.country}\nWurde am {existAddress.CreatedAt} erstellt");
            }
            //}
            //else
            //{
            //    Response.StatusCode = 403;
            //    Response.WriteAsync("Keine Rechte");
            //}
        }

        /// <summary>
        /// Delete's the address with ID
        /// </summary>
        /// <param name="address">AddressID to delete</param>
        [HttpDelete("address")]
        public void DeleteAddress([FromHeader] string Authorization, Address address)
        {
            //if (Token(Authorization).authentication)
            //{
            if (datahandling.RepositoryAddress.checkAddress(address) != null)
            {
                datahandling.RepositoryAddress.Delete(address);
                Response.StatusCode = 201;
                Response.WriteAsync("Erfolgreich gelöscht");
            }
            else
            {
                Response.StatusCode = 500;
                Response.WriteAsync($"Die Addresse mit der ID: {address.Id} gibt es nicht in der Datenbank");
            }
            //}
            //else
            //{
            //    Response.StatusCode = 403;
            //    Response.WriteAsync("Keine Rechte");
            //}
        }

        /// <summary>
        /// Creat's a new Contact in DB
        /// </summary>
        /// <param name="contact">Contact Json with ID=0</param>
        [HttpPost("contact")]
        public void CreateContact([FromHeader] string Authorization, Contact contact)
        {
            //if (Token(Authorization).authentication)
            //{
            if (contact.Id == 0 && contact.person_id != 0)
            {
                try
                {
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
            //}
            //else
            //{
            //    Response.StatusCode = 403;
            //    Response.WriteAsync("Keine Rechte");
            //}
        }

        /// <summary>
        /// Delete contact by value
        /// </summary>
        /// <param name="comment"></param>
        [HttpDelete("contact/{conatctValue}")]
        public void DeleteContact([FromHeader] string Authorization, string conatctValue)
        {
            //if (Token(Authorization).authentication)
            //{
            try
            {
                var toDelete = datahandling.RepositoryContact.checkContact(new Contact { contact_value = conatctValue });
                if (toDelete != null)
                {
                    datahandling.RepositoryContact.Delete(toDelete);
                    Response.StatusCode = 201;
                    Response.WriteAsync("Erfolgreich gelöscht");
                }
                else
                {
                    Response.StatusCode = 500;
                    Response.WriteAsync($"Der Kontakt: {conatctValue} Existiert in der DB nicht");
                }
            }
            catch (Exception)
            {
                throw;
            }
            //}
            //else
            //{
            //    Response.StatusCode = 403;
            //    Response.WriteAsync("Keine Rechte");
            //}
        }

        /// <summary>
        /// Creat's a new Comment in DB
        /// </summary>
        /// <param name="comment">Comment Json with ID=0</param>
        [HttpPost("comment")]
        public void CreateComment([FromHeader] string Authorization, Comment comment)
        {
            //if (Token(Authorization).authentication)
            //{
            if (comment.Id == 0 && comment.person_id != 0)
            {
                try
                {
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
            //}
            //else
            //{
            //    Response.StatusCode = 403;
            //    Response.WriteAsync("Keine Rechte");
            //}
        }

        /// <summary>
        /// Method used in this class to add a Person to the DB
        /// </summary>
        /// <param name="person"></param>
        private void CreatePerson(Person person)
        {
            try
            {
                datahandling.AddPerson(person);
                Response.StatusCode = 201;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Response.StatusCode = 500;
                throw;
            }
        }
    }
}