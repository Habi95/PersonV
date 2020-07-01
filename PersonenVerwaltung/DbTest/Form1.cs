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
           //
            var entities = new PersonEntities();
            PersonRepository repo = new PersonRepository(entities);        

            repo.GetPersons();
            var x = entities.person.Where(x => x.id == 2).FirstOrDefault();
            entities.SaveChanges();

        }

    }
}
