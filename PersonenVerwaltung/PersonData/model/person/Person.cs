using Data.Models;
using PersonData.model;
using PersonData.model.course;
using PersonData.model.material;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonData
{
    //TODO add list course teacher and student  list of courses , same realton communication as documents

    public class Person : BasePerson
    {
        public string title { get; set; }
        public decimal? sv_nr { get; set; }
        public string gender { get; set; }
        public string busy { get; set; }
        public string busy_by { get; set; }
        public string picture { get; set; }
        public EFunction? function { get; set; }
        public bool aktiv { get; set; }
        public bool deleted_inaktiv { get; set; }
        public bool newsletter_flag { get; set; }

        public List<AddressPerson> addresses { get; set; }
        public List<Comment> comments { get; set; }
        public List<Contact> contacts { get; set; }

        public List<RelCourseTrainer> courseTrainers { get; set; }
        public List<RelCourseParticipant> courseParticipants { get; set; }

        public List<book> book { get; set; }
        public List<equipment> equipment { get; set; }
        public List<notebook> notebook { get; set; }

        [NotMapped]
        public List<Document> documents { get; set; }

        [NotMapped]
        public List<Course> CompletedCourse { get; set; }

        [NotMapped]
        public List<Course> NotCompletedCourse { get; set; }

        [NotMapped]
        public List<RelCommunicationClass> Communications { get; set; }

        /*
         *  var bildString = Convert.ToBase64String(System.IO.File.ReadAllBytes(@"C:\xampp\lala.jpeg"));

            System.IO.File.WriteAllBytes(@"C:\xampp\lala1.jpeg", Convert.FromBase64String(bildString));
         */
    }
}