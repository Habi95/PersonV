using Data.Models;
using PersonData;
using PersonData.repo;
using System;
using System.Windows.Forms;

namespace DbTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var entities = new PersonEntities();
            CommunicationRepository repository = new CommunicationRepository(entities);
            var c = repository.GetCommunications<Person>(1);
            var d = repository.GetCommunications<Course>(1);
        }
    }
}