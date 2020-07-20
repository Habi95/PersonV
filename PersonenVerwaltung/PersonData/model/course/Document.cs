using PersonData.model.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonData
{
    [Table("documents")]
    public class Document : BaseClassCreatedModify
    {
        /// <summary>
        /// Path to the document
        /// </summary>
        [Column("url", TypeName = "varchar(200)")]
        public string Url { get; set; }

        /// <summary>
        /// Name of the document
        /// </summary>
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        /// <summary>
        /// Comment for the document
        /// </summary>
        [Column("comment", TypeName = "varchar(500)")]
        public string Comment { get; set; }

        /// <summary>
        /// Connection to reminder
        /// </summary>
        [Column("reminder_id")]
        public int? ReminderId { get; set; }

        /// <summary>
        /// Document type Invitation, RegistrationConfirmation, Bill, Dun, Diploma, Information, Note, Other
        /// </summary>
        [Column("type", TypeName = "varchar(50)")]
        public EDocumentType type { get; set; }

        [NotMapped]
        public List<DocumentClass> Classes { get; set; }

        [NotMapped]
        public BaseClassCreatedModify DocumentOwner { get; set; }
    }
}