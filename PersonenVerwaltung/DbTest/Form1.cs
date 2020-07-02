using Microsoft.EntityFrameworkCore;
using PersonData;
using PersonData.model;
using PersonData.repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
            var entities = new PersonEntities();
            PersonRepository repo = new PersonRepository(entities);

            //repo.GetPersons();
            //repo.GetAddresses();
            //entities.person.Add(new Person() 
            //{
            //    name1 = "Helga", name2 = "Strolz", 
            //    gender = "weiblich", createdAt = DateTime.Now
            //});
            //entities.contact.Add(new Contact()
            //{
            //    person_id = 2,art_of_communication = EKindOfCommunication.Email,
            //    contact_value ="test.lala@dcv.at",contact_type = EContactType.Geschäftlich,
            //    main_contact = true,createdAt = DateTime.Now 
            //});
            //entities.comment.Add(new Comment()
            //{
            //    person_id = 2,
            //    comment_value = "TEST TEST 1 2 3 lalelu",
            //    value_date = DateTime.Parse("15.04.2020"),
            //    createdAt = DateTime.Now
            //}) ;
            //entities.address.Add(new Address()
            //{
            //    street = "Mökleburg",
            //    place = "Bregenz",
            //    zip = 6800,
            //    country = "Austria",
            //    contact_type = EContactType.Privat,
            //    billing_address = true,
            //    createdAt = DateTime.Now
            //}) ;
            //entities.SaveChanges();
            //var adr = entities.address.FirstOrDefault();
            //var x = entities.person.FirstOrDefault();
            //entities.addressperson.Add(new AddressPerson() 
            //{
            //    addressId = adr.id,
            //    personId = x.id
            //});
            //entities.SaveChanges();
           
            
            //x.addresses[0].address.contact_type = EContactType.Geschäftlich;
            //
            //
            //x.addresses[0].address.modifyAt = DateTime.Now;
            //entities.Update(x);
            //entities.SaveChanges();
            //var xy = repo.FindAll();
            //
            //
            //
            //    Console.WriteLine(" ");
        }
    }
}
