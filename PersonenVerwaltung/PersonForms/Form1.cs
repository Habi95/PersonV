using PersonController;
using PersonData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

PersonEntities ente = new PersonEntities();
            master_file master_file = (new master_file() { firstname = "My", lastname = "Lastname", function = EFunction.Interessent, aktiv = true, deleted_inaktiv = false, newsletter_flag = true });
            ente.master_file.Add(master_file);
            ente.SaveChanges();

        }

        Datahandling datahandling = new Datahandling();

        private void btnCreateNewPerson_Click(object sender, EventArgs e)
        {
           
        }

       
    }
}
