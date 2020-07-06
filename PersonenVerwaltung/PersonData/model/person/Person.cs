﻿
using PersonData.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text;

namespace PersonData
{
    //TODO add list course teacher and student  list of courses , same realton communication as documents
    
   public class Person : BasePerson
    {
        public string title { get; set; }
        public decimal? sv_nr {get; set; }   
        public string gender { get; set; }
        public string busy { get; set; }
        public string busy_by { get; set; }
        public string picture { get; set; }
        public EFunction function { get; set; }
        public bool aktiv { get; set; }
        public bool deleted_inaktiv { get; set; }
        public bool newsletter_flag { get; set; }

        public List<AddressPerson> addresses { get; set; }
        public List<Comment> comments { get; set; }
        public List<Contact> contacts { get; set; }

        [NotMapped]
        public List<Document> documents { get; set; }

    }
}