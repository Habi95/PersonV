using PersonData.model.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonData.model.course
{
    [Table("communication")]
    public class Communication
    {
        /// <summary>
        /// /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// the channel the communication was held on
        /// </summary>
        [Column("channel", TypeName = ("varchar(50)"))]
        public EChannel Channel { get; set; }

        /// <summary>
        /// the id of the person a communication was held with
        /// </summary>
        [Column("person_id")]
        public int PersonId { get; set; }

        /// <summary>
        /// needed for linking
        /// </summary>
        //[NotMapped]
        //public Person Person { get; set; }

        /// <summary>
        /// the date the communication was held on
        /// </summary>
        [Column("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// a open comment for the communication
        /// </summary>
        [Column("comment", TypeName = ("varchar(500)"))]
        public string? comment { get; set; } //Comment is in person a spefic class

        /// <summary>
        /// the id of a belonging document
        /// </summary>
        [Column("document_id")]
        public int? DocumentId { get; set; }

        /// <summary>
        /// needed for linking
        /// </summary>
        [NotMapped]
        public Document Document { get; set; }

        /// <summary>
        /// contains the id of a belonging reminder (Reminders not implemented yet)
        /// </summary>
        [Column("reminder_id")]
        public int? ReminderId { get; set; }

        /// <summary>
        /// date when the communication was created
        /// </summary>
        [Column("created@")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// date when the communication was modified
        /// </summary>
        [Column("modified@")]
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// list of all relations between communications and classes
        /// </summary>
        [NotMapped]
        public List<RelCommunicationClass> CommunicationClass { get; set; }
    }
}