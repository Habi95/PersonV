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
            //Person person = new Person() {name1 = "peter" , name2 = "Maier", gender = "männlich", date = DateTime.Parse("20.05.1998"),
            //    function = EFunction.Interessent, aktiv = true, deleted_inaktiv = false, newsletter_flag = false, createdAt = DateTime.Now };
            //Address address = new Address() { street = "hallistreet 56", place = "mäder" };
            var entities = new PersonEntities();
            PersonRepository repo = new PersonRepository(entities);
            // AdressPerson adressP = new AdressPerson() { adressId = 1, personId = 2 };
            //var lala = entities.address.ToList();
            //var lala1 = entities.adressperson.ToList();

            repo.GetPersons();
            var x = entities.person.Where(x => x.id == 2).FirstOrDefault();
            entities.SaveChanges();

        }

    }
}
